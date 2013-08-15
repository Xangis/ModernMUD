#ifndef _HOTKEYEDITOR_H_
#define _HOTKEYEDITOR_H_

#include "wx/wx.h"
#include "HotkeyWindow.h"

#define ID_HOTKEYEDITOR 7000
#define SYMBOL_HOTKEYEDITOR_STYLE wxCAPTION|wxCLOSE_BOX
#define SYMBOL_HOTKEYEDITOR_TITLE _("Edit Hotkey")
#define SYMBOL_HOTKEYEDITOR_IDNAME ID_HOTKEYEDITOR
#define SYMBOL_HOTKEYEDITOR_SIZE wxSize(400,200)
#define SYMBOL_HOTKEYEDITOR_POSITION wxDefaultPosition
#define ID_BUTTONNAME 7001
#define ID_BUTTONTEXT 7002
#define ID_CLOSEBUTTON 7003

/*!
 *  * Compatibility
 *   */
#ifndef wxCLOSE_BOX
#define wxCLOSE_BOX 0x1000
#endif

class HotkeyEditor : public wxDialog
{
    DECLARE_DYNAMIC_CLASS( HotkeyEditor )
    DECLARE_EVENT_TABLE()
public:
    HotkeyEditor( );
    HotkeyEditor( HotkeyWindow* parent, int key, wxWindowID id = SYMBOL_HOTKEYEDITOR_IDNAME, const wxString& caption = SYMBOL_HOTKEYEDITOR_TITLE, const wxPoint& pos = SYMBOL_HOTKEYEDITOR_POSITION, const wxSize& size = SYMBOL_HOTKEYEDITOR_SIZE, long style = SYMBOL_HOTKEYEDITOR_STYLE );
    bool Create( HotkeyWindow* parent, int key, wxWindowID id = SYMBOL_HOTKEYEDITOR_IDNAME, const wxString& caption = SYMBOL_HOTKEYEDITOR_TITLE, const wxPoint& pos = SYMBOL_HOTKEYEDITOR_POSITION, const wxSize& size = SYMBOL_HOTKEYEDITOR_SIZE, long style = SYMBOL_HOTKEYEDITOR_STYLE );
    ~HotkeyEditor();
    void CreateControls();
    void OnClose(wxCommandEvent &event);
	/// Should we show tooltips?
    static bool ShowToolTips();
    wxTextCtrl* _buttonName;
    wxTextCtrl* _buttonText;
private:
    HotkeyWindow* _hotkeyWindow;
    wxButton* _btnClose;
    int _buttonNum;
};

#endif
