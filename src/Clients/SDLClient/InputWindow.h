#ifndef _INPUTWINDOW_H_
#define _INPUTWINDOW_H_

#include "SDLChildWindow.h"
#include "SDL_ttf.h"

#define TEXT_BOX_HEIGHT 32
#define TEXT_BOX_BORDER 2
#define INPUT_BUFFER_SIZE 600

class InputWindow : public SDLChildWindow
{
public:
	InputWindow( SDL_Surface* pSurface );
	~InputWindow();
	void Render();
        void Render(bool focused);
	void SetInputFont( const char* font, int size );
	void SetInputBuffer( char* buffer );
private:
	TTF_Font * _inputFont;
	char _textInputBuffer[INPUT_BUFFER_SIZE];
};

#endif
