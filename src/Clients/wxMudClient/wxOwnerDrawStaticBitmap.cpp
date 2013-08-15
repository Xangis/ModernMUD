#include "wxOwnerDrawStaticBitmap.h"

BEGIN_EVENT_TABLE( wxOwnerDrawStaticBitmap, wxStaticBitmap )
	EVT_PAINT(wxOwnerDrawStaticBitmap::OnPaint)
END_EVENT_TABLE()

void wxOwnerDrawStaticBitmap::OnPaint(wxPaintEvent& event)
{
	wxPaintDC dc(this);
	if( _drawWidth > 0 )
	{
		dc.DrawBitmap(_bitmap.GetSubBitmap(wxRect(0, 0, (_drawWidth * dc.GetSize().x / 100), _bitmap.GetHeight())), 0, 0);
	}
	if( _drawWidth < 100 )
	{
		dc.SetBrush(wxBrush(GetBackgroundColour()));
		dc.SetPen(wxPen(GetBackgroundColour()));
		dc.DrawRectangle(((_drawWidth * dc.GetSize().x) / 100), 0, (((100 - _drawWidth) * dc.GetSize().x) / 100), _bitmap.GetHeight());
	}
}

wxOwnerDrawStaticBitmap::wxOwnerDrawStaticBitmap() : wxStaticBitmap()
{
}

wxOwnerDrawStaticBitmap::~wxOwnerDrawStaticBitmap()
{
}
