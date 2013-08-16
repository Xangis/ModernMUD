#include "wx/wx.h"
#include "wx/stdpaths.h"
#include "EquipmentWindow.h"
#include "wxMudClientDlg.h"

IMPLEMENT_DYNAMIC_CLASS( EquipmentWindow, wxDialog )

BEGIN_EVENT_TABLE( EquipmentWindow, wxDialog )
EVT_PAINT(EquipmentWindow::OnPaint)
EVT_ERASE_BACKGROUND(EquipmentWindow::OnEraseBackground)
END_EVENT_TABLE()

EquipmentWindow::EquipmentWindow()
{
}

EquipmentWindow::EquipmentWindow(wxWindow* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    Create(parent, id, caption, pos, size, style);
}

bool EquipmentWindow::Create(wxWindow* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    SetExtraStyle(GetExtraStyle()|wxWS_EX_BLOCK_EVENTS);
    wxDialog::Create( parent, id, caption, pos, size, style );
#ifdef linux
    _background = new wxBitmap( _("tiles//armor1.png"), wxBITMAP_TYPE_PNG ) ;
#else
#ifdef __WXMAC__
    wxString p = wxStandardPaths::Get().GetResourcesDir();
    _background = new wxBitmap( p + _("//tiles//armor1.png"), wxBITMAP_TYPE_PNG ) ;
#else
	_background = new wxBitmap( _(".\\tiles\\armor1.png"), wxBITMAP_TYPE_PNG ) ;
#endif
#endif
	if( _background == NULL || !_background->Ok() )
	{
		wxMessageBox(_("Unable to load equipment window bitmap.  Please make sure armor1.png is in the 'tiles' directory."));
	}
    CreateControls();
    GetSizer()->Fit(this);
    GetSizer()->SetSizeHints(this);
    Centre();
    wxTopLevelWindow::SetTransparent(240);
    return true;
}

void EquipmentWindow::CreateControls()
{
    EquipmentWindow* itemDialog1 = this;
    this->SetForegroundColour( *wxWHITE );
    this->SetBackgroundColour( *wxBLACK );

    wxFlexGridSizer* itemFlexGridSizer2 = new wxFlexGridSizer(NUMITEMS, 1, 0, 0);
    itemDialog1->SetSizer(itemFlexGridSizer2);

	int x;
	for( x = 0; x < NUMITEMS; x++ )
	{
        // Since we are drawing everything ourselves, we use spacers to make sure the window fits
        // our text and bitmap.
        itemFlexGridSizer2->Add(256,TEXTPIXELSIZE);
        _items[x] = new wxString(_("(none)"));
	}
}

void EquipmentWindow::ProcessEquipmentData( wxString text )
{
    if( text.Length() < 1 )
    {
        return;
    }

    // Clear everything first.
	for( int x = 0; x < NUMITEMS; x++ )
	{
        if( _items[x] != NULL )
        {
            delete _items[x];
            _items[x] = new wxString(_("(none)"));
        }
	}

	int line = 0;
	wxString outText;
	while( 1 )
	{
        int nextpos = text.Find(_(":"));
		if( nextpos == wxNOT_FOUND || nextpos == 0 )
        {
			break;
        }
		outText = text.SubString(0, (nextpos-1));
		AddEquipmentLine(outText); // Could crash if we didn't allocate enough lines.
		text = text.SubString((nextpos+1), (text.Length() - 1));
		line++;
	}
  	Refresh();
}

void EquipmentWindow::AddEquipmentLine(wxString text)
{
	if( text.Contains(_("You are using:")))
	{
		return;
	}

    int pos = text.Find(wxChar(','));
    if( pos == wxNOT_FOUND || pos == 0 )
    {
        return;
    }
    int num = atoi(text.SubString(0, (pos-1)).ToAscii());
    text = text.SubString((pos+1), (text.Length() - 1));

	if( num == 6 )
	{
        *_items[0] = text;
        return;
	}
	if( num == 17 )
	{
		*_items[1] = text;
        return;
	}
	if( num == 19 )
	{
		*_items[2] = text;
        return;
	}
	if( num == 20 )
	{
		*_items[3] = text;
        return;
	}
	if( num == 18 )
	{
		*_items[4] = text;
        return;
	}
	if( num == 3 )
	{
		*_items[5] = text;
        return;
	}
	if( num == 4 )
	{
		*_items[6] = text;
        return;
	}
	if( num == 5 )
	{
		*_items[7] = text;
        return;
	}
	if( num == 11 )
	{
		*_items[8] = text;
        return;
	}
	if( num == 10 )
	{
		*_items[9] = text;
        return;
	}
	if( num == 13 )
	{
		*_items[10] = text;
        return;
	}
	if( num == 14 )
	{
		*_items[11] = text;
        return;
	}
	if( num == 9 )
	{
		*_items[12] = text;
        return;
	}
	if( num == 1 )
	{
		*_items[13] = text;
        return;
	}
	if( num == 2 )
	{
		*_items[14] = text;
        return;
	}
	if( num == 15 )
	{
		*_items[15] = text;
        return;
	}
	if( num == 16 )
	{
		*_items[16] = text;
        return;
	}
	if( num == 12 )
	{
		*_items[17] = text;
        return;
	}
	if( num == 7 )
	{
		*_items[18] = text;
        return;
	}
	if( num == 8 )
	{
		*_items[19] = text;
        return;
	}
    // ITEMS NOT YET PROCESSED
	//
    //"&+y<worn as badge>       &n", // 23
    //"&+y<worn on back>        &n", // 24
    //"&+y<attached to belt>    &n", // 25
    //"&+y<attached to belt>    &n", // 26
    //"&+y<attached to belt>    &n", // 27
    //"&+y<worn as quiver>      &n", // 29
    //"&+y<worn on tail>        &n", // 30
    //"&+y<worn on horse body>  &n", // 31
    //"&+y<worn on horns>       &n", // 32
    //"&+y<worn in nose>        &n", // 33
    //"&+y<third hand>          &n", // 34
    //"&+y<fourth hand>         &n", // 35
    //"&+y<lower arms>          &n", // 36
    //"&+y<lower hands>         &n", // 37
    //"&+y<lower left wrist>    &n", // 38
    //"&+y<lower right wrist>   &n", // 39
	return;
}

/*!
 * Should we show tooltips?
 */
bool EquipmentWindow::ShowToolTips()
{
    return true;
}

void EquipmentWindow::OnPaint(wxPaintEvent &)
{
  wxPaintDC dc (this); // May want to use wxBufferedPaintDC if this is flicker-y
  if(_background->IsOk())
  {
      dc.DrawBitmap(*_background, 0, 0);
  }
  dc.SetTextForeground(*wxWHITE);
  dc.SetBackgroundMode(wxTRANSPARENT);
  int x = 0;
  int y = 0;
  int center;
  for( int it = 0; it < NUMITEMS; it++ )
  {
    // TODO: Use this for precision text calculation.
    GetTextExtent(*_items[it],&x,&y );
    if( *_items[it] != _("(none)") )
    {
        center = 100 - (x/2);
    }
    else
    {
        center = 124 - (x/2);
    }
    if( center < 0 )
        center = 0;
    dc.DrawText( *_items[it], center, it*TEXTPIXELSIZE );
  }
}

void EquipmentWindow::OnEraseBackground(wxEraseEvent &event)
{
  wxDC* dc = event.GetDC();
  if(_background->IsOk())
  {
      dc->DrawBitmap(*_background, 0, 0);
  }
}
