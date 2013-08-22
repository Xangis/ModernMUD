#include "SDLPanel.h" 
#ifdef ARM
#include "PDL.h"
#endif
#include <iostream>
#include <syslog.h>

using namespace std;

SDLPanel* panel = NULL;

void ProcessSDLEvent(SDL_Event* event)
{
    switch(event->type)
    {
        case SDL_KEYDOWN:
            switch( event->key.keysym.sym )
            {
#ifdef ARM
                case PDLK_GESTURE_BACK: // Includes escape.
                    if( PDL_GetPDKVersion() >= 200 )
                    {
                        PDL_Minimize(); // Minimize to a card.
                    }
                    break;
                case 24: // This was 230 in webOS 3.0.0 and earlier, but was changed to 24.
                    syslog(LOG_INFO, "Hiding keyboard.\n");
                    PDL_SetKeyboardState(SDL_FALSE);
                    panel->_keyboardMode = false;
                    break;
#endif
                default:
                    //cout << "SDL_KEYDOWN" << endl;
                    panel->OnKey(event->key);
                    break;
            }
            break;
        case SDL_MOUSEBUTTONDOWN:
            //cout << "SDL_MOUSEBUTTONDOWN" << endl;
            panel->OnMouse(event->button);
            break;
        case SDL_ACTIVEEVENT:
            if( event->active.state == SDL_APPACTIVE)
            {
                // No idea what this does.
                bool paused = !event->active.gain;
            }
            break;
        case SDL_QUIT:
            cout << "SDL_QUIT" << endl;
            panel->SetDone(true);
            break;
        default:
            break;
    }
}

int main(int argc, char** argv)
{
    // Create the SDLPanel.  SDLPanel calls SDL_Init().
    panel = new SDLPanel();
#ifdef ARM
    PDL_Init(0);
    PDL_Err err = PDL_SetKeyboardState(SDL_TRUE);
#endif
    bool gotEvent = false;
    SDL_Event event;
    while( !panel->IsDone() )
    {
        //cout << "Rendering." << endl;
        panel->Render();
        panel->ProcessNetwork();
        gotEvent = SDL_PollEvent(&event);
        while(gotEvent)
        {
            ProcessSDLEvent(&event);
            gotEvent = SDL_PollEvent(&event);
        }
#ifdef WIN32
        Sleep(20);
#else
        usleep(20000);
#endif
    }
    // ~SDLPanel calls SDL_Quit();
    return 0;
}
