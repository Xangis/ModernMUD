#ifndef _WXBASTSETTINGS_H_
#define _WXBASTSETTINGS_H_

#include "wx/statline.h"
#include "wx/colordlg.h"
#include "Modes.h"
#include "MudSettings.h"

class wxMudClientDlg;

#define ID_SETTINGS 11000
#define SYMBOL_SETTINGS_STYLE wxCAPTION|wxSYSTEM_MENU|wxCLOSE_BOX
#define SYMBOL_SETTINGS_TITLE (MUD_NAME + wxString(_(" Client Settings")))
#define SYMBOL_SETTINGS_IDNAME ID_SETTINGS
#define SYMBOL_SETTINGS_SIZE wxSize(400, 300)
#define SYMBOL_SETTINGS_POSITION wxDefaultPosition
#define ID_COLOR_ANSI_GRAPHICS 11001
#define ID_COLOR_ANSI 11002
#define ID_COLOR_NONE 11003
#define ID_COLOR_RAW 11004
#define ID_LOG_HTML 11005
#define ID_LOG_ANSI 11006
#define ID_LOG_TEXT 11007
#define ID_LOG_NONE 11008
#define ID_INPUT_FONT 11009
#define ID_DISPLAY_FONT 11010
#define ID_BUTTON_STATUSCOLOR 11011
#define ID_BUTTON_STATUSTITLECOLOR 11012
#define ID_MAP_ASCII 11013
#define ID_MAP_TILED 11014

/*!
 * Compatibility
 */
#ifndef wxCLOSE_BOX
#define wxCLOSE_BOX 0x1000
#endif

/*!
 * wxMudClientSettings class declaration
 */
class wxMudClientSettings: public wxDialog
{
    DECLARE_DYNAMIC_CLASS( wxMudClientSettings )
    DECLARE_EVENT_TABLE()

public:
    /// Constructors
    wxMudClientSettings( wxMudClientDlg* parent, wxWindowID id = SYMBOL_SETTINGS_IDNAME, const wxString& caption = SYMBOL_SETTINGS_TITLE, const wxPoint& pos = SYMBOL_SETTINGS_POSITION, const wxSize& size = SYMBOL_SETTINGS_SIZE, long style = SYMBOL_SETTINGS_STYLE );

    /// Creation
    bool Create( wxMudClientDlg* parent, wxWindowID id = SYMBOL_SETTINGS_IDNAME, const wxString& caption = SYMBOL_SETTINGS_TITLE, const wxPoint& pos = SYMBOL_SETTINGS_POSITION, const wxSize& size = SYMBOL_SETTINGS_SIZE, long style = SYMBOL_SETTINGS_STYLE );

    /// Creates the controls and sizers
    void CreateControls();

    /// wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_COLOR_ANSI_GRAPHICS
    void OnColorAnsiGraphicsSelected( wxCommandEvent& event );
    /// wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_COLOR_ANSI
    void OnColorAnsiSelected( wxCommandEvent& event );
    /// wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_COLOR_NONE
    void OnColorNoneSelected( wxCommandEvent& event );
    /// wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_COLOR_RAW
    void OnColorRawSelected( wxCommandEvent& event );
    /// wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_LOG_HTML
    void OnLogHtmlSelected( wxCommandEvent& event );
    /// wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_LOG_ANSI
    void OnLogAnsiSelected( wxCommandEvent& event );
    /// wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_LOG_TEXT
    void OnLogTextSelected( wxCommandEvent& event );
    /// wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_LOG_NONE
    void OnLogNoneSelected( wxCommandEvent& event );

	void OnInputFont( wxCommandEvent& event );
	void OnDisplayFont( wxCommandEvent& event );
    void OnStatusColor( wxCommandEvent& event );
    void OnStatusTitleColor( wxCommandEvent& event );
	void OnMapAscii( wxCommandEvent& event );
	void OnMapTiled( wxCommandEvent& event );

	/// Should we show tooltips?
    static bool ShowToolTips();
private:
    wxMudClientSettings( );
	wxMudClientDlg* _parent;
	wxChoice* _displayFontList;
	wxChoice* _inputFontList;
    wxButton* _statusTextColor;
    wxButton* _statusTitleColor;
};

#endif

