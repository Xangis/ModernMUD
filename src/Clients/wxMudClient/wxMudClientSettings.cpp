#include "wx/wx.h"
#include "wxMudClientSettings.h"
#include "wxMudClientDlg.h"

IMPLEMENT_DYNAMIC_CLASS( wxMudClientSettings, wxDialog )

BEGIN_EVENT_TABLE( wxMudClientSettings, wxDialog )
    EVT_RADIOBUTTON( ID_COLOR_ANSI_GRAPHICS, wxMudClientSettings::OnColorAnsiGraphicsSelected )
    EVT_RADIOBUTTON( ID_COLOR_ANSI, wxMudClientSettings::OnColorAnsiSelected )
    EVT_RADIOBUTTON( ID_COLOR_NONE, wxMudClientSettings::OnColorNoneSelected )
    EVT_RADIOBUTTON( ID_COLOR_RAW, wxMudClientSettings::OnColorRawSelected )
    EVT_RADIOBUTTON( ID_LOG_HTML, wxMudClientSettings::OnLogHtmlSelected )
    EVT_RADIOBUTTON( ID_LOG_ANSI, wxMudClientSettings::OnLogAnsiSelected )
    EVT_RADIOBUTTON( ID_LOG_TEXT, wxMudClientSettings::OnLogTextSelected )
    EVT_RADIOBUTTON( ID_LOG_NONE, wxMudClientSettings::OnLogNoneSelected )
	EVT_RADIOBUTTON( ID_MAP_TILED, wxMudClientSettings::OnMapTiled )
	EVT_RADIOBUTTON( ID_MAP_ASCII, wxMudClientSettings::OnMapAscii )
	EVT_CHOICE( ID_INPUT_FONT, wxMudClientSettings::OnInputFont )
	EVT_CHOICE( ID_DISPLAY_FONT, wxMudClientSettings::OnDisplayFont )
    EVT_BUTTON( ID_BUTTON_STATUSCOLOR, wxMudClientSettings::OnStatusColor )
    EVT_BUTTON( ID_BUTTON_STATUSTITLECOLOR, wxMudClientSettings::OnStatusTitleColor )
END_EVENT_TABLE()

/*!
 * wxMudClientSettings constructors
 */
wxMudClientSettings::wxMudClientSettings( )
{
	_inputFontList = NULL;
	_displayFontList = NULL;
    _statusTextColor = NULL;
    _statusTitleColor = NULL;
}

wxMudClientSettings::wxMudClientSettings( wxMudClientDlg* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style )
{
	_parent = parent;
	_inputFontList = NULL;
	_displayFontList = NULL;
    _statusTextColor = NULL;
    _statusTitleColor = NULL;
    Create(parent, id, caption, pos, size, style);
}

/*!
 * wxMudClientSettings creator
 */
bool wxMudClientSettings::Create( wxMudClientDlg* parent, wxWindowID id, const wxString& caption, const wxPoint& pos, const wxSize& size, long style )
{
	_parent = parent;
    SetExtraStyle(GetExtraStyle()|wxWS_EX_BLOCK_EVENTS);
    wxDialog::Create( parent, id, caption, pos, size, style );

    CreateControls();
    GetSizer()->Fit(this);
    GetSizer()->SetSizeHints(this);
    Centre();
    return true;
}

/*!
 * Control creation for wxMudClientSettings
 */
void wxMudClientSettings::CreateControls()
{
    wxMudClientSettings* itemDialog1 = this;

    wxFlexGridSizer* itemFlexGridSizer2 = new wxFlexGridSizer(19, 1, 0, 0);
    itemDialog1->SetSizer(itemFlexGridSizer2);

    wxRadioButton* itemRadioButton3 = new wxRadioButton( itemDialog1, ID_COLOR_ANSI_GRAPHICS, _("ANSI Color With Graphics"), wxDefaultPosition, wxDefaultSize, wxRB_GROUP );
    itemRadioButton3->SetValue(true);
    itemFlexGridSizer2->Add(itemRadioButton3, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxRadioButton* itemRadioButton4 = new wxRadioButton( itemDialog1, ID_COLOR_ANSI, _("ANSI Color"), wxDefaultPosition, wxDefaultSize, 0 );
    itemRadioButton4->SetValue(false);
    itemFlexGridSizer2->Add(itemRadioButton4, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxRadioButton* itemRadioButton5 = new wxRadioButton( itemDialog1, ID_COLOR_NONE, _("No Color"), wxDefaultPosition, wxDefaultSize, 0 );
    itemRadioButton5->SetValue(false);
    itemFlexGridSizer2->Add(itemRadioButton5, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxRadioButton* itemRadioButton6 = new wxRadioButton( itemDialog1, ID_COLOR_RAW, _("Raw Text"), wxDefaultPosition, wxDefaultSize, 0 );
    itemRadioButton6->SetValue(false);
    itemFlexGridSizer2->Add(itemRadioButton6, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxStaticLine* itemStaticLine7 = new wxStaticLine( itemDialog1, wxID_STATIC, wxDefaultPosition, wxDefaultSize, wxLI_HORIZONTAL );
    itemFlexGridSizer2->Add(itemStaticLine7, 0, wxALIGN_CENTER_HORIZONTAL|wxGROW|wxALL, 5);

    wxRadioButton* itemRadioButton8 = new wxRadioButton( itemDialog1, ID_LOG_HTML, _("HTML"), wxDefaultPosition, wxDefaultSize, wxRB_GROUP );
    itemRadioButton8->SetValue(true);
    itemFlexGridSizer2->Add(itemRadioButton8, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxRadioButton* itemRadioButton9 = new wxRadioButton( itemDialog1, ID_LOG_ANSI, _("Raw ANSI"), wxDefaultPosition, wxDefaultSize, 0 );
    itemRadioButton9->SetValue(false);
    itemFlexGridSizer2->Add(itemRadioButton9, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxRadioButton* itemRadioButton10 = new wxRadioButton( itemDialog1, ID_LOG_TEXT, _("Plain Text"), wxDefaultPosition, wxDefaultSize, 0 );
    itemRadioButton10->SetValue(false);
    itemFlexGridSizer2->Add(itemRadioButton10, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxRadioButton* itemRadioButton11 = new wxRadioButton( itemDialog1, ID_LOG_NONE, _("None"), wxDefaultPosition, wxDefaultSize, 0 );
    itemRadioButton11->SetValue(false);
    itemFlexGridSizer2->Add(itemRadioButton11, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxStaticLine* itemStaticLine27 = new wxStaticLine( itemDialog1, wxID_STATIC, wxDefaultPosition, wxDefaultSize, wxLI_HORIZONTAL );
    itemFlexGridSizer2->Add(itemStaticLine27, 0, wxALIGN_CENTER_HORIZONTAL|wxGROW|wxALL, 5);

    wxRadioButton* itemRadioButton28 = new wxRadioButton( itemDialog1, ID_MAP_TILED, _("Tiled Map"), wxDefaultPosition, wxDefaultSize, wxRB_GROUP );
    itemRadioButton28->SetValue(true);
    itemFlexGridSizer2->Add(itemRadioButton28, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

    wxRadioButton* itemRadioButton29 = new wxRadioButton( itemDialog1, ID_MAP_ASCII, _("ASCII Map"), wxDefaultPosition, wxDefaultSize, 0 );
    itemRadioButton29->SetValue(false);
    itemFlexGridSizer2->Add(itemRadioButton29, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5);

	wxStaticLine* itemStaticLine11 = new wxStaticLine( itemDialog1, wxID_STATIC, wxDefaultPosition, wxDefaultSize, wxLI_HORIZONTAL );
    itemFlexGridSizer2->Add(itemStaticLine11, 0, wxALIGN_CENTER_HORIZONTAL|wxGROW|wxALL, 5);

	wxStaticText* itemStaticText12 = new wxStaticText( itemDialog1, wxID_STATIC, _("Input Font") );
    itemFlexGridSizer2->Add(itemStaticText12, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

    wxString fontStr[] = {
        _T("FreeSans.ttf"),
        _T("FreeSansBold.ttf"),
        _T("FreeSansOblique.ttf"),
        _T("FreeSansBoldOblique.ttf"),
        _T("FreeMono.ttf"),
        _T("FreeMonoBold.ttf"),
        _T("FreeMonoOblique.ttf"),
        _T("FreeMonoBoldOblique.ttf"),
        _T("FreeSerif.ttf"),
        _T("FreeSerifBold.ttf"),
        _T("FreeSerifOblique.ttf"),
        _T("FreeSerifBoldOblique.ttf"),
    };
	_inputFontList = new wxChoice( itemDialog1, ID_INPUT_FONT, wxDefaultPosition, wxDefaultSize, 12, fontStr );
	itemFlexGridSizer2->Add(_inputFontList, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_LEFT|wxALL, 5 );
	_inputFontList->SetSelection( 5 );

	wxStaticText* itemStaticText14 = new wxStaticText( itemDialog1, wxID_STATIC, _("Display Font") );
    itemFlexGridSizer2->Add(itemStaticText14, 0, wxALIGN_LEFT|wxALIGN_CENTER_VERTICAL|wxALL, 5 );

	_displayFontList = new wxChoice( itemDialog1, ID_DISPLAY_FONT, wxDefaultPosition, wxDefaultSize, 12, fontStr );
	itemFlexGridSizer2->Add(_displayFontList, 0, wxALIGN_CENTER_VERTICAL|wxALIGN_LEFT|wxALL, 5 );
	_displayFontList->SetSelection( 5 );

    _statusTextColor = new wxButton( itemDialog1, ID_BUTTON_STATUSCOLOR, _("Set Status Text Color"));
    itemFlexGridSizer2->Add( _statusTextColor, 0, wxALIGN_CENTER_HORIZONTAL|wxALL, 5);

    _statusTitleColor = new wxButton( itemDialog1, ID_BUTTON_STATUSTITLECOLOR, _("Set Status Label Color"));
    itemFlexGridSizer2->Add( _statusTitleColor, 0, wxALIGN_CENTER_HORIZONTAL|wxALL, 5);
}

/*!
 * Should we show tooltips?
 */
bool wxMudClientSettings::ShowToolTips()
{
    return true;
}

/*!
 * wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_COLOR_ANSI_GRAPHICS
 */
void wxMudClientSettings::OnColorAnsiGraphicsSelected( wxCommandEvent& event )
{
  _parent->ChangeColorMode(COLORMODE_ANSI_GRAPHICS);
  event.Skip();
}

/*!
 * wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_COLOR_ANSI
 */
void wxMudClientSettings::OnColorAnsiSelected( wxCommandEvent& event )
{
  _parent->ChangeColorMode(COLORMODE_ANSI);
  event.Skip();
}

/*!
 * wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_COLOR_NONE
 */
void wxMudClientSettings::OnColorNoneSelected( wxCommandEvent& event )
{
  _parent->ChangeColorMode(COLORMODE_NONE);
  event.Skip();
}

/*!
 * wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_COLOR_RAW
 */
void wxMudClientSettings::OnColorRawSelected( wxCommandEvent& event )
{
  _parent->ChangeColorMode(COLORMODE_RAW);
  event.Skip();
}

/*!
 * wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_LOG_HTML
 */
void wxMudClientSettings::OnLogHtmlSelected( wxCommandEvent& event )
{
  _parent->ChangeLogMode(LOG_HTML);
  event.Skip();
}

/*!
 * wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_LOG_ANSI
 */
void wxMudClientSettings::OnLogAnsiSelected( wxCommandEvent& event )
{
  _parent->ChangeLogMode(LOG_ANSI);
  event.Skip();
}

/*!
 * wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_LOG_TEXT
 */
void wxMudClientSettings::OnLogTextSelected( wxCommandEvent& event )
{
  _parent->ChangeLogMode(LOG_TEXT);
  event.Skip();
}

/*!
 * wxEVT_COMMAND_RADIOBUTTON_SELECTED event handler for ID_LOG_NONE
 */
void wxMudClientSettings::OnLogNoneSelected( wxCommandEvent& event )
{
  _parent->ChangeLogMode(LOG_NONE);
  event.Skip();
}

void wxMudClientSettings::OnInputFont( wxCommandEvent& event )
{
	_parent->SetInputFont( _inputFontList->GetStringSelection(), 12 );
	event.Skip();
}

void wxMudClientSettings::OnDisplayFont( wxCommandEvent& event )
{
	_parent->SetDisplayFont( _displayFontList->GetStringSelection(), 12 );
	event.Skip();
}

void wxMudClientSettings::OnStatusColor( wxCommandEvent& event )
{
    wxColourData data;
    data.SetChooseFull(true);
    for (int i = 0; i < 16; i++)
    {
        wxColour colour(i*16, i*16, i*16);
        data.SetCustomColour(i, colour);
    }

    wxColourDialog dialog(this, &data);
    if (dialog.ShowModal() == wxID_OK)
    {
        wxColourData retData = dialog.GetColourData();
        wxColour col = retData.GetColour();
        _parent->ChangeStatusColor( col );
    }
    event.Skip();
}

void wxMudClientSettings::OnStatusTitleColor( wxCommandEvent& event )
{
    wxColourData data;
    data.SetChooseFull(true);
    for (int i = 0; i < 16; i++)
    {
        wxColour colour(i*16, i*16, i*16);
        data.SetCustomColour(i, colour);
    }

    wxColourDialog dialog(this, &data);
    if (dialog.ShowModal() == wxID_OK)
    {
        wxColourData retData = dialog.GetColourData();
        wxColour col = retData.GetColour();
        _parent->ChangeStatusTitleColor( col );
    }
    event.Skip();
}

void wxMudClientSettings::OnMapAscii( wxCommandEvent &event )
{
	_parent->SetMapTiled( false );
	event.Skip();
}

void wxMudClientSettings::OnMapTiled( wxCommandEvent &event )
{
	_parent->SetMapTiled( true );
	event.Skip();
}
