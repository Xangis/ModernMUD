#ifndef _WXOWNERDRAWSTATICBITMAP_H_
#define _WXOWNERDRAWSTATICBITMAP_H_

#include "wx/wx.h"

/**
* Owner-drawn static bitmap that only draws a portion of the bitmap based
* on the draw width. Used for fixed-sized bitmaps that we only draw part of.
* In this case, it's used for the health/mana/move bars, but could just as
* easily be used for any sort of progress bar
*/
class wxOwnerDrawStaticBitmap : public wxStaticBitmap
{
    DECLARE_EVENT_TABLE()
public:
	wxOwnerDrawStaticBitmap( wxWindow* parent, int id, wxBitmap& bitmap, wxPoint pos, wxSize size ) : wxStaticBitmap(parent, id, bitmap, pos, size)
	{
		_drawWidth = 0;
		_bitmap = bitmap;
	}
	wxOwnerDrawStaticBitmap();
	~wxOwnerDrawStaticBitmap();
	void OnPaint(wxPaintEvent &event);
	int _drawWidth; // How much of the bitmap width to draw in percent (for partial drawing, i.e. life meters).
	wxBitmap _bitmap;
};

#endif
