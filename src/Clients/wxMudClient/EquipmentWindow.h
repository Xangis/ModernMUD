#ifndef _EQUIPMENTWINDOW_H_
#define _EQUIPMENTWINDOW_H_

#include "wx/wx.h"

#define ID_EQUIPMENTWINDOW 21000
#define SYMBOL_EQUIPMENTWINDOW_STYLE wxCAPTION|wxSYSTEM_MENU|wxCLOSE_BOX
#define SYMBOL_EQUIPMENTWINDOW_TITLE _("Equipment")
#define SYMBOL_EQUIPMENTWINDOW_IDNAME ID_EQUIPMENTWINDOW
#define SYMBOL_EQUIPMENTWINDOW_SIZE wxSize(256, 360)
#define SYMBOL_EQUIPMENTWINDOW_POSITION wxDefaultPosition
#define ID_ITEM1 21001

#define NUMITEMS 20
#define TEXTPIXELSIZE 18

/*!
 *  * Compatibility
 *   */
#ifndef wxCLOSE_BOX
#define wxCLOSE_BOX 0x1000
#endif

class EquipmentWindow : public wxDialog
{
    DECLARE_DYNAMIC_CLASS( EquipmentWindow )
    DECLARE_EVENT_TABLE()
public:
    EquipmentWindow( wxWindow* parent, wxWindowID id = SYMBOL_EQUIPMENTWINDOW_IDNAME, const wxString& caption = SYMBOL_EQUIPMENTWINDOW_TITLE, const wxPoint& pos = SYMBOL_EQUIPMENTWINDOW_POSITION, const wxSize& size = SYMBOL_EQUIPMENTWINDOW_SIZE, long style = SYMBOL_EQUIPMENTWINDOW_STYLE );
    bool Create( wxWindow* parent, wxWindowID id = SYMBOL_EQUIPMENTWINDOW_IDNAME, const wxString& caption = SYMBOL_EQUIPMENTWINDOW_TITLE, const wxPoint& pos = SYMBOL_EQUIPMENTWINDOW_POSITION, const wxSize& size = SYMBOL_EQUIPMENTWINDOW_SIZE, long style = SYMBOL_EQUIPMENTWINDOW_STYLE );
    void CreateControls();
    void ProcessEquipmentData( wxString text );
	void OnPaint(wxPaintEvent &event);
	void OnEraseBackground(wxEraseEvent &event);
    static bool ShowToolTips();
	void AddEquipmentLine(wxString text);
private:
    EquipmentWindow( );
	wxString* _items[NUMITEMS];
	wxBitmap* _background;
};

#endif
