#include "wx/wx.h"
#include "GroupWindow.h"

IMPLEMENT_DYNAMIC_CLASS( GroupWindow, wxDialog )

BEGIN_EVENT_TABLE( GroupWindow, wxDialog )
END_EVENT_TABLE()

GroupWindow::GroupWindow()
{
}

GroupWindow::GroupWindow(wxWindow* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    Create(parent, id, caption, pos, size, style);
}

bool GroupWindow::Create(wxWindow* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    SetTransparent( 240 );
    SetExtraStyle(GetExtraStyle()|wxWS_EX_BLOCK_EVENTS);
	wxDialog::Create( parent, id, caption, pos, size, style );
    CreateControls();
    GetSizer()->Fit(this);
    GetSizer()->SetSizeHints(this);
    Centre();
    wxTopLevelWindow::SetTransparent(240);
    return true;
}

void GroupWindow::CreateControls()
{
    GroupWindow* itemDialog1 = this;
    this->SetForegroundColour( *wxWHITE );
    this->SetBackgroundColour( *wxBLACK );

#ifdef linux
    _lifebarBitmap = new wxBitmap( _("tiles//life_sml.bmp"), wxBITMAP_TYPE_BMP );
    _manabarBitmap = new wxBitmap( _("tiles//mana_sml.bmp"), wxBITMAP_TYPE_BMP );
    _movebarBitmap = new wxBitmap( _("tiles//moves_sml.bmp"), wxBITMAP_TYPE_BMP );
#else
#ifdef __WXMAC__
    wxString p = wxStandardPaths::Get().GetResourcesDir();
    _lifebarBitmap = new wxBitmap( p + _("//tiles//life_sml.bmp"), wxBITMAP_TYPE_BMP );
    _manabarBitmap = new wxBitmap( p + _("//tiles//mana_sml.bmp"), wxBITMAP_TYPE_BMP );
    _movebarBitmap = new wxBitmap( p + _("//tiles//moves_sml.bmp"), wxBITMAP_TYPE_BMP );
#else
    _lifebarBitmap = new wxBitmap( _(".\\tiles\\life_sml.bmp"), wxBITMAP_TYPE_BMP );
    _manabarBitmap = new wxBitmap( _(".\\tiles\\mana_sml.bmp"), wxBITMAP_TYPE_BMP );
    _movebarBitmap = new wxBitmap( _(".\\tiles\\moves_sml.bmp"), wxBITMAP_TYPE_BMP );
#endif
#endif
	if( _lifebarBitmap == NULL || _manabarBitmap == NULL || _movebarBitmap == NULL ||
		!_lifebarBitmap->Ok() || !_manabarBitmap->Ok() || !_movebarBitmap->Ok() )
	{
		wxMessageBox(_("Unable to load group status bitmaps.  Please make sure life_sml.bmp, mana_sml.bmp, and moves_sml.bmp are in the tiles directory."));
	}

    wxFlexGridSizer* itemFlexGridSizer2 = new wxFlexGridSizer(8, 2, 0, 0);
    itemDialog1->SetSizer(itemFlexGridSizer2);

    for( int i = 0; i < MAX_GROUP; i++ )
    {
        _maxHit[i] = 100;
        _hit[i] = 100;
        _maxMana[i] = 100;
        _mana[i] = 100;
        _maxMove[i] = 100;
        _move[i] = 100;

        wxBoxSizer* sizerA = new wxBoxSizer(wxVERTICAL);
	    _txtName[i] = new wxStaticText( itemDialog1, ID_NAME1+i, _("(none)"), wxDefaultPosition, wxSize( 90, -1 ), 0 );
	    sizerA->Add(_txtName[i], 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 3 );
        _txtName[i]->SetForegroundColour( *wxWHITE );
        _txtName[i]->SetBackgroundColour( *wxBLACK );

        wxBoxSizer* sizerB = new wxBoxSizer(wxHORIZONTAL);

	    _txtLevel[i] = new wxStaticText( itemDialog1, ID_LEVEL1+i, _("1"), wxDefaultPosition, wxSize( 16, -1 ), 0 );
	    sizerB->Add(_txtLevel[i], 0, wxALIGN_RIGHT|wxALIGN_CENTER_VERTICAL|wxALL, 3 );
        _txtLevel[i]->SetForegroundColour( *wxWHITE );
        _txtLevel[i]->SetBackgroundColour( *wxBLACK );

   	    _txtClass[i] = new wxStaticText( itemDialog1, ID_CLASS1+i, _("(none)"), wxDefaultPosition, wxSize( 76, -1 ), 0 );
	    sizerB->Add(_txtClass[i], 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 3 );
        _txtClass[i]->SetForegroundColour( *wxWHITE );
        _txtClass[i]->SetBackgroundColour( *wxBLACK );

        sizerA->Add(sizerB, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 0 );
	    itemFlexGridSizer2->Add(sizerA, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 1 );

        wxBoxSizer* sizerC = new wxBoxSizer(wxVERTICAL);

        _hitMeter[i] = new wxOwnerDrawStaticBitmap( itemDialog1, ID_HITMETER1+i, *_lifebarBitmap, wxDefaultPosition, wxSize(68,10));
        sizerC->Add(_hitMeter[i], 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 1 );

        _manaMeter[i] = new wxOwnerDrawStaticBitmap( itemDialog1, ID_MANAMETER1+i, *_manabarBitmap, wxDefaultPosition, wxSize(68,10));
        sizerC->Add(_manaMeter[i], 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 1 );

        _moveMeter[i] = new wxOwnerDrawStaticBitmap( itemDialog1, ID_MOVEMETER1+i, *_movebarBitmap, wxDefaultPosition, wxSize(68,10));
        sizerC->Add(_moveMeter[i], 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 1 );

        itemFlexGridSizer2->Add(sizerC, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 1 );

        UpdateHitMeter(i);
        UpdateManaMeter(i);
        UpdateMoveMeter(i);
    }
}

void GroupWindow::ProcessGroupData( wxString text )
{
    wxString outText;

    int numMembers = 0;
    while(1)
    {
        int pos = text.Find(wxChar(':'));
        if( pos == wxNOT_FOUND || pos == 0 )
		    break;
        outText = text.SubString(0, (pos-1));
	    ProcessGroupMember(outText); // Could crash if we didn't allocate enough lines.
	    text = text.SubString((pos+1), (text.Length() - 1));
        numMembers++;
    }
    if( numMembers < 8 )
    {
        for( int x = numMembers; x < 8; x++ )
        {
            ShowMember(x, false);
        }
    }
    Refresh();
    return;
}

void GroupWindow::ShowMember(int num, bool show)
{
    if( num > 7 ) return;
    _txtName[num]->Show(show);
    _txtLevel[num]->Show(show);
    _txtClass[num]->Show(show);
    _hitMeter[num]->Show(show);
    _manaMeter[num]->Show(show);
    _moveMeter[num]->Show(show);
}

void GroupWindow::ProcessGroupMember( wxString text )
{
    int num = 0;
    int line = 0;
    wxString outText;
    while(1)
    {
        int pos = text.Find(wxChar(','));
        if( pos == wxNOT_FOUND || pos == 0 || num > (MAX_GROUP - 1))
		    break;
        outText = text.SubString(0, (pos-1));
        switch( num )
        {
        case 0:
            line = atoi(outText.ToAscii());
            break;
        case 1:
            _txtLevel[line]->SetLabel(outText);
            break;
        case 2:
            _txtClass[line]->SetLabel(outText);
            break;
        case 3:
            _txtName[line]->SetLabel(outText);
            ShowMember(line, true);
            break;
        case 4:
            _hit[line] = atoi(outText.ToAscii());
            break;
        case 5:
            _maxHit[line] = atoi(outText.ToAscii());
            break;
        case 6:
            _mana[line] = atoi(outText.ToAscii());
            break;
        case 7:
            _maxMana[line] = atoi(outText.ToAscii());
            break;
        case 8:
            _move[line] = atoi(outText.ToAscii());
            break;
        case 9:
            _maxMove[line] = atoi(outText.ToAscii());
            break;
        }
	    text = text.SubString((pos+1), (text.Length() - 1));
        ++num;
    }
    return;    
}

/*!
 * Should we show tooltips?
 */
bool GroupWindow::ShowToolTips()
{
    return true;
}

void GroupWindow::UpdateHitMeter(int num)
{
    if( num > (MAX_GROUP -1) ) return;
	int size = 0;
	if( _maxHit[num] > 0 )
		size = _hit[num] * 100 / _maxHit[num];
	if( size > 100 ) size = 100;
	_hitMeter[num]->_drawWidth = size;
	_hitMeter[num]->Refresh();
}

void GroupWindow::UpdateManaMeter(int num)
{
    if( num > (MAX_GROUP -1) ) return;
	int size = 0;
	if( _maxMana[num] > 0 )
        size = _mana[num] * 100 / _maxMana[num];
	if( size > 100 ) size = 100;
	_manaMeter[num]->_drawWidth = size;
	_manaMeter[num]->Refresh();
}

void GroupWindow::UpdateMoveMeter(int num)
{
    if( num > (MAX_GROUP -1) ) return;
	int size = 0;
	if( _maxMove[num] > 0 )
		size = _move[num] * 100 / _maxMove[num];
	if( size > 100 ) size = 100;
	_moveMeter[num]->_drawWidth = size;
	_moveMeter[num]->Refresh();
}
