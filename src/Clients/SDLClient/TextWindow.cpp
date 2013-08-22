#include "TextWindow.h"
#include "Colors.h"

#include <string>
#include <syslog.h>

TextWindow::TextWindow( SDL_Surface *pSurface ) : SDLChildWindow( pSurface )
{
    _currentLine = new std::list<StringEntry * >;
#ifdef WIN32
    const char* fontName = "fonts\\FreeMono.ttf";
#else
    const char* fontName = "fonts//FreeMono.ttf";
#endif
    _currentFont = TTF_OpenFont( fontName, 16 );
    if( _currentFont == NULL )
    {
        fprintf(stderr, "Error '%s' while opening text window font %s.", TTF_GetError(), fontName );
    }
}

TextWindow::~TextWindow()
{
    // Delete all of our stored strings.
    std::list<std::list<StringEntry *> >::reverse_iterator iter;
    std::list<StringEntry *>::iterator inner_iter;
    for( iter = _screenBuffer.rbegin(); iter != _screenBuffer.rend(); iter++ )
    {
    //  for( inner_iter = (*iter).begin(); inner_iter = (*iter).end(); inner_iter++ )
        {
        // This render is called on a newline.
//          delete (*inner_iter)->string;
//          delete *inner_iter;
        }
        //delete (*iter);
    }
}

// Drawing routine for screen output.
void TextWindow::Render()
{
    SDL_Rect rect;
    SDL_Surface *textSurface;
    int w, h;

    // Blank the screen area
    rect.x = _xbegin;
    rect.y = _ybegin;
    rect.w = _width;
    rect.h = _height;
    SDL_FillRect(_sdlSurface, &rect, 0);

    // Start at the bottom of the screen and render up until it's full.
    rect.x = _xbegin;
    rect.y = _height + _ybegin - TTF_FontLineSkip( _currentFont );

    std::list<std::list<StringEntry *> >::reverse_iterator iter;
    std::list<StringEntry *>::iterator inner_iter;

    bool noNewline = false;
    // Render the current non-newlined line.
    for( inner_iter = (*_currentLine).begin(); inner_iter != (*_currentLine).end(); inner_iter++ )
    {
        // This render is called on a newline.
        TTF_SizeText( _currentFont, (*inner_iter)->string, &w, &h );
        rect.w = w;
        rect.h = h;
        // Check x and y to see if they are out of range.
        textSurface = TTF_RenderText_Shaded( _currentFont, (*inner_iter)->string , (*inner_iter)->color, clrBlack );
        int result = SDL_BlitSurface( textSurface, NULL, _sdlSurface, &rect );
        if( result == -1 )
        {
            syslog(LOG_ERR, "SDL_BlitSurface failed rendering text window.\n");
        }
        SDL_FreeSurface( textSurface );
        // There will be no newlines here.
        rect.x = rect.x + rect.w;
        noNewline = true;
    }
    if( noNewline )
    {
        rect.y -= TTF_FontLineSkip( _currentFont );
        rect.x = 0;
        noNewline = false;
    }
    for( iter = _screenBuffer.rbegin(); iter != _screenBuffer.rend(); iter++ )
    {
        // Render the scrolled text.
        for( inner_iter = (*iter).begin(); inner_iter != (*iter).end(); inner_iter++ )
        {
            // This render is called on a newline.
            TTF_SizeText( _currentFont, (*inner_iter)->string, &w, &h );
            rect.w = w;
            rect.h = h;
            // Check x and y to see if they are out of range.
            textSurface = TTF_RenderText_Shaded( _currentFont, (*inner_iter)->string , (*inner_iter)->color, clrBlack );
            int result = SDL_BlitSurface( textSurface, NULL, _sdlSurface, &rect );
            if( result == -1 )
            {
                syslog(LOG_ERR, "SDL_BlitSurface failed rendering text window.\n");
            }
            SDL_FreeSurface( textSurface );
            // Newline, draw the line and then go to the left.
            if( (*inner_iter)->newline )
            {
                // Advance a line as long as we're not going to go below _ybegin
                if( rect.y > (_ybegin + TTF_FontLineSkip( _currentFont ) ))
                {
                    rect.y -= TTF_FontLineSkip( _currentFont );
                    rect.x = 0;
                }
                else
                {
                    // Text buffer is full - stop drawing.
                    return;
                }
            }
            // No newline - draw farther to the right.  The problem with this is that we're iterating
            // backward, so the previous text will show up to the RIGHT of the later text.
            else
            {
                // Move start position farther to the right.  If we pass the edge,
                // perform a rudimentary line wrap.
                if( rect.x < _width )
                {
                    rect.x = rect.x + rect.w;
                }
                else
                {
                    // Advance a line as long as we're not going to go below _ybegin on the next draw.
                    if( rect.y > (_ybegin + (TTF_FontLineSkip( _currentFont ) * 2 )))
                    {
                        rect.y -= TTF_FontLineSkip( _currentFont );
                        rect.x = 0;
                    }
                    else
                    {
                        // Text buffer is full - stop drawing.
                        return;
                    }
                }
            }
        }
    }
}

void TextWindow::AddStringEntry( StringEntry *entry )
{
    //if( !entry ) return;

    //// if entry->newline, push back inner list and outer list.
    //// if !entry->newline just push back inner list.
    if( !entry->newline )
    {
        (*_currentLine).push_back( entry );
    }
    else
    {
        (*_currentLine).push_back( entry );
        _screenBuffer.push_back( *_currentLine );
        _currentLine = new std::list<StringEntry *>;
    }

    return;
}

void TextWindow::AddNewline()
{
    // If we have a current non-terminated newline, terminate it.  Otherwise do nothing.
    if( (*_currentLine).size() > 0 )
    {
        StringEntry* entry = (*_currentLine).back();
        entry->newline = true;
        _screenBuffer.push_back( *_currentLine );
        _currentLine = new std::list<StringEntry *>;
    }
}

void TextWindow::SetDisplayFont( const char *font, int size )
{
    // TODO: Make sure closing the current font doesn't destroy the rendering thread.
    TTF_CloseFont( _currentFont );

    std::string fontstring = "fonts\\";
    fontstring = fontstring.append(font);
    _currentFont = TTF_OpenFont( fontstring.c_str(), size );

    if( _currentFont == NULL )
    {
        _currentFont = TTF_OpenFont( "fonts\\FreeMono.ttf", 16 );
        if( _currentFont == NULL )
        {
            fprintf(stderr, "Error opening text window font.");
        }
    }
}
