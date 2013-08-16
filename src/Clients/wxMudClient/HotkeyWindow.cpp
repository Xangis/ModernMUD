#include "wx/wx.h"
#include "HotkeyWindow.h"
#include "HotkeyEditor.h"
#include "wxMudClientDlg.h"

IMPLEMENT_DYNAMIC_CLASS( HotkeyWindow, wxDialog )

BEGIN_EVENT_TABLE( HotkeyWindow, wxDialog )
EVT_BUTTON(ID_FIRSTBUTTON, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+1, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+2, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+3, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+4, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+5, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+6, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+7, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+8, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+9, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+10, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+11, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+12, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+13, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+14, HotkeyWindow::OnHotkeyButton )
EVT_BUTTON(ID_FIRSTBUTTON+15, HotkeyWindow::OnHotkeyButton )
END_EVENT_TABLE()

HotkeyWindow::HotkeyWindow()
{
}

HotkeyWindow::~HotkeyWindow()
{
}

HotkeyWindow::HotkeyWindow(wxMudClientDlg* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    Create(parent, id, caption, pos, size, style);
}

bool HotkeyWindow::Create(wxMudClientDlg* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
	_parent = parent;
	SetExtraStyle(GetExtraStyle()|wxWS_EX_BLOCK_EVENTS);
	wxDialog::Create( (wxDialog *)parent, id, caption, pos, size, style );
    CreateControls();
    GetSizer()->Fit(this);
    GetSizer()->SetSizeHints(this);
    Centre();
    SetTransparent(240);
    wxTopLevelWindow::SetTransparent(240);
    return true;
}

void HotkeyWindow::CreateControls()
{
#ifdef __WXMAC__
    // Required for the background color to take.
    wxPanel* itemDialog1 = new wxPanel(this);
    wxBoxSizer* mainSizer = new wxBoxSizer(wxHORIZONTAL);
    this->SetSizer(mainSizer);
    mainSizer->Add(itemDialog1);
#else
    // On Windows, using an extra wxPanel adds unnecessary drawing flicker.
    HotkeyWindow* itemDialog1 = this;
#endif

    itemDialog1->SetForegroundColour( *wxWHITE );
    itemDialog1->SetBackgroundColour( *wxBLACK );

    wxFlexGridSizer* itemFlexGridSizer2 = new wxFlexGridSizer(HOTKEYROWS, HOTKEYCOLUMNS, 0, 0);
    itemDialog1->SetSizer(itemFlexGridSizer2);

	int x;
	int y;
	for( y = 0; y < HOTKEYROWS; y++ )
	{
		for( x = 0; x < HOTKEYCOLUMNS; x++ )
		{
			_hotkeys[y][x] = new wxButton( itemDialog1, ID_FIRSTBUTTON + (y*HOTKEYCOLUMNS+x), _("(none)"), wxDefaultPosition, wxSize(60,24));
			itemFlexGridSizer2->Add(_hotkeys[y][x], 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 2 );
			_hotkeys[y][x]->SetForegroundColour( *wxWHITE );
			_hotkeys[y][x]->SetBackgroundColour( *wxBLACK );
            _hotkeys[y][x]->Connect(ID_FIRSTBUTTON + (y*HOTKEYCOLUMNS+x), wxEVT_RIGHT_DOWN, wxMouseEventHandler(HotkeyWindow::OnButtonRightClick), NULL, this);
		}
	}
}

/*!
 * Should we show tooltips?
 */
bool HotkeyWindow::ShowToolTips()
{
    return true;
}

void HotkeyWindow::OnHotkeyButton(wxCommandEvent &event)
{
	int buttonNumber = event.GetId() - ID_FIRSTBUTTON;
	int row = buttonNumber / HOTKEYCOLUMNS;
	int column = buttonNumber % HOTKEYCOLUMNS;
    if( _hotkeyString[row][column].Length() > 0 )
    {
	    wxString cmd = _hotkeyString[row][column];
	    if( cmd != _("") )
	    {
			_parent->SendText((wxString::Format(_("%s%s"), cmd.c_str(), _("\n") )), cmd.Length() + 1);
        }
    }
}

void HotkeyWindow::OnButtonRightClick(wxMouseEvent &event)
{
    int buttonNumber = event.GetId() - ID_FIRSTBUTTON;
    HotkeyEditor* wnd = new HotkeyEditor(this, buttonNumber);
	int row = buttonNumber / HOTKEYCOLUMNS;
	int column = buttonNumber % HOTKEYCOLUMNS;
    if( _hotkeyString[row][column].Length() > 0 )
	{
	    wnd->_buttonText->SetLabel(_hotkeyString[row][column]);
		wnd->_buttonName->SetLabel(_hotkeys[row][column]->GetLabel());
	}
    wnd->Show();
}

void HotkeyWindow::SetHotkey( int num, wxString command, wxString title )
{
    int row = num / HOTKEYCOLUMNS;
    int column = num % HOTKEYCOLUMNS;
    _hotkeys[row][column]->SetLabel( title );
    _hotkeyString[row][column] = command;
    _parent->CreateAlias( wxString::Format(_("button%d"), num), command);
    _parent->CreateAlias( wxString::Format(_("button%dname"), num), title);
    Refresh();
}
