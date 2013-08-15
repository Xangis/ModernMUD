#ifndef _GROUPWINDOW_H_
#define _GROUPWINDOW_H_

#include "wx/wx.h"
#include "wxOwnerDrawStaticBitmap.h"

#define ID_GROUPWINDOW 15000
#define SYMBOL_GROUPWINDOW_STYLE wxCAPTION|wxSYSTEM_MENU|wxCLOSE_BOX
#define SYMBOL_GROUPWINDOW_TITLE _("Group Members")
#define SYMBOL_GROUPWINDOW_IDNAME ID_GROUPWINDOW
#define SYMBOL_GROUPWINDOW_SIZE wxSize(440, 320)
#define SYMBOL_GROUPWINDOW_POSITION wxDefaultPosition
#define ID_NAME1 15001
#define ID_CLASS1 15101
#define ID_LEVEL1 15201
#define ID_HITMETER1 15301
#define ID_MANAMETER1 15401
#define ID_MOVEMETER1 15501

#define MAX_GROUP 8

/*!
 *  * Compatibility
 *   */
#ifndef wxCLOSE_BOX
#define wxCLOSE_BOX 0x1000
#endif

class GroupWindow : public wxDialog
{
    DECLARE_DYNAMIC_CLASS( GroupWindow )
    DECLARE_EVENT_TABLE()
public:
    GroupWindow( wxWindow* parent, wxWindowID id = SYMBOL_GROUPWINDOW_IDNAME, const wxString& caption = SYMBOL_GROUPWINDOW_TITLE, const wxPoint& pos = SYMBOL_GROUPWINDOW_POSITION, const wxSize& size = SYMBOL_GROUPWINDOW_SIZE, long style = SYMBOL_GROUPWINDOW_STYLE );
    bool Create( wxWindow* parent, wxWindowID id = SYMBOL_GROUPWINDOW_IDNAME, const wxString& caption = SYMBOL_GROUPWINDOW_TITLE, const wxPoint& pos = SYMBOL_GROUPWINDOW_POSITION, const wxSize& size = SYMBOL_GROUPWINDOW_SIZE, long style = SYMBOL_GROUPWINDOW_STYLE );
    void CreateControls();
    void ProcessGroupData( wxString text );
    void ProcessGroupMember( wxString text );
    static bool ShowToolTips();
    void UpdateHitMeter(int num);
    void UpdateManaMeter(int num);
    void UpdateMoveMeter(int num);
    void ShowMember(int num, bool show);
private:
    GroupWindow( );
	wxBitmap* _lifebarBitmap;
    wxBitmap* _manabarBitmap;
    wxBitmap* _movebarBitmap;
	wxStaticText* _txtName[MAX_GROUP];
    wxStaticText* _txtLevel[MAX_GROUP];
    wxStaticText* _txtClass[MAX_GROUP];
    int _maxHit[MAX_GROUP];
    int _maxMana[MAX_GROUP];
    int _maxMove[MAX_GROUP];
    int _hit[MAX_GROUP];
    int _mana[MAX_GROUP];
    int _move[MAX_GROUP];
	wxOwnerDrawStaticBitmap* _hitMeter[MAX_GROUP];
	wxOwnerDrawStaticBitmap* _manaMeter[MAX_GROUP];
	wxOwnerDrawStaticBitmap* _moveMeter[MAX_GROUP];
};

#endif
