#ifndef _MAPWINDOW_H_
#define _MAPWINDOW_H_

#define MAX_BACKGROUND_TILE 16
#define MAX_FOREGROUND_TILE 85
#define XTILES 9
#define YTILES 9

#define ID_MAPWINDOW 19000
#define SYMBOL_MAPWINDOW_STYLE wxCAPTION|wxSYSTEM_MENU|wxCLOSE_BOX
#define SYMBOL_MAPWINDOW_TITLE _("Map")
#define SYMBOL_MAPWINDOW_IDNAME ID_MAPWINDOW
#define SYMBOL_MAPWINDOW_SIZE wxSize(420, 400)
#define SYMBOL_MAPWINDOW_POSITION wxDefaultPosition

#include "wx/wx.h"
#include "wx/richtext/richtextctrl.h"

class MapWindow : public wxDialog
{
    DECLARE_DYNAMIC_CLASS( MapWindow )
    DECLARE_EVENT_TABLE()
public:
    MapWindow( wxWindow* parent, wxWindowID id = SYMBOL_MAPWINDOW_IDNAME, const wxString& caption = SYMBOL_MAPWINDOW_TITLE, const wxPoint& pos = SYMBOL_MAPWINDOW_POSITION, const wxSize& size = SYMBOL_MAPWINDOW_SIZE, long style = SYMBOL_MAPWINDOW_STYLE );
    bool Create( wxWindow* parent, wxWindowID id = SYMBOL_MAPWINDOW_IDNAME, const wxString& caption = SYMBOL_MAPWINDOW_TITLE, const wxPoint& pos = SYMBOL_MAPWINDOW_POSITION, const wxSize& size = SYMBOL_MAPWINDOW_SIZE, long style = SYMBOL_MAPWINDOW_STYLE );
	~MapWindow();
	void SetRoomName( wxString name );
	void SetZoneName( wxString name );
    void OnPaint(wxPaintEvent &event);
    void SetExits( wxString exits );
    void CreateControls();
	void ProcessMap( wxString text);
	void SetFont( wxFont* font );
	void SetMapLine( int line, wxString text );
	void SetMapTiled( bool state );
	void SetRoomDescription( wxString name );
    void ProcessMapLine( wxString text );
    wxBitmap* GetBackgroundBitmap(wxChar mapCharacter);
    wxBitmap* GetForegroundBitmap(wxChar mapCharacter);
private:
	MapWindow();
	wxBitmap * _backgroundTile[MAX_BACKGROUND_TILE];
    wxBitmap * _foregroundTile[MAX_FOREGROUND_TILE];
    wxBitmap * _currentTiles[YTILES][XTILES][2];
	wxStaticText* _zone;
	wxStaticText* _room;
    wxStaticText* _exits;
	wxStaticText* _roomDescription;
    wxPanel* _mapTileArea;
	wxFont* _currentFont;
	wxColor _color;
	wxColor _titleColor;
	bool _zoneMode; // Are we in a zone or are we on the worldmap?
	bool _tiled;
	wxFlexGridSizer* _mapGrid;
};

#endif
