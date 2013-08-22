#ifndef _CONNECTBAR_H_
#define _CONNECTBAR_H_

#include "SDLChildWindow.h"
#include "SDL_ttf.h"

#define CONNECT_BAR_BORDER 2
#define CONNECT_BAR_SIZE 32
#define SERVER_NAME_SIZE 600

class ConnectBar : public SDLChildWindow
{
public:
    ConnectBar( SDL_Surface* pSurface );
    ~ConnectBar();
    void Render();
    void Render(bool serverActive, bool PortActive, int newlineMode);
    void SetInputFont( const char* font, int size );
    void SetInputBuffer(char* buffer);
    void SetPortBuffer(char* buffer);
    const char* GetServer();
    int GetPort();
private:
    TTF_Font * _inputFont;
    char _textInputBuffer[SERVER_NAME_SIZE];
    char _portBuffer[SERVER_NAME_SIZE];
};

#endif
