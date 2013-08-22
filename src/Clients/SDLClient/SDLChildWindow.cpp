#include "SDLChildWindow.h"

SDLChildWindow::SDLChildWindow( SDL_Surface* pSurface )
{
	_xbegin = 0;
	_ybegin = 0;
	_width = 0;
	_height = 0;
	_sdlSurface = pSurface;
}

SDLChildWindow::~SDLChildWindow()
{
}

void SDLChildWindow::SetScreen( short x, short y, short width, short height )
{
	_xbegin = x;
	_ybegin = y;
	_width = width;
	_height = height;
}

void SDLChildWindow::SetSurface( SDL_Surface* pSurface )
{
	_sdlSurface = pSurface;
}
