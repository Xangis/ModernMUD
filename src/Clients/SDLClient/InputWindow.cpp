#include "InputWindow.h"
#include "Colors.h"

#include <string>
#include <syslog.h>

InputWindow::InputWindow( SDL_Surface* pSurface ) : SDLChildWindow( pSurface )
{
#ifdef WIN32
    const char* fontName = "fonts\\FreeMono.ttf";
#else
    const char* fontName = "fonts//FreeMono.ttf";
#endif
    _inputFont = TTF_OpenFont( fontName, 20 );
    if( _inputFont == NULL )
    {
        fprintf(stderr, "Error opening input window font.");
    }
    memset( _textInputBuffer, 0, INPUT_BUFFER_SIZE );
}

InputWindow::~InputWindow()
{
}

void InputWindow::Render()
{
}

// Drawing routine for input window.
void InputWindow::Render(bool focused)
{
    // Text input location - draw box and print text that has been typed

    // Draw border first.
    SDL_Rect rect;
    rect.x = _xbegin;
    rect.y = _ybegin;
    rect.w = _width;
    rect.h = TEXT_BOX_BORDER;

    // Now draw white background.
    SDL_FillRect( _sdlSurface, &rect, SDL_MapRGB( _sdlSurface->format, 128, 128, 128 ) );
    rect.x = _xbegin;
    rect.y = _ybegin + TEXT_BOX_BORDER;
    rect.w = _width;
    rect.h = TEXT_BOX_HEIGHT;

    if( focused )
    {
        SDL_FillRect ( _sdlSurface, &rect, SDL_MapRGB( _sdlSurface->format, 255, 255, 255 ) );
    }
    else
    {
        SDL_FillRect ( _sdlSurface, &rect, SDL_MapRGB( _sdlSurface->format, 208, 208, 208 ) );
    }

    // Don't try to draw if there is no text.
    if( strlen(_textInputBuffer) < 1 ) return;

    // Draw text input line.
    rect.x = _xbegin;
    rect.y = _ybegin + TEXT_BOX_BORDER;
    rect.w = _width;
    rect.h = 28;
    SDL_Surface* textInputSurface = TTF_RenderText_Shaded( _inputFont, _textInputBuffer, clrBlack, clrWhite );
    int result = SDL_BlitSurface( textInputSurface, NULL, _sdlSurface, &rect );
    if( result == -1 )
    {
        syslog(LOG_ERR, "SDL_BlitSurface failed rendering input window, X: %d, Y: %d, Width: %d, Height: %d.\n",
            rect.x, rect.y, rect.w, rect.h);
    }
    SDL_FreeSurface( textInputSurface );
}

void InputWindow::SetInputFont( const char* font, int size )
{
    // TODO: Make sure closing the current font doesn't destroy the rendering thread.
    TTF_CloseFont( _inputFont );

    std::string fontstring = "fonts\\";
    fontstring = fontstring.append( font );
    _inputFont = TTF_OpenFont( fontstring.c_str(), size );

    if( _inputFont == NULL )
    {
        _inputFont = TTF_OpenFont( "fonts\\FreeMono.ttf", 20 );
        if( _inputFont == NULL )
        {
            fprintf(stderr, "Error opening input window font.");
        }
    }
}

void InputWindow::SetInputBuffer( char *buffer )
{
    memcpy( _textInputBuffer, buffer, INPUT_BUFFER_SIZE );
}
