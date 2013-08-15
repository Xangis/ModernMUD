#ifndef _STRINGENTRY_H_
#define _STRINGENTRY_H_

/**
* Represents a string entry to be displayed on the output window. Contains the text
* to be rendered, its color, and whether it should contain a newline.
*/
class StringEntry
{
public:
	StringEntry(wxString text, bool newline, wxColour foregroundColour){ Text = text.c_str(); Newline = newline; ForegroundColour = foregroundColour; };
	wxString Text;
	bool Newline;
	wxColour ForegroundColour;
};

#endif
