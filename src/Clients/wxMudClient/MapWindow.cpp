#include "MapWindow.h"
#include "wxMudClientDlg.h"
#include "wx/stdpaths.h"
#include "Colors.h"

#include <memory.h>
#include <stdlib.h> // Needed for rand().
#include <string> // Needed for strlen().
#include <iostream>
#include <sstream>
using namespace std;

// Make snprintf work on Windows.
#ifdef WIN32
#define snprintf _snprintf
#endif

#include "wx/wx.h"

IMPLEMENT_DYNAMIC_CLASS( MapWindow, wxDialog )

BEGIN_EVENT_TABLE( MapWindow, wxDialog )
END_EVENT_TABLE()

MapWindow::MapWindow()
{
}

MapWindow::~MapWindow( )
{
}

MapWindow::MapWindow(wxWindow* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    Create(parent, id, caption, pos, size, style);
}

bool MapWindow::Create(wxWindow* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    //_mapImage = NULL;
    _currentFont = new wxFont(12,wxFONTFAMILY_MODERN,wxFONTSTYLE_NORMAL,wxFONTWEIGHT_NORMAL,false);
    if( _currentFont == NULL )
    {
            wxMessageBox( _("Error while setting map window font."));
    }
	//load bitmap tiles - change to load all tiles.
#ifdef linux
    wxString pathPrefix = _("tiles//");
#endif
#ifdef WXMAC
    wxString pathPrfvix = wxStandardPaths::Get().GetResourcesDir() + _("//tiles//");
#endif
#ifdef WIN32
    wxString pathPrefix = _(".\\tiles\\");
#endif
    // Background Tiles

    // TODO: Since the tiles used for Basternae are not open source, provide some placeholder
    // tiles that demonstrate how the map works and give an easy way to configure what tiles
    // are used for what terrain.
    //
    // The graphical map system could be better coordinated with the core codebase -- i.e.
    // loaded from a unified config file that the client and server both use.

    /*
    _backgroundTile[0] =  new wxBitmap( pathPrefix + _("river.png"), wxBITMAP_TYPE_PNG ) ;
    _backgroundTile[1] =  new wxBitmap( pathPrefix + _("field_green.png"), wxBITMAP_TYPE_PNG ) ;
    _backgroundTile[2] =  new wxBitmap( pathPrefix + _("field_arid.png"), wxBITMAP_TYPE_PNG ) ;
    _backgroundTile[3] =  new wxBitmap( pathPrefix + _("cavefloor.png"), wxBITMAP_TYPE_PNG ) ;
    _backgroundTile[4] =  new wxBitmap( pathPrefix + _("lava.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[5] =  new wxBitmap( pathPrefix + _("glacier.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[6] =  new wxBitmap( pathPrefix + _("charred.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[7] =  new wxBitmap( pathPrefix + _("permafrost.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[8] =  new wxBitmap( pathPrefix + _("ocean.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[9] =  new wxBitmap( pathPrefix + _("solidrock.png"), wxBITMAP_TYPE_PNG );    
    _backgroundTile[10] = new wxBitmap( pathPrefix + _("desert.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[11] = new wxBitmap( pathPrefix + _("tundra.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[12] = new wxBitmap( pathPrefix + _("swamp.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[13] = new wxBitmap( pathPrefix + _("black.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[14] = new wxBitmap( pathPrefix + _("brown.png"), wxBITMAP_TYPE_PNG );
    _backgroundTile[15] = new wxBitmap( pathPrefix + _("white.png"), wxBITMAP_TYPE_PNG );

    // Foreground Tiles
    _foregroundTile[0] =   new wxBitmap( pathPrefix + _("blank.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[1] =   new wxBitmap( pathPrefix + _("ash_tree.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[2] =   new wxBitmap( pathPrefix + _("bridge.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[3] =   new wxBitmap( pathPrefix + _("bushes.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[4] =   new wxBitmap( pathPrefix + _("cactus.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[5] =   new wxBitmap( pathPrefix + _("castle.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[6] =   new wxBitmap( pathPrefix + _("cave_entrance.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[7] =   new wxBitmap( pathPrefix + _("crater.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[8] =   new wxBitmap( pathPrefix + _("dragon_shadow.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[9] =   new wxBitmap( pathPrefix + _("dust_cloud.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[10] =  new wxBitmap( pathPrefix + _("enslaver_city.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[11] =  new wxBitmap( pathPrefix + _("evil_city.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[12] =  new wxBitmap( pathPrefix + _("fog.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[13] =  new wxBitmap( pathPrefix + _("fourlegged_shadow.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[14] =  new wxBitmap( pathPrefix + _("good_city.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[15] =  new wxBitmap( pathPrefix + _("green_grass.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[16] =  new wxBitmap( pathPrefix + _("hills_brown.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[17] =  new wxBitmap( pathPrefix + _("hills_brown_cave.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[18] =  new wxBitmap( pathPrefix + _("hills_green.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[19] =  new wxBitmap( pathPrefix + _("hills_green_cave.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[20] =  new wxBitmap( pathPrefix + _("hills_icysnow.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[21] =  new wxBitmap( pathPrefix + _("hills_icysnow_cave.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[22] =  new wxBitmap( pathPrefix + _("humanoid_shadow_large.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[23] =  new wxBitmap( pathPrefix + _("humanoid_shadow_large_w.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[24] =  new wxBitmap( pathPrefix + _("humanoid_shadow_small.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[25] =  new wxBitmap( pathPrefix + _("humanoid_shadow_small_w.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[26] =  new wxBitmap( pathPrefix + _("humanoid_shadow_medium.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[27] =  new wxBitmap( pathPrefix + _("humanoid_shadow_medium_w.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[28] =  new wxBitmap( pathPrefix + _("humanoid_statue.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[29] =  new wxBitmap( pathPrefix + _("island.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[30] =  new wxBitmap( pathPrefix + _("jungle_tree.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[31] =  new wxBitmap( pathPrefix + _("ladder_down.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[32] =  new wxBitmap( pathPrefix + _("ladder_up.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[33] =  new wxBitmap( pathPrefix + _("library.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[34] =  new wxBitmap( pathPrefix + _("mansion.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[35] =  new wxBitmap( pathPrefix + _("maple_tree.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[36] =  new wxBitmap( pathPrefix + _("mountain.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[37] =  new wxBitmap( pathPrefix + _("mountain_cave.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[38] =  new wxBitmap( pathPrefix + _("mountain_snow.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[39] =  new wxBitmap( pathPrefix + _("mountain_snow_cave.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[40] =  new wxBitmap( pathPrefix + _("neutral_city.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[41] =  new wxBitmap( pathPrefix + _("oak_tree.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[42] =  new wxBitmap( pathPrefix + _("obelisk.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[43] =  new wxBitmap( pathPrefix + _("palm_tree.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[44] =  new wxBitmap( pathPrefix + _("pier.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[45] =  new wxBitmap( pathPrefix + _("pine_tree.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[46] =  new wxBitmap( pathPrefix + _("pit.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[47] =  new wxBitmap( pathPrefix + _("poison_cloud.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[48] =  new wxBitmap( pathPrefix + _("pool.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[49] =  new wxBitmap( pathPrefix + _("primitive_hut.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[50] =  new wxBitmap( pathPrefix + _("primitive_village.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[51] =  new wxBitmap( pathPrefix + _("pyramid.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[52] =  new wxBitmap( pathPrefix + _("road_ew.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[53] =  new wxBitmap( pathPrefix + _("road_ns.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[54] =  new wxBitmap( pathPrefix + _("road_4way.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[55] =  new wxBitmap( pathPrefix + _("road_corner_ne.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[56] =  new wxBitmap( pathPrefix + _("road_corner_nw.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[57] =  new wxBitmap( pathPrefix + _("road_corner_se.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[58] =  new wxBitmap( pathPrefix + _("road_corner_sw.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[59] =  new wxBitmap( pathPrefix + _("road_tshape_e.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[60] =  new wxBitmap( pathPrefix + _("road_tshape_n.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[61] =  new wxBitmap( pathPrefix + _("road_tshape_s.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[62] =  new wxBitmap( pathPrefix + _("road_tshape_w.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[63] =  new wxBitmap( pathPrefix + _("skeleton_fourlegged.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[64] =  new wxBitmap( pathPrefix + _("skeleton_human.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[65] =  new wxBitmap( pathPrefix + _("skull.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[66] =  new wxBitmap( pathPrefix + _("stalactite.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[67] =  new wxBitmap( pathPrefix + _("stalactite_icy.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[68] =  new wxBitmap( pathPrefix + _("stalagmite.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[69] =  new wxBitmap( pathPrefix + _("stalagmite_icy.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[70] =  new wxBitmap( pathPrefix + _("stones_circle.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[71] =  new wxBitmap( pathPrefix + _("stones_grey.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[72] =  new wxBitmap( pathPrefix + _("stones_mossy.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[73] =  new wxBitmap( pathPrefix + _("stones_sandy.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[74] =  new wxBitmap( pathPrefix + _("swamptree.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[75] =  new wxBitmap( pathPrefix + _("temple.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[76] =  new wxBitmap( pathPrefix + _("tombstone.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[77] =  new wxBitmap( pathPrefix + _("tower.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[78] =  new wxBitmap( pathPrefix + _("ud_mushroom_large.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[79] =  new wxBitmap( pathPrefix + _("ud_mushrooms_small.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[80] =  new wxBitmap( pathPrefix + _("volcano.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[81] =  new wxBitmap( pathPrefix + _("well.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[82] =  new wxBitmap( pathPrefix + _("whirlpool.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[83] =  new wxBitmap( pathPrefix + _("yellow_grass.png"), wxBITMAP_TYPE_PNG ) ;
    _foregroundTile[84] =  new wxBitmap( pathPrefix + _("ash_cloud.png"), wxBITMAP_TYPE_PNG ) ;
    */

	memset( &_zone, 0, sizeof( _zone ));
	memset( &_room, 0, sizeof( _room ));
    memset(_currentTiles, 0, sizeof(wxBitmap*) * XTILES * YTILES * 2 );
	_color = wxColor(180,180,180);
    _titleColor = wxColour(150,180,190);
	_zoneMode = true;
	_tiled = true;
    SetExtraStyle(GetExtraStyle()|wxWS_EX_BLOCK_EVENTS);
    wxDialog::Create( parent, id, caption, pos, size, style );
    CreateControls();
    GetSizer()->Fit(this);
    GetSizer()->SetSizeHints(this);
    Centre();
    wxTopLevelWindow::SetTransparent(248);
    return true;
}

void MapWindow::CreateControls()
{
#ifdef __WXMAC__
    // Required for the background color to take.
    wxPanel* itemDialog1 = new wxPanel(this);
    wxBoxSizer* mainSizer = new wxBoxSizer(wxHORIZONTAL);
    this->SetSizer(mainSizer);
    mainSizer->Add(itemDialog1);
#else
    // On Windows, using an extra wxPanel adds unnecessary drawing flicker.
    MapWindow* itemDialog1 = this;
#endif

    itemDialog1->SetForegroundColour( *wxWHITE );
    itemDialog1->SetBackgroundColour( *wxBLACK );

	wxBoxSizer* itemSizer1 = new wxBoxSizer(wxVERTICAL);
	itemDialog1->SetSizer(itemSizer1);

    wxFlexGridSizer* itemFlexGridSizer2 = new wxFlexGridSizer(7, 2, 0, 0);
    itemSizer1->Add(itemFlexGridSizer2);

	wxStaticText* itemStaticText15 = new wxStaticText( itemDialog1, wxID_STATIC, _("Zone") );
    itemFlexGridSizer2->Add(itemStaticText15, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
    itemStaticText15->SetForegroundColour( _titleColor );

	_zone = new wxStaticText( itemDialog1, wxID_STATIC, _("(none)") );
    itemFlexGridSizer2->Add(_zone, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	wxStaticText* itemStaticText16 = new wxStaticText( itemDialog1, wxID_STATIC, _("Room") );
    itemFlexGridSizer2->Add(itemStaticText16, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
    itemStaticText16->SetForegroundColour( _titleColor );

	_room = new wxStaticText( itemDialog1, wxID_STATIC, _("(none)") );
    itemFlexGridSizer2->Add(_room, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	wxStaticText* itemStaticText19 = new wxStaticText( itemDialog1, wxID_STATIC, _("Exits") );
    itemFlexGridSizer2->Add(itemStaticText19, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );
    itemStaticText19->SetForegroundColour( _titleColor );

    _exits = new wxStaticText( itemDialog1, wxID_STATIC, _("(none)"));
    itemFlexGridSizer2->Add(_exits, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

	_roomDescription = new wxStaticText( itemDialog1, wxID_STATIC, _("(none)"), wxDefaultPosition, wxSize(300, 320) );
    _roomDescription->SetForegroundColour( clrWhite );
    _roomDescription->SetBackgroundColour( clrBlack );
	itemSizer1->Add(_roomDescription, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	_mapGrid = new wxFlexGridSizer(YTILES,XTILES,0,0);
	itemSizer1->Add(_mapGrid, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALIGN_CENTER_HORIZONTAL|wxALL, 10 );

    _mapTileArea = new wxPanel( itemDialog1, ID_MAPTILES, wxDefaultPosition, wxSize( XTILES*32, YTILES*32 ) );
  	_mapTileArea->Connect(ID_MAPTILES, wxEVT_PAINT, wxPaintEventHandler(MapWindow::OnPaint), NULL, this);
    _mapGrid->Add(_mapTileArea, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 0 );

    SetMapTiled(true);
}

void MapWindow::OnPaint(wxPaintEvent &event)
{
	wxPaintDC dc(_mapTileArea); 
    for(int y = 0; y < YTILES; y++ )
    {
        for( int x = 0; x < XTILES; x++ )
        {
            if( _currentTiles[y][x][0] != NULL )
            {
                dc.DrawBitmap(*_currentTiles[y][x][0], (x*32), (y*32), true);
            }
            if( _currentTiles[y][x][1] != NULL )
            {
                dc.DrawBitmap(*_currentTiles[y][x][1], (x*32), (y*32), true);
            }
        }
    }
    event.Skip(false);
}

void MapWindow::ProcessMap( wxString text )
{
	if( text.Length() < 1 )
		return;

	int line = 0;
	wxString outText;
	while( 1 )
	{
        int nextpos = text.Find(_(":"));
		if( nextpos == wxNOT_FOUND || nextpos == 0 )
			break;
		outText = text.SubString(0, (nextpos-1));
	    ProcessMapLine(outText); // Could crash if we didn't allocate enough lines.
		text = text.SubString((nextpos+1), (text.Length() - 1));
		line++;
	}
	SetMapTiled(true);
    GetSizer()->Fit(this);
  	Refresh();
}

void MapWindow::ProcessMapLine( wxString text )
{
    int pos = text.Find(wxChar('|'));
    if( pos == wxNOT_FOUND || pos == 0 )
    {
        return;
    }
    int num = atoi(text.SubString(0, (pos-1)).ToAscii());
    text = text.SubString((pos+1), (text.Length() - 1));
    SetMapLine( num, text );
}

void MapWindow::SetMapLine( int line, wxString text )
{
	if( line < 0 || line > YTILES )
	{
		return;
	}

    if( text.length() < XTILES * 2 )
    {
        return;
    }

    for( int i = 0; i < XTILES; i++ )
    {
        _currentTiles[line][i][0] = GetBackgroundBitmap(text[(i*2)]);
        _currentTiles[line][i][1] = GetForegroundBitmap(text[((i*2)+1)]);
    }
}

/**
* Retrieves the foreground bitmap for the specified map character.
*/
wxBitmap* MapWindow::GetForegroundBitmap(wxChar mapCharacter)
{
    if( mapCharacter < MAX_FOREGROUND_TILE + 64 && mapCharacter >= 64)
    {
        return _foregroundTile[mapCharacter-64];
    }

    return _foregroundTile[0];
}

/**
* Retrieves the background bitmap for the specified map character.
*/
wxBitmap* MapWindow::GetBackgroundBitmap(wxChar mapCharacter)
{
    switch( mapCharacter-32 )
    {
    default:
        return _backgroundTile[13];
    case 1: // Inside
        return _backgroundTile[3];
    case 2: // City
        return _backgroundTile[2];
    case 3: // Field
        return _backgroundTile[1];
    case 4: // Forest
        return _backgroundTile[1];
    case 5: // Hills
        return _backgroundTile[14];
    case 6: // Mountain
        return _backgroundTile[14];
    case 7: // Desert
        return _backgroundTile[10];
    case 8: // Arctic
        return _backgroundTile[5];
    case 9: // Swamp
        return _backgroundTile[12];
    case 10: // Road
        return _backgroundTile[3];
    case 11: // Lava
        return _backgroundTile[4];
    case 12: // Glacier
        return _backgroundTile[5];
    case 13: // Tundra
        return _backgroundTile[11];
    case 14: // Jungle
        return _backgroundTile[1];
    case 15: // Swimmable Water
        return _backgroundTile[0];
    case 16: // Unswimmable Water
        return _backgroundTile[8];
    case 17: // Ocean
        return _backgroundTile[8];
    case 18: // Underwater, no ground
        return _backgroundTile[8];
    case 19: // Underwater, has ground
        return _backgroundTile[8];
    case 20: // Air
        return _backgroundTile[0];
    case 21: // Underground, wild
        return _backgroundTile[3];
    case 22: // Underground, city
        return _backgroundTile[3];
    case 23: // Underground, indoors
        return _backgroundTile[3];
    case 24: // Underground, swimmable water
        return _backgroundTile[8];
    case 25: // Underground, unswimmable water
        return _backgroundTile[8];
    case 26: // Underground, no ground
        return _backgroundTile[3];
    case 27: // Underground, impassable
        return _backgroundTile[9];
    case 28: // Underground, ocean
        return _backgroundTile[8];
    case 29: // Underground, frozen
        return _backgroundTile[5];
    case 30: // Plane of fire
        return _backgroundTile[4];
    case 31: // Plane of air
        return _backgroundTile[5];
    case 32: // Plane of earth
        return _backgroundTile[2];
    case 33: // Ethereal plane
        return _backgroundTile[15];
    case 34: // Astral plane
        return _backgroundTile[15];
    }
}

void MapWindow::SetRoomName( wxString name )
{
	wxMudClientDlg::RemoveANSICodes(&name);
	_room->SetLabel( name );
}

void MapWindow::SetExits( wxString exits )
{
    wxMudClientDlg::RemoveANSICodes(&exits);
    _exits->SetLabel( exits );
}

void MapWindow::SetZoneName( wxString name )
{
	wxMudClientDlg::RemoveANSICodes(&name);
    _zone->SetLabel( name );
}

void MapWindow::SetRoomDescription( wxString name )
{
	wxMudClientDlg::RemoveANSICodes(&name);
	_roomDescription->SetLabel( name );
    _roomDescription->Wrap(300);
	SetMapTiled(false);
    GetSizer()->Fit(this);
}

void MapWindow::SetFont( wxFont* font )
{
	_currentFont = font;
}

void MapWindow::SetMapTiled( bool state )
{
	_tiled = state;
	if( !_tiled )
	{
		_mapGrid->Show(false);
        _roomDescription->Show(true);
	}
	else
	{
		_mapGrid->Show(true);
        _roomDescription->Show(false);
	}
}
