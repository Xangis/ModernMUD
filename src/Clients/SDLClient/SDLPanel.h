#pragma once
/*******************************************************************************
// Headers
*******************************************************************************/

#include <iostream>
#include <fstream>

#include "SDL.h"
#include "SDL_image.h"
#ifdef WIN32
#include "winsock.h"
#else
#include <sys/socket.h>
#include <unistd.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <resolv.h>
#include <sys/time.h>
#include <sys/types.h>
#include <sys/signal.h>
#include <errno.h>
#include <time.h>
#include <fcntl.h>
#include <netdb.h>
#include <signal.h>
#include <sys/stat.h>
#endif
#include "InputWindow.h"
#include "TextWindow.h"
#ifdef NOHOTKEYS
#include "ConnectBar.h"
#endif
#include "stringentry.h"
#include "modes.h"
#include <list>
#include <string>
#include <map>

//#define HOSTNAME "basternae.org"
//#define PORT 4502
#define HOSTNAME "example.org"
#define PORT 23
#define RECEIVE_BUFFER_SIZE 1500
#define INPUT_BUFFER_SIZE 600
#define ICON_HEIGHT 64
#define CONNECTBAR_HEIGHT 32

/*******************************************************************************
// SDLPanel Class
*******************************************************************************/

#define EDIT_INPUT 1
#define EDIT_SERVER 2
#define EDIT_PORT 3

#define NEWLINE_LF 0
#define NEWLINE_CR 1
#define NEWLINE_CRLF 2
#define NEWLINE_LFCR 3
#define NUM_NEWLINE_MODES 4

class SDLPanel
{
public:
    SDLPanel();
    ~SDLPanel();
    void OnSize(SDL_ResizeEvent& event);
    void OnMouse(SDL_MouseButtonEvent& event);
    void SetDisplayFont( const char* font, int size );
    void SetInputFont( const char* font, int size );
    void Connect();
    void Disconnect();
    void OnKey(SDL_KeyboardEvent& event);
    void RemoveANSICodes( char* string );
    void RemoveNewlines( char* string );
    void RemoveUnprintableASCIIChars( char* string );
    void RemoveNewlinesAndSpaces( char* string );
    void ProcessANSICodes( char* string );
    void ChangeLogMode( int mode );
    void ChangeColorMode( int mode );
    void FlushText( const char * text, int length, bool newline );
    SDL_Color* GetColorFromAnsiCode( char* text );
    static bool HasStringPrefix( const char *prefix, const char *string );
    void LoadAliases();
    void SaveAliases();
    bool IsDone();
    void SetDone(bool value);
    void Render();
    void ProcessNetwork();
    bool _keyboardMode;
private:
    SDL_Surface* _screen;
#ifndef NOHOTKEYS
    SDL_Surface* _hotkeyButton[12];
#else
    ConnectBar* _connectBar;
#endif
    int _newlineMode;
    bool _done;
    int _editLocation;
    void CreateScreen();
    unsigned short _port;
    struct sockaddr_in _servaddr;
    unsigned int _connection;
    bool _connected;
    // True if a closing XML tag hasn't been found for an opening one.
    bool _unclosedTag;
    bool _mccpEnabled; // Is MUD Client Compression Protocol enabled?
    int _colorMode;
    int _logMode;
    int _parseMode;
    short _screenWidth;
    short _screenHeight;
    int _rmask;
    int _gmask;
    int _bmask;
    int _amask;
    std::ofstream _htmlFile;
    InputWindow* _inputWindow;
    TextWindow* _textWindow;
    char _inputBuffer[INPUT_BUFFER_SIZE];
    char _serverBuffer[INPUT_BUFFER_SIZE];
    char _portBuffer[INPUT_BUFFER_SIZE];
    char _inputBufferPrevious[INPUT_BUFFER_SIZE];
    int _inptr;
    SDL_Color * _foregroundColor;
    SDL_Color * _backgroundColor;
    // Figure out what data type to make this - it should be a struct of some sort
    // so we know what color to make the text.  Newlines and intra-line text changes
    // could be difficult.
    std::map<std::string, std::string> _aliases;
};
