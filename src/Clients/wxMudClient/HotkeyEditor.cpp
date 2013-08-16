#include "wx/wx.h"
#include "HotkeyEditor.h"

IMPLEMENT_DYNAMIC_CLASS( HotkeyEditor, wxDialog )

BEGIN_EVENT_TABLE( HotkeyEditor, wxDialog )
EVT_BUTTON(ID_CLOSEBUTTON, HotkeyEditor::OnClose)
END_EVENT_TABLE()

HotkeyEditor::HotkeyEditor()
{
}

HotkeyEditor::~HotkeyEditor()
{
}

HotkeyEditor::HotkeyEditor(HotkeyWindow* parent, int key, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    Create(parent, key, id, caption, pos, size, style);
}

bool HotkeyEditor::Create(HotkeyWindow* parent, int key, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style)
{
    _hotkeyWindow = parent;
    _buttonNum = key;
	wxDialog::Create( parent, id, caption, pos, size, style );
    CreateControls();
    GetSizer()->Fit(this);
    GetSizer()->SetSizeHints(this);
    Centre();
    SetTransparent(240);
    wxTopLevelWindow::SetTransparent(240);
    return true;
}

void HotkeyEditor::CreateControls()
{
    HotkeyEditor* itemDialog1 = this;
    this->SetForegroundColour( *wxWHITE );
    this->SetBackgroundColour( *wxBLACK );

    wxBoxSizer* itemSizer1 = new wxBoxSizer(wxVERTICAL);
    itemDialog1->SetSizer(itemSizer1);

    wxBoxSizer* itemSizer3 = new wxBoxSizer(wxHORIZONTAL);
    itemSizer1->Add(itemSizer3, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_CENTER, 5);

    wxStaticText* static2 = new wxStaticText(itemDialog1, wxID_STATIC, _("Label"), wxDefaultPosition, wxDefaultSize );
    itemSizer3->Add(static2, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_CENTER, 0);

    _buttonName = new wxTextCtrl(itemDialog1, ID_BUTTONNAME, wxEmptyString, wxDefaultPosition, wxSize(128,24));
    itemSizer3->Add(_buttonName, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_CENTER, 0);

    wxBoxSizer* itemSizer4 = new wxBoxSizer(wxHORIZONTAL);
    itemSizer1->Add(itemSizer4, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_CENTER, 5);

    wxStaticText* static3 = new wxStaticText(itemDialog1, wxID_STATIC, _("Command"), wxDefaultPosition, wxDefaultSize );
    itemSizer4->Add(static3, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_CENTER, 0);

    _buttonText = new wxTextCtrl(itemDialog1, ID_BUTTONTEXT, wxEmptyString, wxDefaultPosition, wxSize(128,24));
    itemSizer4->Add(_buttonText, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_CENTER, 0);

    _btnClose = new wxButton(itemDialog1, ID_CLOSEBUTTON, _("Save and Close"), wxDefaultPosition, wxDefaultSize );
    itemSizer1->Add(_btnClose, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_CENTER,5);
}

/*!
 * Should we show tooltips?
 */
bool HotkeyEditor::ShowToolTips()
{
    return true;
}

void HotkeyEditor::OnClose( wxCommandEvent & )
{
    _hotkeyWindow->SetHotkey( _buttonNum, _buttonText->GetValue(), _buttonName->GetValue() );
    Destroy();
}
