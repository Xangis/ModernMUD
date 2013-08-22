#include "ConnectBar.h"
#include "Colors.h"

#include <string>
#include <syslog.h>

ConnectBar::ConnectBar( SDL_Surface* pSurface ) : SDLChildWindow( pSurface )
{
#ifdef WIN32
    const char* fontName = "fonts\\FreeMono.ttf";
#else
    const char* fontName = "fonts//FreeMono.ttf";
#endif
    _inputFont = TTF_OpenFont( fontName, 18 );
    if( _inputFont == NULL )
    {
        fprintf(stderr, "Error opening input window font.");
    }
    memset( _textInputBuffer, 0, SERVER_NAME_SIZE );
    memset( _portBuffer, 0, SERVER_NAME_SIZE );
}

ConnectBar::~ConnectBar()
{
}

// Required by base class, do not use.
void ConnectBar::Render()
{
	Render(false, false, 0);
}

// Drawing routine for input window.
void ConnectBar::Render(bool serverActive, bool portActive, int newlineMode)
{
    // Text input location - draw box and print text that has been typed

    SDL_Color srvBkColor;
    SDL_Color portBkColor;
    if( !serverActive )
    {
    	srvBkColor.r = 208;
	srvBkColor.g = 208;
	srvBkColor.b = 208;
    }
    else
    {
    	srvBkColor.r = 255;
	srvBkColor.g = 255;
	srvBkColor.b = 255;
    }
    if( !portActive )
    {
    	portBkColor.r = 208;
	portBkColor.g = 208;
	portBkColor.b = 208;
    }
    else
    {
    	portBkColor.r = 255;
	portBkColor.g = 255;
	portBkColor.b = 255;
    }

    // Draw border first.
    SDL_Rect rect;
    rect.x = _xbegin;
    rect.y = _ybegin;
    rect.w = _width;
    rect.h = CONNECT_BAR_SIZE;
    SDL_FillRect( _sdlSurface, &rect, SDL_MapRGB( _sdlSurface->format, 128, 128, 128 ) );

    // Now draw white background for server
    rect.x = _xbegin + 4;
    rect.y = _ybegin + CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.4) - 4;
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*2);
    SDL_FillRect ( _sdlSurface, &rect, SDL_MapRGB( _sdlSurface->format, srvBkColor.r, srvBkColor.g, srvBkColor.b ));

    // Draw white background for port.
    rect.x = ((float)_width * 0.4);
    rect.w = ((float)_width * 0.16);
    SDL_FillRect ( _sdlSurface, &rect, SDL_MapRGB( _sdlSurface->format, portBkColor.r, portBkColor.g, portBkColor.b ));

    // Draw Port Separator
    rect.x = ((float)_width * 0.4);
    rect.y = _ybegin;
    rect.w = ((float)_width * 0.01);
    rect.h = CONNECT_BAR_SIZE;
    SDL_FillRect( _sdlSurface, &rect, SDL_MapRGB(_sdlSurface->format, 128, 128, 128 ));

    // Draw Button 1
    rect.x = ((float)_width * 0.57);
    rect.y = CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.15);
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*2);
    SDL_FillRect( _sdlSurface, &rect, SDL_MapRGB(_sdlSurface->format, 200, 200, 200 ));

    // Draw Button 2
    rect.x = ((float)_width * 0.73);
    rect.y = CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.15);
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*2);
    SDL_FillRect( _sdlSurface, &rect, SDL_MapRGB(_sdlSurface->format, 200, 200, 200 ));

    // Draw Button 3 - Newline Mode
    rect.x = ((float)_width * 0.885);
    rect.y = CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.065);
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*2);
    SDL_FillRect( _sdlSurface, &rect, SDL_MapRGB(_sdlSurface->format, 48, 48, 255 ));

    // Draw Button 3 - Enlarge/shrink
    rect.x = ((float)_width * 0.955);
    rect.y = CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.04);
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*2);
    SDL_FillRect( _sdlSurface, &rect, SDL_MapRGB(_sdlSurface->format, 48, 48, 255 ));

    int result;
    if( strlen(_textInputBuffer) > 0 )
    {
      // Draw server name.
      rect.x = _xbegin + 6;
      rect.y = _ybegin + CONNECT_BAR_BORDER;
      rect.w = ((float)_width * 0.4) - 6;
      rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*4);
      SDL_Surface* textInputSurface = TTF_RenderText_Shaded( _inputFont, _textInputBuffer, clrBlack, srvBkColor );
      result = SDL_BlitSurface( textInputSurface, NULL, _sdlSurface, &rect );
      if( result == -1 )
      {
          syslog(LOG_ERR, "SDL_BlitSurface failed rendering connect bar.\n");
      }
      SDL_FreeSurface( textInputSurface );
    }

    if( strlen(_portBuffer) > 0 )
    {
      // Draw port.
      rect.x = ((float)_width * 0.41) + 2;
      rect.y = _ybegin + CONNECT_BAR_BORDER;
      rect.w = ((float)_width * 0.16) - 2;
      rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*4);
      SDL_Surface* portSurface = TTF_RenderText_Shaded( _inputFont, _portBuffer, clrBlack, portBkColor );
      result = SDL_BlitSurface( portSurface, NULL, _sdlSurface, &rect );
      if( result == -1 )
      {
          syslog(LOG_ERR, "SDL_BlitSurface failed rendering connect bar.\n");
      }
      SDL_FreeSurface( portSurface );
    }

    // Draw connect text.
    rect.x = ((float)_width * 0.59);
    rect.y = _ybegin + CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.14);
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*4);
    SDL_Surface* btn1Surface = TTF_RenderText_Shaded( _inputFont, "Connect", clrBlack, clrDkwhite );
    result = SDL_BlitSurface( btn1Surface, NULL, _sdlSurface, &rect );
    if( result == -1 )
    {
        syslog(LOG_ERR, "SDL_BlitSurface failed rendering connect bar.\n");
    }
    SDL_FreeSurface( btn1Surface );

    // Draw disconnect text.
    rect.x = ((float)_width * 0.73) + 2;
    rect.y = _ybegin + CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.14);
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*4);
    SDL_Surface* btn2Surface = TTF_RenderText_Shaded( _inputFont, "Disconnect", clrBlack, clrDkwhite );
    result = SDL_BlitSurface( btn2Surface, NULL, _sdlSurface, &rect );
    if( result == -1 )
    {
        syslog(LOG_ERR, "SDL_BlitSurface failed rendering connect bar.\n");
    }
    SDL_FreeSurface( btn2Surface );

    // Draw newline mode
    rect.x = ((float)_width * 0.8875);
    rect.y = _ybegin + CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.05);
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*4);
    SDL_Surface* btn3Surface = NULL;
    switch( newlineMode )
    {
        case 0:
            btn3Surface = TTF_RenderText_Shaded( _inputFont, " LF", clrBlack, clrBlue );
            break;
        case 1:
            btn3Surface = TTF_RenderText_Shaded( _inputFont, " CR", clrBlack, clrBlue );
            break;
        case 2:
            btn3Surface = TTF_RenderText_Shaded( _inputFont, "CRLF", clrBlack, clrBlue );
            break;
        case 3:
            btn3Surface = TTF_RenderText_Shaded( _inputFont, "LFCR", clrBlack, clrBlue );
            break;
    }
    //btn3Surface = TTF_RenderText_Shaded( _inputFont, " LF", clrBlack, clrBlue );
    result = SDL_BlitSurface( btn3Surface, NULL, _sdlSurface, &rect );
    if( result == -1 )
    {
        syslog(LOG_ERR, "SDL_BlitSurface failed rendering connect bar.\n");
    }
    SDL_FreeSurface( btn3Surface );

    // Draw enlarge/shrink button
    rect.x = ((float)_width * 0.9675);
    rect.y = _ybegin + CONNECT_BAR_BORDER;
    rect.w = ((float)_width * 0.03);
    rect.h = CONNECT_BAR_SIZE - (CONNECT_BAR_BORDER*4);
    SDL_Surface* btn4Surface = TTF_RenderText_Shaded( _inputFont, "+", clrBlack, clrBlue );
    result = SDL_BlitSurface( btn4Surface, NULL, _sdlSurface, &rect );
    if( result == -1 )
    {
        syslog(LOG_ERR, "SDL_BlitSurface failed rendering connect bar.\n");
    }
    SDL_FreeSurface( btn4Surface );
}

void ConnectBar::SetInputFont( const char* font, int size )
{
    // TODO: Make sure closing the current font doesn't destroy the rendering thread.
    TTF_CloseFont( _inputFont );

    std::string fontstring = "fonts\\";
    fontstring = fontstring.append( font );
    _inputFont = TTF_OpenFont( fontstring.c_str(), size );

    if( _inputFont == NULL )
    {
        _inputFont = TTF_OpenFont( "fonts\\FreeMono.ttf", 18 );
        if( _inputFont == NULL )
        {
            fprintf(stderr, "Error opening input window font.");
        }
    }
}

void ConnectBar::SetInputBuffer( char *buffer )
{
    memcpy( _textInputBuffer, buffer, SERVER_NAME_SIZE );
}

void ConnectBar::SetPortBuffer( char *buffer )
{
    memcpy( _portBuffer, buffer, SERVER_NAME_SIZE );
}
