#ifndef _SDLCHILDWINDOW_H_
#define _SDLCHILDWINDOW_H_

#include "SDL.h"

/* 
	Represents a child window of an SDL surface.  Keeps track of the rendering coordinates
	(sub-area).  Derive from this to build specific rendering areas on a window.
*/
class SDLChildWindow
{
public:
	SDLChildWindow( SDL_Surface* pSurface );
	~SDLChildWindow();
	void SetScreen( short x, short y, short width, short height );
	void SetSurface( SDL_Surface* pSurface );
	virtual void Render() = 0;
protected:
	short _xbegin;
	short _ybegin;
	short _width;
	short _height;
	SDL_Surface* _sdlSurface;
};

#endif
