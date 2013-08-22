#include "SDLPanel.h"
#ifdef WEBOS
#include "PDL.h"
#endif
#include "id.h"
#include "Colors.h"
#include <ctype.h>
#include <iostream>
#include <sstream>
#include <syslog.h>

using namespace std;

#ifdef WIN32
#define snprintf _snprintf
#endif

// Workaround for short screen on laptop.
#ifdef WIN32
#define HEIGHT 712
#else
#define HEIGHT 1024
#endif

#define WIDTH 768

#define HEIGHTWITHKEYBOARD 684

SDLPanel::SDLPanel()
{
    openlog("com.zetacentauri.telnet", 0, LOG_USER);

    _newlineMode = NEWLINE_LF;

    // initialize SDL
    if (SDL_Init(SDL_INIT_VIDEO) < 0)
    {
        syslog(LOG_ERR, "Unable to initialize SDL: %s\n", SDL_GetError());
    }

    SDL_EnableUNICODE(SDL_TRUE);
    // Set orientation to center button above the screen so we get proper
    // keyboard popup and coordinates.
#ifdef ARM
    PDL_Err err = PDL_SetOrientation(PDL_ORIENTATION_90);
#endif

    // Requires SDL_image 1.2.8.  PDK is using 1.2.7.
    //if( IMG_Init(IMG_INIT_PNG)) < 0 )
    //{
    //    std::cerr << "Unable to init PNG image support." << std::endl;
    //}

    // Setup video mode
//#ifndef ARM
    _screen = SDL_SetVideoMode(WIDTH, HEIGHT, 0, 0);
//#else
//    _screen = SDL_SetVideoMode(0, 0, 0, 0);
//#endif

    _done = false;
#ifndef NOHOTKEYS
    memset(_hotkeyButton, 0,  (sizeof(SDL_Surface*) * 12));
#endif
    _editLocation = EDIT_INPUT;
    _screenWidth = WIDTH;
    _keyboardMode = true;
    _screenHeight = HEIGHT;
    _inputWindow = NULL;
    _textWindow = NULL;
    _mccpEnabled = false;
#if SDL_BYTEORDER == SDL_BIG_ENDIAN
    _rmask = 0xff000000;
    _gmask = 0x00ff0000;
    _bmask = 0x0000ff00;
    _amask = 0x000000ff;
#else
    _rmask = 0x000000ff;
    _gmask = 0x0000ff00;
    _bmask = 0x00ff0000;
    _amask = 0xff000000;
#endif
#ifdef WIN32
    // Start Sockets
    WSADATA wsaData;
    WORD wVersionRequested = MAKEWORD( 1, 1 );
    WSAStartup( wVersionRequested, &wsaData );
#endif
    memset( _inputBuffer, 0, INPUT_BUFFER_SIZE );
    memset( _serverBuffer, 0, INPUT_BUFFER_SIZE );
    sprintf( _serverBuffer, HOSTNAME );
    memset( _portBuffer, 0, INPUT_BUFFER_SIZE );
    sprintf( _portBuffer, "%d", PORT );
    memset( _inputBufferPrevious, 0, INPUT_BUFFER_SIZE );
    _inptr = 0;
    _parseMode = MODE_NONE;
    _connected = false;
    _unclosedTag = false;
    _logMode = LOG_NONE;
    // TODO: Compensate for screen size changes.
    _colorMode = COLORMODE_ANSI;
    _foregroundColor = &clrWhite;
    _backgroundColor = &clrBlack;
    TTF_Init();
    CreateScreen();
    // Don't set this until here so we create the full rendering area, but then
    // only use what isn't covered by the screen.
    _screenHeight = HEIGHTWITHKEYBOARD;
    // Now that we've created the screen we can create the subsections.
    _inputWindow = new InputWindow( _screen );
    _textWindow = new TextWindow( _screen );
    if( _inputWindow != NULL )
    {
        _inputWindow->SetScreen( 0, (_screenHeight - TEXT_BOX_BORDER - TEXT_BOX_HEIGHT ), _screenWidth,
            (TEXT_BOX_HEIGHT + TEXT_BOX_BORDER ));
    }
    if( _textWindow != NULL )
    {
#ifndef NOHOTKEYS
        _textWindow->SetScreen( 0, ICON_HEIGHT, _screenWidth, (_screenHeight - TEXT_BOX_BORDER - TEXT_BOX_HEIGHT - ICON_HEIGHT ) );
#else
        _textWindow->SetScreen( 0, CONNECTBAR_HEIGHT, _screenWidth, (_screenHeight - TEXT_BOX_BORDER - TEXT_BOX_HEIGHT - CONNECTBAR_HEIGHT ) );
#endif
    }

#ifndef NOHOTKEYS
    _hotkeyButton[0] = IMG_Load("images//connect.png");
    _hotkeyButton[1] = IMG_Load("images//disconnect.png");
    _hotkeyButton[2] = IMG_Load("images//settings.png");
    _hotkeyButton[3] = IMG_Load("images//1.png");
    _hotkeyButton[4] = IMG_Load("images//2.png");
    _hotkeyButton[5] = IMG_Load("images//3.png");
    _hotkeyButton[6] = IMG_Load("images//4.png");
    _hotkeyButton[7] = IMG_Load("images//5.png");
    _hotkeyButton[8] = IMG_Load("images//6.png");
    _hotkeyButton[9] = IMG_Load("images//7.png");
    _hotkeyButton[10] = IMG_Load("images//8.png");
    _hotkeyButton[11] = IMG_Load("images//9.png");
    for( int i = 0; i < 12; i++ )
    {
        if(_hotkeyButton[i] == NULL )
        {
            printf("Could not load image.");
        }
    }
    printf("Icon images loaded.\n");
#else
    _connectBar = new ConnectBar( _screen );
    _connectBar->SetScreen( 0, 0, _screenWidth, CONNECTBAR_HEIGHT );
#endif
    // SDL_Surface* temp = SDL_LoadBMP("1.bmp");
    // SDL_Surface* bg = SDL_DisplayFormat(temp);
    // SDL_FreeSurface(temp);
}

SDLPanel::~SDLPanel()
{
#ifdef WIN32
    WSACleanup();
#endif
    if (_screen != NULL)
    {
        SDL_FreeSurface(_screen);
    }
    // Cleanup SDL
    //IMG_Quit(); // Requires SDL_image 1.2.8
    SDL_Quit();
}

bool SDLPanel::IsDone()
{
    return _done;
}

void SDLPanel::OnSize(SDL_ResizeEvent& event)
{
    _screenWidth = (short)event.w;
    _screenHeight = (short)event.h;
    // Call CreateScreen to delete and recreate the screen, but only if necessary.
    syslog(LOG_INFO, "OnSize: Screen size changed to w: %d, h: %d.\n", event.w, event.h);
    CreateScreen();
    Render();
}

void SDLPanel::ProcessNetwork()
{
    // Process the network.
    if( _connected )
    {
        //printf("Processing network.\n");
        char recvbuf[(RECEIVE_BUFFER_SIZE+1)];
        char outputbuf[(RECEIVE_BUFFER_SIZE+1)];
        memset( outputbuf, 0, (RECEIVE_BUFFER_SIZE+1) );
        memset( recvbuf, 0, (RECEIVE_BUFFER_SIZE+1) );
        int error = recv(_connection, recvbuf, RECEIVE_BUFFER_SIZE, 0 );
        if( error > 0 )
        {
            //printf("recv() returned %d.\n", error);
            // Call a function to process our received data.
            if( _colorMode == COLORMODE_NONE )
            {
                   printf("Calling RemoveANSICodes.\n");
                   RemoveANSICodes(recvbuf);
            }
            if( _colorMode == COLORMODE_NONE || _colorMode == COLORMODE_RAW )
            {
                // For when we are not processing our text.
                int incount = 0;
                int outcount = 0;
                int numChars = sizeof( recvbuf );
                for( incount = 0; incount < numChars; incount++ )
                {
                   if( recvbuf[incount] == '\r' )
                   {
                       continue;
                   }
                   else if( recvbuf[incount] != '\n' )
                   {
                        outputbuf[outcount] = recvbuf[incount];
                        outcount++;
                   }
                   else
                   {
                        FlushText( outputbuf, (int)strlen(outputbuf), true );
                        memset( outputbuf, 0, RECEIVE_BUFFER_SIZE );
                        outcount = 0;
                        _foregroundColor = &clrWhite;
                   }
                }
                printf("Calling FlushText.\n");
                FlushText( outputbuf, (int)strlen(outputbuf), false );
            }
            else
            {
                ProcessANSICodes(recvbuf);
            }
        }
        // We had an error in our call to recv(), deal with it:
        else
        {
#ifdef WIN32
            int errorNo = WSAGetLastError();
            if( errorNo != WSAEWOULDBLOCK )
#else
            int errorNo;
            int len = sizeof(int);
            getsockopt( _connection, SOL_SOCKET, SO_ERROR, (void *)&errorNo, (socklen_t*)&len );
            // TODO: Figure out what file to include to get this to work.
            //if( errorNo != E_WOULDBLOCK )
            if( errorNo != 0 )
#endif
            {
                char errBuf[256];
                memset( errBuf, 0, 256 );
                snprintf( errBuf, 256, "Socket Error: %d", errorNo );
                FlushText( errBuf, (int)strlen(errBuf), true );
                _connected = false;
            }
        }
    }
}

void SDLPanel::Render()
{
    //printf("Rendering.\n");

    // Lock surface if needed
    if (SDL_MUSTLOCK(_screen))
    {
        if (SDL_LockSurface(_screen) < 0)
        {
            printf("Failed to lock screen.\n");
            return;
        }
    }

    // Ask SDL for the time in milliseconds
    //int tick = SDL_GetTicks();

    // We may need to blank the screen before drawing.

    /***********************
    * Draw GUI Elements
    ************************/
    // Draw the command buttons
#ifndef NOHOTKEYS
    SDL_Rect rect;
    rect.x = 0;
    rect.y = 0;
    rect.w = ICON_HEIGHT;
    rect.h = ICON_HEIGHT;
    for( short i = 0; i < 12; i++ )
    {
        rect.x = i * ICON_HEIGHT;
        SDL_BlitSurface(_hotkeyButton[i], NULL, _screen, &rect);
    }
#else
    // Draw the connect bar
    if( _connectBar != NULL )
    {
        bool serverFocused = (_editLocation == EDIT_SERVER);
        bool portFocused = (_editLocation == EDIT_PORT);
        _connectBar->SetInputBuffer(_serverBuffer);
        _connectBar->SetPortBuffer(_portBuffer);
        _connectBar->Render(serverFocused, portFocused, _newlineMode);
    }
#endif

    // Draw the input section.
    if( _inputWindow != NULL )
    {
        _inputWindow->SetInputBuffer( _inputBuffer );
        _inputWindow->Render((_editLocation == EDIT_INPUT ));
    }
    // Draw the output section.
    if( _textWindow != NULL )
    {
        _textWindow->Render();
    }
    /***********************
    * End Draw GUI Elements
    ************************/

    // Unlock if needed
    if (SDL_MUSTLOCK(_screen))
    {
        SDL_UnlockSurface(_screen);
    }

    SDL_UpdateRect(_screen, 0, 0, 0, 0);
}

void SDLPanel::CreateScreen()
{
    if( _screenWidth % 4 != 0 )
    {
        _screenWidth -= _screenWidth % 4;
    }

    if (_screen == NULL)
    {
        _screen = SDL_CreateRGBSurface(SDL_HWSURFACE, _screenWidth, _screenHeight, 24, _rmask, _gmask, _bmask, _amask );
        if( _textWindow != NULL ) _textWindow->SetSurface( _screen );
        if( _inputWindow != NULL ) _inputWindow->SetSurface( _screen );
        if( _screen == NULL )
        {
            syslog(LOG_ERR, "CreateScreen: Failed to create screen X: %d, Y: %d", _screenWidth, _screenHeight);
        }
        else
        {
            syslog(LOG_INFO, "CreateScreen: Screen created X: %d, Y: %d.\n", _screenWidth, _screenHeight);
        }
    }
    // Size has changed - nuke and redo.
    else if( _screen->w != _screenWidth || _screen->h != _screenHeight )
    {
        SDL_FreeSurface( _screen );
        _screen = SDL_CreateRGBSurface(SDL_HWSURFACE, _screenWidth, _screenHeight, 24, _rmask, _gmask, _bmask, _amask );
        if( _textWindow != NULL ) _textWindow->SetSurface( _screen );
        if( _inputWindow != NULL ) _inputWindow->SetSurface( _screen );
        if( _screen == NULL )
        {
            syslog(LOG_ERR, "CreateScreen: Failed to resize screen to X: %d, Y: %d", _screenWidth, _screenHeight);
        }
        else
        {
            syslog(LOG_INFO, "CreateScreen: Screen resized to X: %d, Y: %d.\n", _screenWidth, _screenHeight);
        }
    }
    else
    {
        return;
    }

    // Active width: width of usable area for map and status windows.
    // Active width equals half of the width minus half of the border (half of the border will be in status, half in map)
    if( _inputWindow != NULL )
    {
        _inputWindow->SetScreen( 0, (_screenHeight - TEXT_BOX_BORDER - TEXT_BOX_HEIGHT ), _screenWidth,
            (TEXT_BOX_HEIGHT + TEXT_BOX_BORDER ));
    }
    if( _textWindow != NULL )
    {
#ifndef NOHOTKEYS
        _textWindow->SetScreen( 0, ICON_HEIGHT, _screenWidth, (_screenHeight - TEXT_BOX_BORDER - TEXT_BOX_HEIGHT - ICON_HEIGHT ) );
#else
        _textWindow->SetScreen( 0, CONNECTBAR_HEIGHT, _screenWidth, (_screenHeight - TEXT_BOX_BORDER - TEXT_BOX_HEIGHT - ICON_HEIGHT ) );
#endif
    }

    syslog(LOG_INFO, "CreateScreen: Screen attached to text and input windows.\n");
}

void SDLPanel::Connect()
{
    if( _serverBuffer == NULL || strlen(_serverBuffer) < 1 )
    {
        FlushText( "Cannot connect - no address entered.", 36, true );
        return;
    }
    // Initialize TCP/IP Socket
    unsigned long nonblocking = 1;

    _connection = socket( AF_INET, SOCK_STREAM, 0 );
    memset( &_servaddr, 0, sizeof(_servaddr));
    _servaddr.sin_family = AF_INET;
    _port = atoi(_portBuffer);
    _servaddr.sin_port = htons( _port );
    struct hostent *host = gethostbyname( _serverBuffer );
    memcpy( (void *)&_servaddr.sin_addr, (void *)host->h_addr_list[0], sizeof( _servaddr.sin_addr ) );
    stringstream io;
    io << "Connecting to " << _serverBuffer << ", Port " << _portBuffer;
    FlushText( io.str().c_str(), io.str().length(), true );
    io << endl;
    //syslog( LOG_INFO, io.str().c_str());
    int error = connect( _connection, (const struct sockaddr *)&_servaddr, sizeof(_servaddr) );
    // Error on Connect, get number
    if( error )
    {
#ifdef WIN32
        error = WSAGetLastError();
#endif
    }
    else
    {
        // make it a nonblocking socket
#ifdef WIN32
        ioctlsocket( _connection, FIONBIO, &nonblocking );
#else
        fcntl( _connection, F_SETFL, O_NONBLOCK );
#endif
        _connected = true;
        _editLocation = EDIT_INPUT;
        _inptr = strlen(_inputBuffer);
        FlushText( "Connected to server.", 20, true );
    }
}

void SDLPanel::Disconnect()
{
    // Disconnect the TCP/IP Socket
#ifdef WIN32
    closesocket( _connection );
#else
    close( _connection );
#endif
    _connected = false;
    FlushText( "Disconnected from server.", 25, true );
}

void SDLPanel::OnKey(SDL_KeyboardEvent& event)
{
    // TODO: Implement a blinking cursor.
    // TODO: Implement insert and overtype modes.

    //syslog(LOG_INFO, "keysym.sym: %d\n", event.keysym.sym );

    char* charBuffer;
    switch(_editLocation)
    {
        case EDIT_SERVER:
            charBuffer = _serverBuffer;
            break;
        case EDIT_PORT:
            charBuffer = _portBuffer;
            break;
        case EDIT_INPUT:
            charBuffer = _inputBuffer;
            break;
    }

    // Send the buffer and clear it.
    if( event.keysym.sym == SDLK_RETURN )
    {
        // Add a newline for the benefit of the recipient.
        int endpoint = (int)strlen( charBuffer );
        if( endpoint < (INPUT_BUFFER_SIZE + 1))
        {
            switch( _newlineMode )
            {
                case NEWLINE_LF:
                     charBuffer[endpoint] = '\n';
                     break;
                case NEWLINE_CR:
                     charBuffer[endpoint] = '\r';
                     break;
                case NEWLINE_CRLF:
                     charBuffer[endpoint] = '\r';
                     if( endpoint < (INPUT_BUFFER_SIZE + 2))
                         charBuffer[endpoint+1] = '\n';
                     break;
                case NEWLINE_LFCR:
                     charBuffer[endpoint] = '\n';
                     if( endpoint < (INPUT_BUFFER_SIZE + 2))
                         charBuffer[endpoint+1] = '\r';
                     break;
            }
        }
        // We ifdef for win32 here because the alias expansion is broken on Linux.
#ifdef WIN32
        // If we have a pound key, process it as if it were a tintin command.
        // Additionally, here is where we will want to process any alias expansion.
        if( charBuffer[0] == '#' )
        {
            // Get the keyword to see what we've got
            char* keyword = strtok( charBuffer, " " );
            // Check whether we have an alias
            //if( HasStringPrefix( "#al", keyword ))
            //{
            //    // We have an alias, get the keyword.
            //    char* alias = strtok( NULL, " " );
            //    // Get the remainder of the string.
            //    char* expansion = strtok( NULL, "\n" );
            //    // Insert the keyword and the expansion into our map.
            //    if( alias == NULL )
            //    {
            //        // Just print all of our aliases and return.
            //        SDL_Color* tmpColor = _foregroundColor;
            //        _foregroundColor = &clrWhite;
            //        ConfigurationSettings::iterator it;
            //        // Write a line to the screen for each alias, in white text.
            //        for( it = _aliasData->begin(); it != _aliasData->end(); it++ )
            //        {
            //            std::string first = (it->first).c_str();
            //            std::string second = (it->second).c_str();
            //            char aliasText[INPUT_BUFFER_SIZE];
            //            memset( aliasText, 0, INPUT_BUFFER_SIZE );
            //            snprintf( aliasText, INPUT_BUFFER_SIZE, "ALIAS:: %s :: %s\0", first.c_str(), second.c_str() );
            //            FlushText( aliasText, (int)strlen(aliasText), true );
            //        }
            //        _foregroundColor = tmpColor;
            //        _inptr = 0;
            //        memset( _inputBuffer, 0, INPUT_BUFFER_SIZE );
            //        return;
            //    }
            //    else if( expansion == NULL )
            //    {
            //        // Find the alias we're referring to and print it.
            //        std::map<std::string, std::string>::iterator it;
            //        char tmpAlias[600];
            //        memset( tmpAlias, 0, 600 );
            //        // Copy everything but the newline into our search string.
            //        memcpy( tmpAlias, alias, strlen(alias)-1 );
            //        it = _aliases.find( tmpAlias );
            //        if( it != _aliases.end() )
            //        {
            //            // Print this result to the screen.
            //            FlushText( (it->second).c_str(), (int)((it->second).size()), true );
            //        }
            //        // Create a new character buffer the length of the text we sent and add it
            //        // to the text buffer to be rendered.
            //        // TODO: Only add this to the text buffer if we have local echo turned on.
            //        // TODO: Make sendtext color user-configurable.
            //        SDL_Color* tmpColor = _foregroundColor;
            //        _foregroundColor = &clrWhite;
            //        char tmpBuffer[INPUT_BUFFER_SIZE];
            //        snprintf( tmpBuffer, INPUT_BUFFER_SIZE, "%s %s", keyword, tmpAlias );
            //        // Subtract 1 so we don't render the \n as a box.
            //        FlushText( tmpBuffer, (int)strlen(tmpBuffer), true );
            //        _foregroundColor = tmpColor;
            //        memcpy( _inputBufferPrevious, tmpBuffer, INPUT_BUFFER_SIZE );
            //        _inptr = 0;
            //        memset( _inputBuffer, 0, INPUT_BUFFER_SIZE );
            //        return;
            //    }
            //    char *permAlias = new char[strlen(alias)+1];
            //    memset( permAlias, 0, strlen(alias)+1 );
            //    memcpy( permAlias, alias, strlen(alias) );
            //    char *permExpansion = new char[strlen(expansion)+1];
            //    memset( permExpansion, 0, strlen(expansion)+1 );
            //    memcpy( permExpansion, expansion, strlen(expansion) );
            //    //_aliases.insert( std::pair<char *, char *>( permAlias, permExpansion ));
            //    //_aliases[permAlias] = permExpansion;
            //    //_aliasData->setValue( permAlias, permExpansion );
            //    // Create a new character buffer the length of the text we sent and add it
            //    // to the text buffer to be rendered.
            //    // TODO: Only add this to the text buffer if we have local echo turned on.
            //    // TODO: Make sendtext color user-configurable.
            //    SDL_Color* tmpColor = _foregroundColor;
            //    _foregroundColor = &clrWhite;
            //    // Subtract 1 so we don't render the \n as a box.
            //    char tmpBuffer[INPUT_BUFFER_SIZE];
            //    snprintf( tmpBuffer, INPUT_BUFFER_SIZE, "%s %s %s", keyword, alias, expansion );
            //    FlushText( tmpBuffer, (int)strlen( tmpBuffer ), true );
            //    _foregroundColor = tmpColor;
            //    memcpy( _inputBufferPrevious, tmpBuffer, INPUT_BUFFER_SIZE );
            //    _inptr = 0;
            //    memset( _inputBuffer, 0, INPUT_BUFFER_SIZE );
            //    // Nothing to transmit - it was an internal command.
            //    return;
            //}
        }
        else
        {
            // Get the first word of the string.
            char* keyword = strtok( charBuffer, " " );
            // Set the inputbuffer to the rest of the string.
            char* tmpBuffer;
            tmpBuffer = strtok( NULL, "\n" );
            // Look up the expansion of the keyword.  Blank the last char if it is an '\r' or '\n' so we can get a good search.
            int keylength = (int)strlen(keyword);
            if( keyword[(keylength-1)] == '\n' || keyword[(keylength-1)] == '\r' )
            {
                keyword[(keylength-1)] = '\0';
            }
            std::map<std::string, std::string>::iterator it = _aliases.find( keyword );
            if( it != _aliases.end() )
            {
                // Concatenate the expansion with the rest of the string.
                if( tmpBuffer != NULL )
                {
                    std::string tmpString = it->second;
                    // We have to do this because tmpBuffer points to a place inside _inputBuffer and gets overwritten.
                    char tmp[INPUT_BUFFER_SIZE];
                    memset( tmp, 0, INPUT_BUFFER_SIZE );
                    snprintf( tmp, INPUT_BUFFER_SIZE, "%s %s\n\0", tmpString.c_str(), tmpBuffer );
                    memcpy( charBuffer, tmp, INPUT_BUFFER_SIZE );
                }
                else
                {
                    std::string tmpString = it->second;
                    snprintf( charBuffer, INPUT_BUFFER_SIZE, "%s\n\0", tmpString.c_str() );
                }
            }
            else
            {
                // Put the keyword back where we found it.  Since the newline was stripped to we have to reattach that.
                if( tmpBuffer != NULL )
                {
                    // We have to do this because tmpBuffer points to a place inside _inputBuffer and gets overwritten.
                    char tmp[INPUT_BUFFER_SIZE];
                    memset( tmp, 0, INPUT_BUFFER_SIZE );
                    snprintf( tmp, INPUT_BUFFER_SIZE, "%s %s\n\0", keyword, tmpBuffer );
                    memcpy( charBuffer, tmp, INPUT_BUFFER_SIZE );
                }
                else
                {
                    // We only get here if there isn't anything after the first word (a single word), so we don't need
                    // to add back the newline.
                    snprintf( charBuffer, INPUT_BUFFER_SIZE, "%s\n\0", keyword );
                }
            }
        }
#endif
        // Create a new character buffer the length of the text we sent and add it
        // to the text buffer to be rendered.
        // TODO: Only add this to the text buffer if we have local echo turned on.
        // TODO: Make sendtext color user-configurable.
        SDL_Color* tmpColor = _foregroundColor;
        _foregroundColor = &clrWhite;
        int length = (int)strlen(_inputBuffer);
        // Flush text before sending so we don't see the boxes for semicolon replacement.
        // Subtract 1 so we don't render the \n as a box.
        FlushText( charBuffer, (length-1), true );
        _foregroundColor = tmpColor;

        // Semicolon replacement.
        int inputlen = (int)strlen( charBuffer );
        int iter = 0;
        for( iter = 0; iter < inputlen; iter++ )
        {
            if( charBuffer[iter] == ';' )
            {
                charBuffer[iter] = '\n';
            }
        }

        if( _connected )
        {
          send(_connection, charBuffer, inputlen, 0 );
        }

        // Copy to previous and then clear.
        memcpy( _inputBufferPrevious, charBuffer, INPUT_BUFFER_SIZE );
        _inptr = 0;
        memset( charBuffer, 0, INPUT_BUFFER_SIZE );
    }
    // Delete previous char and move back.
    else if( event.keysym.sym == SDLK_BACKSPACE )
    {
        if( _inptr > 0 )
        {
            _inptr--;
            charBuffer[_inptr] = '\0';
        }
    }
    // Delete current char and slide text left.
    else if( event.keysym.sym == SDLK_DELETE )
    {
        if( _inptr < (INPUT_BUFFER_SIZE - 1))
        {
            memcpy( &charBuffer[_inptr], &charBuffer[(_inptr+1)], (INPUT_BUFFER_SIZE - _inptr - 1));
            charBuffer[(INPUT_BUFFER_SIZE - 1)] = '\0';
        }
    }
    // Move one to the left.
    else if( event.keysym.sym == SDLK_LEFT )
    {
        if( _inptr > 0 )
        {
            _inptr--;
        }
    }
    // Show the previous input buffer.
    else if( event.keysym.sym == SDLK_UP )
    {
        // Copy previous input into current.
        memcpy( charBuffer, _inputBufferPrevious, INPUT_BUFFER_SIZE );
        _inptr = 0;
    }
    // Move to beginning of text.
    else if( event.keysym.sym == SDLK_HOME )
    {
        _inptr = 0;
    }
    // Move to the end of the text.
    else if( event.keysym.sym == SDLK_END )
    {
        // TODO: Scan for the first NULL and place the cursor there.
    }
    // Move one to the right.
    else if( event.keysym.sym == SDLK_RIGHT )
    {
        if( _inptr < INPUT_BUFFER_SIZE && charBuffer[(_inptr+1)] != '\0' )
        {
            _inptr++;
        }
    }
    else if( _inptr < INPUT_BUFFER_SIZE )
    {
        if( event.keysym.sym == SDLK_LSHIFT || event.keysym.sym == SDLK_RSHIFT )
        {
            return;
        }
        if(  event.keysym.sym >= SDLK_a && event.keysym.sym <= SDLK_z)
        {
            //syslog(LOG_INFO, "Unicode symbol: %d, Filtered: %d", event.keysym.unicode, event.keysym.unicode&0x7F);
            charBuffer[_inptr] = event.keysym.unicode & 0x7F;
        }
        else if( event.keysym.mod & KMOD_SHIFT || event.keysym.mod & KMOD_CAPS )
        {
                switch( event.keysym.sym )
                {
                    default:
                        charBuffer[_inptr] = event.keysym.sym;
                        break;
                    case SDLK_SLASH:
                        charBuffer[_inptr] = '?';
                        break;
                    case SDLK_QUOTE:
                        charBuffer[_inptr] = '"';
                        break;
                    case SDLK_SEMICOLON:
                        charBuffer[_inptr] = ':';
                        break;
                    case SDLK_COMMA:
                        charBuffer[_inptr] = '<';
                        break;
                    case SDLK_PERIOD:
                        charBuffer[_inptr] = '>';
                        break;
                    case SDLK_MINUS:
                        charBuffer[_inptr] = '_';
                        break;
                    case SDLK_LEFTBRACKET:
                        charBuffer[_inptr] = '{';
                        break;
                    case SDLK_RIGHTBRACKET:
                        charBuffer[_inptr] = '}';
                        break;
                    case SDLK_EQUALS:
                        charBuffer[_inptr] = '+';
                        break;
                    case SDLK_0:
                        charBuffer[_inptr] = ')';
                        break;
                    case SDLK_1:
                        charBuffer[_inptr] = '!';
                        break;
                    case SDLK_2:
                        charBuffer[_inptr] = '@';
                        break;
                    case SDLK_3:
                        charBuffer[_inptr] = '#';
                        break;
                    case SDLK_4:
                        charBuffer[_inptr] = '$';
                        break;
                    case SDLK_5:
                        charBuffer[_inptr] = '%';
                        break;
                    case SDLK_6:
                        charBuffer[_inptr] = '^';
                        break;
                    case SDLK_7:
                        charBuffer[_inptr] = '&';
                        break;
                    case SDLK_8:
                        charBuffer[_inptr] = '*';
                        break;
                    case SDLK_9:
                        charBuffer[_inptr] = '(';
                        break;
                    case SDLK_BACKQUOTE:
                        charBuffer[_inptr] = '~';
                        break;
                    case SDLK_BACKSLASH:
                        charBuffer[_inptr] = '|';
                        break;
                }
        }
        else
        {
            charBuffer[_inptr] = event.keysym.sym;
        }
        // We never want _inptr to hit the end of our buffer.
        if( _inptr < (INPUT_BUFFER_SIZE - 1))
        {
          _inptr++;
        }
    }
    return;
}

void SDLPanel::RemoveANSICodes( char* string )
{
    int length = (int)strlen(string);
    int incount;
    int outcount = 0;
    char *instring = new char[length];
    memset( instring, 0, length );
    memcpy( instring, string, length );

    for( incount = 0; incount < length; incount++ )
    {
        if( instring[incount] == 27 )
        {
            // Go until we find an m to terminate the string.
            // This may not work properly with ANSI codes at the end of a string.
            // Not sure why it was length+1 but this has been changed.
            while( instring[incount] != 'm' && incount < (length+1) )
            //while( instring[incount] != 'm' && incount < (length) )
            {
                incount++;
            }
            // We found an m at the end of the string.  Skip that too.
            continue;
        }
        string[outcount] = instring[incount];
        outcount++;
    }
    // Clear the rest of the string.
    if( outcount < length )
    {
        memset( &string[outcount], 0, (length - outcount) );
    }

    delete instring;

    return;
}

// Strips \n and \r characters from a string.
void SDLPanel::RemoveNewlines( char* string )
{
    int length = (int)strlen(string);
    int incount;
    int outcount = 0;
    char *instring = new char[length];
    memset( instring, 0, length );
    memcpy( instring, string, length );

    for( incount = 0; incount < length; incount++ )
    {
        if( instring[incount] == '\n' || instring[incount] == '\r' )
        {
            continue;
        }
        string[outcount] = instring[incount];
        outcount++;
    }
    // Clear the rest of the string.
    if( outcount < length )
    {
        memset( &string[outcount], 0, (length - outcount) );
    }

    delete instring;

    return;
}

void SDLPanel::RemoveUnprintableASCIIChars( char* string )
{
    int length = (int)strlen(string);
    int incount;
    int outcount = 0;
    char *instring = new char[length];
    memset( instring, 0, length );
    memcpy( instring, string, length );

    for( incount = 0; incount < length; incount++ )
    {
        // 0xFF + 0xFB + 0X55 and 0xFF + 0xFB + 0x56 are for compression settings.
        int value = instring[incount];
        if( value == -1 || value == -5 || value == -4 || value == 1 || value == 9)
        {
            continue;
        }
        string[outcount] = instring[incount];
        outcount++;
    }
    // Clear the rest of the string.
    if( outcount < length )
    {
        memset( &string[outcount], 0, (length - outcount) );
    }

    delete instring;

    return;
}

// Strips \n and \r characters and spaces from a string.
void SDLPanel::RemoveNewlinesAndSpaces( char* string )
{
    int length = (int)strlen(string);
    int incount;
    int outcount = 0;
    char *instring = new char[length];
    memset( instring, 0, length );
    memcpy( instring, string, length );

    for( incount = 0; incount < length; incount++ )
    {
        if( instring[incount] == '\n' || instring[incount] == '\r' || instring[incount] == ' ' )
        {
            continue;
        }
        string[outcount] = instring[incount];
        outcount++;
    }
    // Clear the rest of the string.
    if( outcount < length )
    {
        memset( &string[outcount], 0, (length - outcount) );
    }

    delete instring;

    return;
}

// TODO:
// What we should be doing, rather than allowing separate strings in any combination,
// we should instead just grab the entire string from ESC[ to m.
//
// Once we have that, we can check for internal semicolons and other settings and
// process it as a single unit.  This makes sense because an ESC[1;37m doesn't mean
// flush text, change to bold, then flush text, then change to white.  Instead it means
// flush text and then change to bold white.
//
// The ANSI code processing as it is now is very broken and only partially works, only
// some of the time.
void SDLPanel::ProcessANSICodes( char* string )
{
    int length = (int)strlen(string);
    int incount;
    int outcount = 0;
    char *instring = new char[length];
    memset( instring, 0, length );
    memcpy( instring, string, length );

    for( incount = 0; incount < length; incount++ )
    {
        if( instring[incount] == '\n' )
        {
            // Newline found, flush all text to screen.
            FlushText( string, outcount, true );
            memset( string, 0, (outcount+1) );
            outcount = 0;
            // Uncomment this code if we want color to reset to white at the end of each line.
            //_foregroundColor = &clrWhite;
            //_foregroundColorNum = COLOR_WHITE;
            continue;
        }
        else if( instring[incount] == '\r' )
        {
            continue;
        }
        else if( instring[incount] == 27 )
        {
            // Go until we find an m to terminate the string.
            // This may not work properly with ANSI codes at the end of a string.
            // This should allow us to process any number of ANSI codes.
            int ansiStringPtr = 0;
            char* ansiString = new char[length+1];
            memset( ansiString, 0, length+1 );
            while( instring[incount] != 'm' && incount < (length) )
            {
                // Copy to our ANSI string buffer.
                if( instring[incount] != 27 && instring[incount] != '[' )
                {
                    ansiString[ansiStringPtr] = instring[incount];
                    ansiStringPtr++;
                }
                incount++;
            }

            // We found an m at the end of the string.  Skip that too, but DO flush colors if we have them.
            SDL_Color* newcolor = GetColorFromAnsiCode( ansiString );
            delete[] ansiString;
            if( newcolor != NULL )
            {
                if( outcount > 0 )
                {
                    FlushText( string, outcount, false );
                    memset( string, 0, (outcount+1) );
                    outcount = 0;
                }
                _foregroundColor = newcolor;
            }
            continue;
        }
        string[outcount] = instring[incount];
        outcount++;
    }
    // If we reach the end of the buffer without doing anything, put the string onscreen.
    FlushText( string, outcount, false );
    memset( string, 0, (outcount+1) );
    outcount = 0;

    delete instring;

    return;
}

void SDLPanel::ChangeLogMode( int mode )
{
    _logMode = mode;
}

void SDLPanel::ChangeColorMode( int mode )
{
    _colorMode = mode;
}

SDL_Color* SDLPanel::GetColorFromAnsiCode( char* text )
{
    if( text == NULL || text[0] == '\0' )
    {
        return NULL;
    }

    if( !strcmp( text, "0" ))
    {
        return &clrDkwhite;
    }
    else if( !strcmp( text, "1;37" ) || !strcmp( text, "1;5;40;37" ))
    {
        return &clrWhite;
    }
    else if( !strcmp( text, "37" ) || !strcmp( text, "0;37" ))
    {
        return &clrDkwhite;
    }
    else if( !strcmp( text, "1;30" ))
    {
        return &clrGray;
    }
    else if( !strcmp( text, "30" ) || !strcmp( text, "0;30" ))
    {
        return &clrBlackText;
    }
    else if( !strcmp( text, "1;31" ) || !strcmp( text, "0;31;40;1" ) || !strcmp( text, "31;40;1" ))
    {
        return &clrRed;
    }
    else if( !strcmp( text, "31" ) || !strcmp( text, "0;31" ))
    {
        return &clrDkred;
    }
    else if( !strcmp( text, "1;32" ))
    {
        return &clrGreen;
    }
    else if( !strcmp( text, "32" ) || !strcmp( text, "0;32" ))
    {
        return &clrDkgreen;
    }
    else if( !strcmp( text, "1;33" ) || !strcmp( text, "1;5;40;33" ) || !strcmp( text, "0;33;40;1" ) )
    {
        return &clrYellow;
    }
    else if( !strcmp( text, "33" ) || !strcmp( text, "0;33" ))
    {
        return &clrOrange;
    }
    else if( !strcmp( text, "1;34" ) || !strcmp( text, "0;34;1" ) || !strcmp( text, "1;5;40;34" ) )
    {
        return &clrBlue;
    }
    else if( !strcmp( text, "34" ) || !strcmp( text, "0;34" ))
    {
        return &clrDkblue;
    }
    else if( !strcmp( text, "1;35" ) || !strcmp( text, "0;35;40;1" ))
    {
        return &clrPurple;
    }
    else if( !strcmp( text, "35" ) || !strcmp( text, "0;35" ) )
    {
        return &clrDkpurple;
    }
    else if( !strcmp( text, "1;36" ))
    {
        return &clrCyan;
    }
    else if( !strcmp( text, "36" ) || !strcmp( text, "0;36" ))
    {
        return &clrDkcyan;
    }
    else
    {
        return NULL;
    }
}

void SDLPanel::FlushText( const char * text, int length, bool newline )
{
    // Do automatic word wrap.
    if( length > 77 )
    {
        FlushText(text, 77, true);
        FlushText(&text[77], length-77, newline);
        return;
    }

    //syslog(LOG_INFO, "Adding text to window: %s\n", text);
    if( length > 0 && _textWindow != NULL )
    {
        StringEntry* entry = new StringEntry;
        entry->string = new char[(length+1)];
        memset( entry->string, 0, (length+1));
        memcpy( entry->string, text, length);
        // Remove ASCII that can't be printed to the screen.
        RemoveUnprintableASCIIChars(entry->string);
        entry->color = *_foregroundColor;
        // Special processing required for newline = false;
        entry->newline = newline;
        _textWindow->AddStringEntry( entry );
    }
    // This is to keep us from dropping a newline that just came after a color change.
    // In this case, we may only want to append the newline if the previous line was
    // not a newline (find last item and change newline to true), otherwise we will
    // get too many newlines in our text window.  This shall be left to our text window
    // to decide.
    else if( length == 0 && newline )
    {
        _textWindow->AddNewline();
    }
    return;
}

void SDLPanel::SetDisplayFont( const char *font, int size )
{
    if( _textWindow != NULL )
    {
        _textWindow->SetDisplayFont( font, size );
    }
}

void SDLPanel::SetInputFont( const char *font, int size )
{
    if( _inputWindow != NULL )
    {
        _inputWindow->SetInputFont( font, size );
    }
}

/**
* Checks whether a string is a prefix of another string and returns true if so, false if not.
*/
bool SDLPanel::HasStringPrefix( const char *prefix, const char *string )
{
    if ( prefix == NULL )
    {
        return false;
    }

    if ( string == NULL )
    {
        return false;
    }

    for ( ; *prefix; prefix++, string++ )
    {
        if ( tolower(*prefix) != tolower(*string) )
        {
            return false;
        }
    }

    return true;
}

void SDLPanel::SaveAliases()
{
    std::string fileName = "Aliases.txt";
    // TODO: Save alias data.
}

void SDLPanel::LoadAliases()
{
    std::string fileName = "Aliases.txt";
    // TODO: Load alias data.
}

void SDLPanel::OnMouse(SDL_MouseButtonEvent& event)
{
    //syslog(LOG_INFO, "Mouse X: %d, Mouse Y: %d\n", event.x, event.y);
    //syslog(LOG_INFO, "Translated Mouse X: %d, Y: %d", 768-event.x, 1024-event.y);
    //event.x = 768 - event.x;
    //event.y = 1024 - event.y;
    if( event.button == SDL_BUTTON_LEFT)
    {
#ifndef NOHOTKEYS
        if( event.x < ICON_HEIGHT && event.y < ICON_HEIGHT )
        {
            Connect();
            return;
        }
        if( event.x >= ICON_HEIGHT && event.x < ICON_HEIGHT * 2 && event.y < ICON_HEIGHT )
        {
            Disconnect();
            return;
        }
#else
        if( event.x < ((float)_screenWidth * 0.73) && event.x > ((float)_screenWidth * 0.57) && event.y < CONNECT_BAR_SIZE)
        {
            syslog(LOG_INFO, "Connect clicked.\n");
            Connect();
            return;
        }
        if( event.x < ((float)_screenWidth * 0.885) && event.x > ((float)_screenWidth * 0.73) && event.y < CONNECT_BAR_SIZE)
        {
            syslog(LOG_INFO, "Disconnect clicked.\n");
            Disconnect();
            return;
        }
        if( event.x < ((float)_screenWidth * 1.00) && event.x > ((float)_screenWidth * 0.955) && event.y < CONNECT_BAR_SIZE)
        {
            syslog(LOG_INFO, "Keyboard toggle clicked.\n");
            _keyboardMode = !_keyboardMode;
            if( _keyboardMode )
            {
                _screenHeight = HEIGHTWITHKEYBOARD;
            }
            else
            {
                _screenHeight = HEIGHT;
            }
            syslog(LOG_INFO, "Usable screen height is now %d\n", _screenHeight);
            _inputWindow->SetScreen( 0, (_screenHeight - TEXT_BOX_BORDER - TEXT_BOX_HEIGHT ), _screenWidth,
                (TEXT_BOX_HEIGHT + TEXT_BOX_BORDER ));
            _textWindow->SetScreen( 0, CONNECTBAR_HEIGHT, _screenWidth, (_screenHeight - TEXT_BOX_BORDER - TEXT_BOX_HEIGHT - CONNECTBAR_HEIGHT ) );
            _connectBar->SetScreen( 0, 0, _screenWidth, CONNECTBAR_HEIGHT );
#ifdef ARM
            PDL_bool mode = SDL_FALSE;
            if( _keyboardMode )
            {
                mode = SDL_TRUE;
            }
            PDL_Err err = PDL_SetKeyboardState(mode);
            if( err == PDL_ECONNECTION )
            {
                syslog(LOG_INFO, "PDL_ECONNECTION with keyboard state %d: could not communicate with the card.\n", _keyboardMode);
            }
            if( err == PDL_EOTHER )
            {
                syslog(LOG_INFO, "PDL_EOTHER with keyboard state %d: not allowed to bring up the keyboard as a plugin.", _keyboardMode);
            }
            else
            {
               syslog(LOG_INFO, "PDL_SetKeyboardState returned %d\n", err);
            }
#endif
            return;
        }
        if( event.y < CONNECT_BAR_SIZE && event.x > ((float)_screenWidth * 0.40) && event.x < ((float)_screenWidth * 0.57))
        {
            syslog(LOG_INFO, "Edit port clicked.\n");
            _editLocation = EDIT_PORT;
            _inptr = strlen(_portBuffer);
            return;
        }
        if( event.y < CONNECT_BAR_SIZE && event.x > ((float)_screenWidth * 0.885) && event.x < ((float)_screenWidth * 0.955))
        {
            syslog(LOG_INFO, "Newline mode clicked.\n");
            _newlineMode = (++_newlineMode % NUM_NEWLINE_MODES);
            return;
        }
        if( event.y < CONNECT_BAR_SIZE && event.x < ((float)_screenWidth * 0.40))
        {
            syslog(LOG_INFO, "Edit server clicked.\n");
            _editLocation = EDIT_SERVER;
            _inptr = strlen(_serverBuffer);
            SDL_UpdateRect(_screen, 0, 0, 0, 0);
            return;
        }
        // If we're not a button, server name, or port, switch to input mode.
        _editLocation = EDIT_INPUT;
        _inptr = strlen(_inputBuffer);
#endif
    }
}

void SDLPanel::SetDone(bool value)
{
    _done = value;
}

