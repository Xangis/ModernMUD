#ifndef _HOTKEYWINDOW_H_
#define _HOTKEYWINDOW_H_

#include "wx/wx.h"

#define ID_HOTKEYWINDOW 3000
#define SYMBOL_HOTKEYWINDOW_STYLE wxCAPTION|wxSYSTEM_MENU|wxCLOSE_BOX
#define SYMBOL_HOTKEYWINDOW_TITLE _("Hotkeys")
#define SYMBOL_HOTKEYWINDOW_IDNAME ID_HOTKEYWINDOW
#define SYMBOL_HOTKEYWINDOW_SIZE wxSize(400, 300)
#define SYMBOL_HOTKEYWINDOW_POSITION wxDefaultPosition
#define ID_FIRSTBUTTON 3002

#define HOTKEYROWS 2
#define HOTKEYCOLUMNS 8

class wxMudClientDlg;

/*!
 *  * Compatibility
 *   */
#ifndef wxCLOSE_BOX
#define wxCLOSE_BOX 0x1000
#endif

class HotkeyWindow : public wxDialog
{
    DECLARE_DYNAMIC_CLASS( HotkeyWindow )
    DECLARE_EVENT_TABLE()
public:
    ~HotkeyWindow();
    HotkeyWindow( wxMudClientDlg* parent, wxWindowID id = SYMBOL_HOTKEYWINDOW_IDNAME, const wxString& caption = SYMBOL_HOTKEYWINDOW_TITLE, const wxPoint& pos = SYMBOL_HOTKEYWINDOW_POSITION, const wxSize& size = SYMBOL_HOTKEYWINDOW_SIZE, long style = SYMBOL_HOTKEYWINDOW_STYLE );
    bool Create( wxMudClientDlg* parent, wxWindowID id = SYMBOL_HOTKEYWINDOW_IDNAME, const wxString& caption = SYMBOL_HOTKEYWINDOW_TITLE, const wxPoint& pos = SYMBOL_HOTKEYWINDOW_POSITION, const wxSize& size = SYMBOL_HOTKEYWINDOW_SIZE, long style = SYMBOL_HOTKEYWINDOW_STYLE );
    void CreateControls();
    void SetHotkey( int num, wxString command, wxString title );
    static bool ShowToolTips();
	void OnHotkeyButton(wxCommandEvent &event);
    void OnButtonRightClick(wxMouseEvent &event);
private:
    HotkeyWindow();
	wxMudClientDlg * _parent;
	wxButton* _hotkeys[HOTKEYROWS][HOTKEYCOLUMNS];
	wxString _hotkeyString[HOTKEYROWS][HOTKEYCOLUMNS];
};

#endif
