#ifndef _TEXTWINDOW_H_
#define _TEXTWINDOW_H_

#include "SDLChildWindow.h"
#include "SDL_ttf.h"
#include "stringentry.h"
#include <list>

class TextWindow : public SDLChildWindow
{
public:
    TextWindow( SDL_Surface* pSurface );
    ~TextWindow();
    virtual void Render();
    void AddStringEntry( StringEntry *entry );
    void AddNewline();
    void SetDisplayFont( const char *font, int size );
private:
    TTF_Font * _currentFont;
    std::list<std::list<StringEntry *> > _screenBuffer;
    std::list<StringEntry *> *_currentLine;
};

#endif
