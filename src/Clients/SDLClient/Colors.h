#pragma once

#include "SDL.h"

#define COLOR_BLACK   0
#define COLOR_RED     1
#define COLOR_GREEN   2
#define COLOR_YELLOW  3
#define COLOR_BLUE    4
#define COLOR_MAGENTA 5
#define COLOR_CYAN    6
#define COLOR_WHITE   7

// Set color for received text
// colors should be set by ansi parser
extern SDL_Color clrRed;
extern SDL_Color clrGreen;
extern SDL_Color clrBlue;
extern SDL_Color clrWhite;
extern SDL_Color clrGray;
extern SDL_Color clrBlack;
extern SDL_Color clrBlackText;
extern SDL_Color clrYellow;
extern SDL_Color clrPurple;
extern SDL_Color clrCyan;
extern SDL_Color clrDkred;
extern SDL_Color clrDkgreen;
extern SDL_Color clrDkblue;
extern SDL_Color clrDkwhite;
extern SDL_Color clrDkgray;
extern SDL_Color clrDkyellow;  // Orange
extern SDL_Color clrDkpurple;
extern SDL_Color clrDkcyan;
extern SDL_Color clrOrange; // Same as dark yellow so far
