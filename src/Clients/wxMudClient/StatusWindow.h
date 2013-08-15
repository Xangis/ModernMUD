#ifndef _STATUSWINDOW_H_
#define _STATUSWINDOW_H_

// Prompt States for last indicator type.  These will probably need to change.
#define PROMPT_NONE			0
#define PROMPT_BEGIN		1		// Initial left arrow
#define PROMPT_CURRENTHP	2
#define PROMPT_MAXHP		3
#define PROMPT_CURRENTMOVES 4
#define PROMPT_MAXMOVES		5
#define PROMPT_POSITION		6		// "R:"
#define PROMPT_TANKNAME		7		// "T:"
#define PROMPT_TANKPOS		8		// "V:"
#define PROMPT_TANKCOND		9		// "U:"
#define PROMPT_ENEMYNAME	10		// "E:"
#define PROMPT_ENEMYPOS		11		// "G:"
#define PROMPT_ENEMYCOND	12		// "F:"
#define PROMPT_END			13		// Trailing right arrow '>'
#define PROMPT_BRACKETED    14		// "<" has been seen.
#define PROMPT_PLAYERNAME   15		// Data not used
#define PROMPT_PLAYERCOND   16      // Data not used

#define STATUS_STRING_LENGTH 128
#define LABEL_PIXEL_WIDTH 120
#define MAX_STATUS_ITEM_LENGTH 64

#define ID_STATUSWINDOW 15000
#define SYMBOL_STATUSWINDOW_STYLE wxCAPTION|wxSYSTEM_MENU|wxCLOSE_BOX
#define SYMBOL_STATUSWINDOW_TITLE _("Status")
#define SYMBOL_STATUSWINDOW_IDNAME ID_STATUSWINDOW
#define SYMBOL_STATUSWINDOW_SIZE wxSize(400, 320)
#define SYMBOL_STATUSWINDOW_POSITION wxDefaultPosition
#include "ID.h"
#include "wxOwnerDrawStaticBitmap.h"

/*!
 *  * Compatibility
 *   */
#ifndef wxCLOSE_BOX
#define wxCLOSE_BOX 0x1000
#endif

#include "wx/wx.h"

class StatusWindow : public wxDialog
{
    DECLARE_DYNAMIC_CLASS( StatusWindow )
    DECLARE_EVENT_TABLE()
public:
    StatusWindow( wxWindow* parent, wxWindowID id = SYMBOL_STATUSWINDOW_IDNAME, const wxString& caption = SYMBOL_STATUSWINDOW_TITLE, const wxPoint& pos = SYMBOL_STATUSWINDOW_POSITION, const wxSize& size = SYMBOL_STATUSWINDOW_SIZE, long style = SYMBOL_STATUSWINDOW_STYLE );
    bool Create( wxWindow* parent, wxWindowID id = SYMBOL_STATUSWINDOW_IDNAME, const wxString& caption = SYMBOL_STATUSWINDOW_TITLE, const wxPoint& pos = SYMBOL_STATUSWINDOW_POSITION, const wxSize& size = SYMBOL_STATUSWINDOW_SIZE, long style = SYMBOL_STATUSWINDOW_STYLE );
    void CreateControls();
	~StatusWindow();
	void setHits( int hits );
	void setEnemyHits( int hits );
	void setTankHits( int hits );
	void setMana( int mana );
	void setMoves( int moves );
	void setMaxHits( int hits );
	void setMaxMoves( int moves );
	void setMaxMana( int mana );
	void setTankPosition( wxString position );
	void setPlayerPosition( wxString position );
	void setEnemyPosition( wxString position );
	void setTankName( wxString name );
	void setEnemyName( wxString name );
	void setTankCond( wxString cond );
	void setEnemyCond( wxString cond );
	void setPlayerCond( wxString cond );
	void setFont(wxFont* font);
    void SetStatusColor( int r, int g, int b );
    void SetStatusTitleColor( int r, int g, int b );
	void UpdateHitMeter();
	void UpdateManaMeter();
	void UpdateMoveMeter();
	void ProcessPrompt( wxString output );
    bool _haveTank; // Used for clearing tank info from status when not in combat.
    bool _haveEnemy; // Used for clearing enemy info from status when not in combat.
	int ExtractNumberFromString(wxString string, unsigned int* position);
private:
	StatusWindow();
	int iHits;
	int iMana;
	int iMoves;
	int iMaxHits;
	int iMaxMana;
	int iMaxMoves;
	int iEnemyHits;
	int iTankHits;
	wxColour _statusTextColor;
    wxColour _statusTitleColor;
	wxFont * _ttfStatusFont;
	wxOwnerDrawStaticBitmap* _hitMeter;
	wxOwnerDrawStaticBitmap* _manaMeter;
	wxOwnerDrawStaticBitmap* _moveMeter;
	wxOwnerDrawStaticBitmap* _enemyCond;
	wxOwnerDrawStaticBitmap* _tankCond;
	wxStaticText* _tankPos;
	wxStaticText* _enemyPos;
	wxStaticText* _playerPos;
	wxStaticText* _tankName;
	wxStaticText* _enemyName;
	wxBitmap* _lifebarBitmap;
    wxBitmap* _manabarBitmap;
    wxBitmap* _movebarBitmap;
};

#endif
