#include "StatusWindow.h"
#include <memory.h>
#include "Colors.h"
#include "wx/stdpaths.h"

// Lets us use "normal" snprintf and still find the Windows version.
#ifdef WIN32
#define snprintf _snprintf
#endif

/*!
 * EquipmentWindow type definition
 */
IMPLEMENT_DYNAMIC_CLASS( StatusWindow, wxDialog )

/*!
 * EquipmentWindow event table definition
 */
BEGIN_EVENT_TABLE( StatusWindow, wxDialog )
END_EVENT_TABLE()

StatusWindow::StatusWindow()
{
}

StatusWindow::~StatusWindow()
{
}


StatusWindow::StatusWindow(wxWindow* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    Create(parent, id, caption, pos, size, style);
}

bool StatusWindow::Create(wxWindow* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
	iHits = 0;
	iMana = 0;
	iMoves = 0;
	iMaxHits = 0;
	iMaxMana = 0;
	iMaxMoves = 0;
	iEnemyHits = 0;
	iTankHits = 0;
	_ttfStatusFont = NULL;
	_hitMeter = NULL;
	_manaMeter = NULL;
	_moveMeter = NULL;
	_enemyCond = NULL;
	_tankCond = NULL;
	_enemyPos = NULL;
	_tankPos = NULL;
	_tankName = NULL;
	_enemyName = NULL;
	// Set color of text labels - differences in groups for easy reference.
	_statusTitleColor = wxColour(150,180,190);
	_statusTextColor = wxColour( 32,192,32);
#ifdef linux
    _lifebarBitmap = new wxBitmap( _("tiles//life.bmp"), wxBITMAP_TYPE_BMP );
    _manabarBitmap = new wxBitmap( _("tiles//mana.bmp"), wxBITMAP_TYPE_BMP );
    _movebarBitmap = new wxBitmap( _("tiles//moves.bmp"), wxBITMAP_TYPE_BMP );
#else
#ifdef __WXMAC__
    wxString p = wxStandardPaths::Get().GetResourcesDir();
    _lifebarBitmap = new wxBitmap( p + _("//tiles//life.bmp"), wxBITMAP_TYPE_BMP );
    _manabarBitmap = new wxBitmap( p + _("//tiles//mana.bmp"), wxBITMAP_TYPE_BMP );
    _movebarBitmap = new wxBitmap( p + _("//tiles//moves.bmp"), wxBITMAP_TYPE_BMP );
#else
    _lifebarBitmap = new wxBitmap( _(".\\tiles\\life.bmp"), wxBITMAP_TYPE_BMP );
    _manabarBitmap = new wxBitmap( _(".\\tiles\\mana.bmp"), wxBITMAP_TYPE_BMP );
    _movebarBitmap = new wxBitmap( _(".\\tiles\\moves.bmp"), wxBITMAP_TYPE_BMP );
#endif
#endif
	if( _lifebarBitmap == NULL || _manabarBitmap == NULL || _movebarBitmap == NULL ||
		!_lifebarBitmap->Ok() || !_manabarBitmap->Ok() || !_movebarBitmap->Ok() )
	{
		wxMessageBox(_("Unable to load status bitmaps.  Please make sure life.bmp, mana.bmp, and moves.bmp are in the tiles directory."));
	}
	// TODO: Add face name param so it uses above font.
	_ttfStatusFont = new wxFont(12,wxFONTFAMILY_SWISS,wxFONTSTYLE_NORMAL,wxFONTWEIGHT_NORMAL,false);
	if( _ttfStatusFont == NULL )
	{
            wxMessageBox( _("Error while creating font.") );
    }

    SetExtraStyle(GetExtraStyle()|wxWS_EX_BLOCK_EVENTS);
    wxDialog::Create( parent, id, caption, pos, size, style );
    CreateControls();
    GetSizer()->Fit(this);
    GetSizer()->SetSizeHints(this);
    Centre();
    wxTopLevelWindow::SetTransparent(240);
    _haveTank = false;
    _haveEnemy = false;
    return true;
}

void StatusWindow::CreateControls()
{
#ifdef __WXMAC__
    // Required for the background color to take.
    wxPanel* itemDialog1 = new wxPanel(this);
    wxBoxSizer* mainSizer = new wxBoxSizer(wxHORIZONTAL);
    this->SetSizer(mainSizer);
    mainSizer->Add(itemDialog1);
#else
    // On Windows, using an extra wxPanel adds unnecessary drawing flicker.
    StatusWindow* itemDialog1 = this;
#endif
    itemDialog1->SetForegroundColour( *wxWHITE );
    itemDialog1->SetBackgroundColour( *wxBLACK );

    wxFlexGridSizer* itemFlexGridSizer2 = new wxFlexGridSizer(18, 2, 0, 0);
    itemDialog1->SetSizer(itemFlexGridSizer2);

	wxStaticText* itemStaticText14 = new wxStaticText( itemDialog1, wxID_STATIC, _("Hits") );
    itemFlexGridSizer2->Add(itemStaticText14, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText14->SetForegroundColour( _statusTitleColor );

	_hitMeter = new wxOwnerDrawStaticBitmap( itemDialog1, wxID_STATIC, *_lifebarBitmap, wxDefaultPosition, wxDefaultSize );
	_hitMeter->SetBackgroundColour(clrBlack);
    itemFlexGridSizer2->Add(_hitMeter, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALIGN_LEFT|wxALL, 5 );

	wxStaticText* itemStaticText15 = new wxStaticText( itemDialog1, wxID_STATIC, _("Mana") );
    itemFlexGridSizer2->Add(itemStaticText15, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText15->SetForegroundColour( _statusTitleColor );

	_manaMeter = new wxOwnerDrawStaticBitmap( itemDialog1, wxID_STATIC, *_manabarBitmap, wxDefaultPosition, wxDefaultSize );
	_manaMeter->SetBackgroundColour(clrBlack);
    itemFlexGridSizer2->Add(_manaMeter, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALIGN_LEFT|wxALL, 5 );

    wxStaticText* itemStaticText16 = new wxStaticText( itemDialog1, wxID_STATIC, _("Moves") );
    itemFlexGridSizer2->Add(itemStaticText16, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText16->SetForegroundColour( _statusTitleColor );

	_moveMeter = new wxOwnerDrawStaticBitmap( itemDialog1, wxID_STATIC, *_movebarBitmap, wxDefaultPosition, wxDefaultSize );
	_moveMeter->SetBackgroundColour(clrBlack);
    itemFlexGridSizer2->Add(_moveMeter, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALIGN_LEFT|wxALL, 5 );

	wxStaticText* itemStaticText17 = new wxStaticText( itemDialog1, wxID_STATIC, _("Tank") );
    itemFlexGridSizer2->Add(itemStaticText17, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText17->SetForegroundColour( _statusTitleColor );

	_tankName = new wxStaticText( itemDialog1, wxID_STATIC, _(""), wxDefaultPosition, wxSize(100, 20) );
    itemFlexGridSizer2->Add(_tankName, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	wxStaticText* itemStaticText18 = new wxStaticText( itemDialog1, wxID_STATIC, _("Condition") );
    itemFlexGridSizer2->Add(itemStaticText18, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText18->SetForegroundColour( _statusTitleColor );

	_tankCond = new wxOwnerDrawStaticBitmap( itemDialog1, wxID_STATIC, *_lifebarBitmap, wxDefaultPosition, wxDefaultSize );
	_tankCond->SetBackgroundColour(clrBlack);
	itemFlexGridSizer2->Add(_tankCond, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	wxStaticText* itemStaticText28 = new wxStaticText( itemDialog1, wxID_STATIC, _("Position") );
    itemFlexGridSizer2->Add(itemStaticText28, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText28->SetForegroundColour( _statusTitleColor );

	_tankPos = new wxStaticText( itemDialog1, wxID_STATIC, _(""), wxDefaultPosition, wxSize(100, 20) );
    itemFlexGridSizer2->Add(_tankPos, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	wxStaticText* itemStaticText19 = new wxStaticText( itemDialog1, wxID_STATIC, _("Enemy") );
    itemFlexGridSizer2->Add(itemStaticText19, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText19->SetForegroundColour( _statusTitleColor );

	_enemyName = new wxStaticText( itemDialog1, wxID_STATIC, _(""), wxDefaultPosition, wxSize(100, 20) );
    itemFlexGridSizer2->Add(_enemyName, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	wxStaticText* itemStaticText20 = new wxStaticText( itemDialog1, wxID_STATIC, _("Condition") );
    itemFlexGridSizer2->Add(itemStaticText20, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText20->SetForegroundColour( _statusTitleColor );

	_enemyCond = new wxOwnerDrawStaticBitmap( itemDialog1, ID_ENEMYCONDITION, *_lifebarBitmap, wxDefaultPosition, wxDefaultSize );
	_enemyCond->SetBackgroundColour(clrBlack);
	itemFlexGridSizer2->Add(_enemyCond, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	wxStaticText* itemStaticText38 = new wxStaticText( itemDialog1, wxID_STATIC, _("Position") );
    itemFlexGridSizer2->Add(itemStaticText38, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
	itemStaticText38->SetForegroundColour( _statusTitleColor );

	_enemyPos = new wxStaticText( itemDialog1, wxID_STATIC, _(""), wxDefaultPosition, wxSize(100, 20) );
    itemFlexGridSizer2->Add(_enemyPos, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

    iMaxHits = 100;
    iMaxMoves = 100;
    iMaxMana = 100;
    iMana = 100;
    iMoves = 100;
    iHits = 100;
    UpdateHitMeter();
    UpdateManaMeter();
    UpdateMoveMeter();
	setTankCond(_("0"));
	setEnemyCond(_("0"));
}

void StatusWindow::SetStatusColor( int r, int g, int b )
{
    _statusTextColor = wxColour(r,g,b);
}

void StatusWindow::SetStatusTitleColor( int r, int g, int b )
{
    _statusTitleColor = wxColour(r,g,b);
}

void StatusWindow::setHits( int hits )
{
	iHits = hits;
	UpdateHitMeter();
}

void StatusWindow::setMana( int mana )
{
	iMana = mana;
	UpdateManaMeter();
}

void StatusWindow::setMoves( int moves )
{
	iMoves = moves;
	UpdateMoveMeter();
}

void StatusWindow::setMaxHits( int hits )
{
	iMaxHits = hits;
	UpdateHitMeter();
}

void StatusWindow::setMaxMana( int mana )
{
	iMaxMana = mana;
	UpdateManaMeter();
}

void StatusWindow::setMaxMoves( int moves )
{
	iMaxMoves = moves;
	UpdateMoveMeter();
}

void StatusWindow::setEnemyHits( int hits )
{
	iEnemyHits = hits;
}

void StatusWindow::setTankHits( int hits )
{
	iTankHits = hits;
}

void StatusWindow::setFont( wxFont* font )
{
	_ttfStatusFont = font;
}

void StatusWindow::setTankPosition( wxString position )
{
	if( position == _("0"))
	{
		_tankPos->SetLabel(_(""));
		return;
	}

	if( position == _("sta") )
	{
		_tankPos->SetLabel( _("Standing") );
	}
	else if( position == _("sit") )
	{
		_tankPos->SetLabel( _("Sitting") );
	}
	else if( position == _("kne") )
	{
		_tankPos->SetLabel( _("Kneeling") );
	}
	else if( position == _("ass" ))
	{
		_tankPos->SetLabel( _("On Their Ass") );
	}
	else
	{
		_tankPos->SetLabel( position );
	}
        Refresh();
}

void StatusWindow::setPlayerPosition( wxString position )
{
	// Not displaying player position.
	return;
	if( position == _("standing" ))
	{
		_playerPos->SetLabel( _("Standing") );
	}
	else if( position == _( "kneeling" ))
	{
		_playerPos->SetLabel( _("Kneeling") );
	}
	else if( position == _( "sitting" ))
	{
		_playerPos->SetLabel( _("Sitting") );
	}
	else if( position == _( "reclining" ))
	{
		_playerPos->SetLabel( _("Reclining") );
	}
	else if( position == _( "on" ))
	{
		_playerPos->SetLabel( _("On Your Ass") );
	}
	else
	{
		_playerPos->SetLabel( position );
	}
        Refresh();
}

void StatusWindow::setEnemyPosition( wxString position )
{
	if( position == _("0"))
	{
		_enemyPos->SetLabel(_("(none)"));
		return;
	}

	if( position == _("sta" ))
	{
		_enemyPos->SetLabel( _("Standing") );
	}
	else if( position == _( "sit" ))
	{
		_enemyPos->SetLabel( _("Sitting") );
	}
	else if( position == _( "kne" ))
	{
		_enemyPos->SetLabel( _("Kneeling") );
	}
	else if( position == _( "ass" ))
	{
		_enemyPos->SetLabel( _("On Their Ass") );
	}
	else
	{
		_enemyPos->SetLabel( position );
	}
        Refresh();
}

void StatusWindow::setTankName( wxString name )
{
	if( name == _("0"))
	{
		_tankName->SetLabel(_("(none)"));
		_tankPos->SetLabel(_(""));
		_tankCond->_drawWidth = 0;
		return;
	}
	name.Replace(_("_"), _(" "), true );
	_tankName->SetLabel(name);
}

void StatusWindow::setEnemyName( wxString name )
{
	if( name == _("0"))
	{
		_enemyName->SetLabel(_("(none)"));
		_enemyPos->SetLabel(_(""));
		_enemyCond->_drawWidth = 0;
		return;
	}
	name.Replace(_("_"), _(" "), true );
    _enemyName->SetLabel(name);
}

void StatusWindow::setTankCond( wxString cond )
{
	int width = atoi(cond.ToAscii());
	if( width > 100 ) width = 100;
	_tankCond->_drawWidth = width;
	_tankCond->Refresh();
}

void StatusWindow::setEnemyCond( wxString cond )
{
	int width = atoi(cond.ToAscii());
	if( width > 100 ) width = 100;
	_enemyCond->_drawWidth = width;
	_enemyCond->Refresh();
}

void StatusWindow::setPlayerCond( wxString cond )
{
	// NOT USED: Player condition is a meter.
	//snprintf(playerCond, STATUS_STRING_LENGTH, cond );
}

void StatusWindow::UpdateHitMeter()
{
	int size = 0;
	if( iMaxHits > 0 )
		size = iHits * 100 / iMaxHits;
	if( size > 100 ) size = 100;
	_hitMeter->_drawWidth = size;
	_hitMeter->Refresh();
}

void StatusWindow::UpdateManaMeter()
{
	int size = 0;
	if( iMaxMana > 0 )
		size = iMana * 100 / iMaxMana;
	if( size > 100 ) size = 100;
	_manaMeter->_drawWidth = size;
	_manaMeter->Refresh();
}

void StatusWindow::UpdateMoveMeter()
{
	int size = 0;
	if( iMaxMoves > 0 )
		size = iMoves * 100 / iMaxMoves;
	if( size > 100 ) size = 100;
	_moveMeter->_drawWidth = size;
	_moveMeter->Refresh();
}

/**
* Extracts a number from a string, returns it, and sets the original position counter to the next available character.
*/
int StatusWindow::ExtractNumberFromString(wxString outbuf, unsigned int* count)
{
	wxString number;
	bool valid = true;
	// This will be true at least once, for the first digit.
	while( outbuf[*count] >= '0' && outbuf[*count] <= '9' && valid )
	{
		number.Append(outbuf[*count]);
		++(*count);
		if( number.Length() >= 126 )
			valid = false;
	}
	// Treat it as a hitpoint or somesuch.
	long value = 0;
	number.ToLong(&value);
	return value;
}

void StatusWindow::ProcessPrompt( wxString outbuf )
{
	unsigned int count;
	unsigned int length = outbuf.Length();

	for( count = 0; count < length; count++ )
	{
		if( outbuf.Length() > count+2 && outbuf[count] == 'H' && outbuf[count+1] == ':')
		{
			count+=2;
			setHits( ExtractNumberFromString(outbuf, &count ) );
			continue;
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'I' && outbuf[count+1] == ':')
		{
			count+=2;
			setMaxHits( ExtractNumberFromString(outbuf, &count ) );
			continue;
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'M' && outbuf[count+1] == ':')
		{
			count+=2;
			setMoves( ExtractNumberFromString(outbuf, &count ) );
			continue;
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'N' && outbuf[count+1] == ':')
		{
			count+=2;
			setMaxMoves( ExtractNumberFromString(outbuf, &count ) );
			continue;
		}
		if(outbuf.Length() > count+2 &&  outbuf[count] == 'A' && outbuf[count+1] == ':')
		{
			count+=2;
			setMana( ExtractNumberFromString(outbuf, &count ) );
			continue;
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'B' && outbuf[count+1] == ':')
		{
			count+=2;
			setMaxMana( ExtractNumberFromString(outbuf, &count ) );
			continue;
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'P' && outbuf[count+1] == ':')
		{
			//Log( "Prompt mode: player name." );
            _haveTank = true;
			count += 2;
			bool ended = false;
			wxString name;
			if( count < length )
			{
				while( (count < length) && !ended && (name.Length() < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf[count] == ' ' )
					{
						ended = true;
						setTankName( name );
						//Log( name );
						continue;
					}
					else
					{
						name.Append( outbuf[count] );
						count++;
					}
				}
			}
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'T' && outbuf[count+1] == ':')
		{
			//Log( "Prompt mode: tank name." );
            _haveTank = true;
			count += 2;
			bool ended = false;
			wxString name;
			if( count < length )
			{
				while( (count < length) && !ended && (name.Length() < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf[count] == ' ' )
					{
						ended = true;
						setTankName( name );
						//Log( name );
						continue;
					}
					else
					{
						name.Append(outbuf[count]);
						count++;
					}
				}
			}
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'E' && outbuf[count+1] == ':' )
		{
			//Log( "Prompt mode: enemy name." );
            _haveEnemy = true;
			count += 2;
			bool ended = false;
			wxString name;
			if( count < length )
			{
				while( (count < length) && !ended && (name.Length() < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf[count] == ' ' )
					{
						ended = true;
						setEnemyName( name );
						//Log( name );
						continue;
					}
					else
					{
						name.Append(outbuf[count]);
						count++;
					}
				}
			}
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'U' && outbuf[count+1] == ':')
		{
			//Log( "Prompt mode: tank condition." );
            _haveTank = true;
			count += 2;
			bool ended = false;
			wxString condition;
			if( count < length )
			{
				while( (count < length) && !ended && (condition.Length() < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf != _("few") && outbuf[count] == ' ' )
					{
						ended = true;
						setTankCond( condition );
						//Log( condition );
						continue;
					}
					else
					{
						condition.Append(outbuf[count]);
						count++;
					}
				}
			}
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'Q' && outbuf[count+1] == ':')
		{
			// Not used -- we have the exact hits from the prompt.
			//Log( "Prompt mode: player condition." );
			count += 2;
			bool ended = false;
			wxString condition;
			if( count < length )
			{
				while( (count < length) && !ended && (condition.Length() < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf != _("few") && outbuf != _("nearly") && outbuf != _("nearly") && outbuf[count] == ' ' )
					{
						ended = true;
						//DO NOTHING setTankCond( condition );
						//Log( condition );
						continue;
					}
					else
					{
						condition.Append(outbuf[count]);
						count++;
					}
				}
			}
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'V' && outbuf[count+1] == ':' )
		{
			//Log( "Prompt mode: tank position." );
            _haveTank = true;
			count += 2;
			bool ended = false;
			wxString position;
			if( count < length )
			{
				while( (count < length) && !ended && (position.Length() < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf[count] == ' ' )
					{
						ended = true;
						setTankPosition( position );
						//Log( position );
						continue;
					}
					else
					{
						position.Append(outbuf[count]);
						count++;
					}
				}
			}
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'F' && outbuf[count+1] == ':')
		{
			//Log( "Prompt mode: enemy condition." );
            _haveEnemy = true;
			count += 2;
			bool ended = false;
			wxString condition;
			if( count < length )
			{
				while( (count < length) && !ended && (condition.Length() < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf != _("few") && outbuf != _("nearly") && outbuf[count] == ' ' )
					{
						ended = true;
						setEnemyCond( condition );
						//Log( condition );
						continue;
					}
					else
					{
						condition.Append(outbuf[count]);
						count++;
					}
				}
			}

		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'G' && outbuf[count+1] == ':')
		{
			//Log( "Prompt mode: enemy position." );
            _haveEnemy = true;
			count += 2;
			bool ended = false;
			wxString position;
			if( count < length )
			{
				while( (count < length) && !ended && (position.Length() < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf[count] == ' ' )
					{
						ended = true;
						setEnemyPosition( position );
						//Log( position );
						continue;
					}
					else
					{
						position.Append( outbuf[count] );
						count++;
					}
				}
			}
		}
		if( outbuf.Length() > count+2 && outbuf[count] == 'R' && outbuf[count+1] == ':')
		{
			//Log( "Prompt Mode: player position." );
			count += 2;
			bool ended = false;
			wxString position;
			int poscount = 0;
			if( count < length )
			{
				while( (count < length) && !ended && (poscount < MAX_STATUS_ITEM_LENGTH) )
				{
					if( outbuf[count] == '>' || outbuf[count] == ' ')
					{
						ended = true;
						setPlayerPosition( position );
						//Log( position );
						continue;
					}
					else
					{
						position.Append(outbuf[count]);
						count++;
					}
				}
			}
		}
		Refresh();
	}

}

