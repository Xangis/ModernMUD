#ifndef _COLORS_H_
#define _COLORS_H_

#include "wx/wx.h"

#define COLOR_BLACK   0
#define COLOR_RED     1
#define COLOR_GREEN   2
#define COLOR_YELLOW  3
#define COLOR_BLUE    4
#define COLOR_MAGENTA 5
#define COLOR_CYAN    6
#define COLOR_WHITE   7

// Set color for received text
// Colors should be set by ANSI parser
extern wxColour clrRed;
extern wxColour clrGreen;
extern wxColour clrBlue;
extern wxColour clrWhite;
extern wxColour clrGray;
extern wxColour clrBlack;
extern wxColour clrBlackText;
extern wxColour clrYellow;
extern wxColour clrPurple;
extern wxColour clrCyan;
extern wxColour clrDkred;
extern wxColour clrDkgreen;
extern wxColour clrDkblue;
extern wxColour clrDkwhite;
extern wxColour clrDkgray;
extern wxColour clrDkyellow;  // Orange
extern wxColour clrDkpurple;
extern wxColour clrDkcyan;
extern wxColour clrOrange; // Same as dark yellow

#endif
