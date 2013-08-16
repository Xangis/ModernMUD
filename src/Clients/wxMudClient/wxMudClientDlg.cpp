#include "wxMudClientDlg.h"
#include "ID.h"
#include "Colors.h"
#include "wx/tokenzr.h"
#include "wx/wfstream.h"
#include "wx/aboutdlg.h"
#include "B_16x16.xpm"
#include "MudSettings.h"

inline void wxMudClientDlg::OnFileExit(wxCommandEvent &) { Close(); }

IMPLEMENT_CLASS(wxMudClientDlg, wxFrame)

BEGIN_EVENT_TABLE(wxMudClientDlg, wxFrame)
    EVT_MENU(IDM_FILE_EXIT, wxMudClientDlg::OnFileExit)
	EVT_MENU(IDM_FILE_CONNECT, wxMudClientDlg::OnFileConnect )
	EVT_MENU(IDM_FILE_CONNECT_LOCALHOST, wxMudClientDlg::OnConnectLocalhost )
	EVT_MENU(IDM_FILE_DISCONNECT, wxMudClientDlg::OnFileDisconnect )
    EVT_MENU(IDM_HELP_ABOUT, wxMudClientDlg::OnHelpAbout)
	EVT_MENU(IDM_FILE_SAVE_ALIASES, wxMudClientDlg::OnSaveAliases )
	EVT_MENU(IDM_FILE_LOAD_ALIASES, wxMudClientDlg::OnLoadAliases )
	EVT_MENU(IDM_VIEW_SETTINGS, wxMudClientDlg::OnViewSettings)
    EVT_MENU(IDM_VIEW_GROUP, wxMudClientDlg::OnViewGroup )
    EVT_MENU(IDM_VIEW_MAP, wxMudClientDlg::OnViewMap )
    EVT_MENU(IDM_VIEW_STATUS, wxMudClientDlg::OnViewStatus )
	EVT_MENU(IDM_VIEW_HOTKEYS, wxMudClientDlg::OnViewHotkeys )
    EVT_MENU(IDM_VIEW_EQUIPMENT, wxMudClientDlg::OnViewEquipment )
	EVT_COMMAND(IDM_TEXT_UPDATE, wxEVT_NULL, wxMudClientDlg::FlushText )
	EVT_SIZE(wxMudClientDlg::OnSize)
END_EVENT_TABLE()

wxMudClientDlg::wxMudClientDlg()
{
    _done = false;
	_txtInput = NULL;
	_txtOutput = NULL;
	_panel = NULL;
    _commandLocation = -1;
	wxInitAllImageHandlers();
    // Create the wxMudClientDlg
	wxFrame::Create(NULL, ID_FRAME, MUD_NAME + wxString(_(" Client")), wxPoint(326,2),
           wxDefaultSize, wxCAPTION | wxSYSTEM_MENU | wxRESIZE_BORDER | wxMAXIMIZE_BOX |
           wxMINIMIZE_BOX | wxCLOSE_BOX);

    // create the main menubar
    wxMenuBar *mb = new wxMenuBar;

    // create the file menu
    wxMenu *fileMenu = new wxMenu;
    fileMenu->Append(IDM_FILE_SAVE_ALIASES, wxT("&Save Aliases"));
    fileMenu->Append(IDM_FILE_LOAD_ALIASES, wxT("&Load Aliases"));
	fileMenu->Append(IDM_FILE_CONNECT, wxT("&Connect"));
	fileMenu->Append(IDM_FILE_CONNECT_LOCALHOST, wxT("&Connect to Localhost"));
	fileMenu->Append(IDM_FILE_DISCONNECT, wxT("&Disconnect"));
    fileMenu->Append(IDM_FILE_EXIT, wxT("E&xit"));

    // add the file menu to the menu bar
    mb->Append(fileMenu, wxT("&File"));

    // Create the view menu.
    wxMenu *viewMenu = new wxMenu;
    viewMenu->Append(IDM_VIEW_GROUP, wxT("&Group" ));
    viewMenu->Append(IDM_VIEW_EQUIPMENT, wxT("&Equipment"));
    viewMenu->Append(IDM_VIEW_MAP, wxT("&Map"));
    viewMenu->Append(IDM_VIEW_STATUS, wxT("&Status"));
	// Disabled:  None of the settings actually do anything.
	//viewMenu->Append(IDM_VIEW_SETTINGS, wxT("Se&ttings" ));
	viewMenu->Append(IDM_VIEW_HOTKEYS, wxT("Hot&keys" ));

    mb->Append(viewMenu, wxT("&View" ));

    // Create the help menu
    wxMenu *helpMenu = new wxMenu;
    helpMenu->Append(IDM_HELP_ABOUT, wxT("&About"));

    // add the help menu to the menu bar
    mb->Append(helpMenu, wxT("&Help"));

    // add the menu bar to the wxMudClientDlg
    SetMenuBar(mb);

	_panel = new wxPanel(this, ID_DLG_MAIN, wxDefaultPosition, wxSize(671, 587) );

	wxBoxSizer* sizer = new wxBoxSizer(wxVERTICAL);

	_txtOutput = new wxRichTextCtrl(_panel, ID_TXT_OUTPUT, wxEmptyString, wxDefaultPosition, wxSize(631, 547));
	_txtOutput->SetForegroundColour(clrWhite);
	_txtOutput->SetBackgroundColour(wxColour(0,0,0));
	_txtOutput->SetEditable(false);
	_foregroundColor = &clrWhite;
	_backgroundColor = &clrBlack;
	sizer->Add(_txtOutput, 0, wxALL, 2);
	_outputFont = new wxFont(10, wxTELETYPE, wxNORMAL, wxNORMAL, false, _("Courier"));
	_txtOutput->SetFont(*_outputFont);

	_txtInput = new wxTextCtrl(_panel, ID_TXT_INPUT, wxEmptyString, wxDefaultPosition, wxSize(660, 24), wxTE_PROCESS_ENTER);
	_txtInput->Connect(wxID_ANY, wxEVT_CHAR, wxKeyEventHandler( wxMudClientDlg::OnKey), NULL, this);
	sizer->Add(_txtInput, 0, wxALL, 2);
	_panel->SetSizer(sizer);

    // Set window icon.
	//wxIcon icon;
	//if( icon.LoadFile(_T("IconName.ico"), wxBITMAP_TYPE_ICO ))
	//{
	//	SetIcon(icon);
	//}

	SetTransparent(240);
	_connection = 0;
	// Create windows and set default positions.
	_SettingsDlg =  new wxMudClientSettings(this);
	_groupWindow = new GroupWindow(this);
	_groupWindow->Move(1012, 405);
    _equipmentWindow = new EquipmentWindow(this);
	_equipmentWindow->Move(1012, 6);
    _mapWindow = new MapWindow(this);
	_mapWindow->Move(6, 314);
    _statusWindow = new StatusWindow(this);
	_statusWindow->Move(148, 6);
	_hotkeyWindow = new HotkeyWindow(this);
	_hotkeyWindow->Move(400, 680);
	_mccpEnabled = false;
#ifdef WIN32
	// Start Sockets
	WSADATA wsaData;
	WORD wVersionRequested = MAKEWORD( 1, 1 );
	WSAStartup( wVersionRequested, &wsaData );
#endif
	_hostname = HOSTNAME;
	_parseMode = MODE_NONE;
	_port = PORT;
	_connected = false;
	_unclosedTag = false;
	_logMode = LOG_NONE;
	_logFile.open( "LogFile.txt" );
	_colorMode = COLORMODE_ANSI;
	_foregroundColor = &clrWhite;
	_backgroundColor = &clrBlack;
	_aliasData = new wxFileConfig;
	wxThread::Create();
	Run();
	_txtInput->SetFocus();
	_pendingStringOutputMutex = new wxMutex;
	_pendingStringOutputs = new std::deque<StringEntry>;
	_txtOutput->BeginParagraphSpacing(4,4);
	_txtOutput->BeginLineSpacing(4);
	_statusWindow->Show();
	_mapWindow->Show();
	_groupWindow->Show();
	_equipmentWindow->Show();
	_hotkeyWindow->Show();
	_icon = wxIcon(B_16x16_xpm);
	SetIcon(_icon);
}

void wxMudClientDlg::AddStringEvent(wxString text, bool newline, wxColour color )
{
    if( text.Length() == 0 && !newline )
    {
        return;
    }
	StringEntry entry = StringEntry(text, newline, color);
	_pendingStringOutputMutex->Lock();
	_pendingStringOutputs->push_back(entry);
	_pendingStringOutputMutex->Unlock();
	wxCommandEvent event(wxEVT_NULL, IDM_TEXT_UPDATE);
	_txtOutput->GetEventHandler()->AddPendingEvent(event);
}

void wxMudClientDlg::OnHelpAbout(wxCommandEvent &)
{
    wxAboutDialogInfo info;
    info.SetName(MUD_NAME + wxString(_(" Client")));
    info.SetVersion(_("1.0"));
    info.SetCopyright(_("(c) 2005-2013 Zeta Centauri.\nhttp://zetacentauri.com"));
	info.AddDeveloper(_("Code by Jason Champion"));
	info.SetIcon(_icon);
	info.SetWebSite(MUD_WEBSITE);
#ifdef _DEBUG
	wxString str = wxString::Format(_("Main Pos: %d, %d\nMain Size: %d, %d\nMap Pos: %d, %d\nStatus Pos: %d, %d\nGroup Pos: %d, %d\nHotkey Pos: %d, %d\nEquipment Pos: %d, %d"),
		this->GetPosition().x, this->GetPosition().y,
		this->GetSize().x, this->GetSize().y,
		_mapWindow->GetPosition().x, _mapWindow->GetPosition().y,
		_statusWindow->GetPosition().x, _statusWindow->GetPosition().y,
		_groupWindow->GetPosition().x, _groupWindow->GetPosition().y,
		_hotkeyWindow->GetPosition().x, _hotkeyWindow->GetPosition().y,
		_equipmentWindow->GetPosition().x, _equipmentWindow->GetPosition().y);
	info.SetDescription(str);
#else
	info.SetDescription(wxString(_("The ")) + MUD_NAME + wxString(_(" Client uses the wxWidgets (http://www.wxwidgets.org) libraries.")));
#endif

    wxAboutBox(info);
}

void wxMudClientDlg::OnFileConnect(wxCommandEvent& )
{
	ShowConnectionString(wxString::Format(_("Connecting to %s port %d..."), HOSTNAME, PORT ));
	ConnectToMyMUD();
    _txtInput->SetFocus();
}

void wxMudClientDlg::OnConnectLocalhost(wxCommandEvent& )
{
	ShowConnectionString(wxString::Format(_("Connecting to localhost port %d..."), PORT ));
	ConnectToLocalhost();
    _txtInput->SetFocus();
}

void wxMudClientDlg::OnSize(wxSizeEvent& event )
{
	wxSize size = event.GetSize();
#ifdef WIN32
	if( _panel != NULL )
	{
		_panel->SetSize(size.GetX() - 18, size.GetY() - 54 );
	}
	if( _txtOutput != NULL )
	{
		_txtOutput->SetSize(size.GetX() - 18, size.GetY() - 78);
	}
	if( _txtInput != NULL )
	{
		_txtInput->SetSize(size.GetX() - 18, 24);
		_txtInput->SetPosition(wxPoint(2, size.GetY() - 77));
	}
#else
	if( _panel != NULL )
	{
		_panel->SetSize(size.GetX() - 5, size.GetY() - 30 );
	}
	if( _txtOutput != NULL )
	{
		_txtOutput->SetSize(size.GetX() - 5, size.GetY() - 54);
	}
	if( _txtInput != NULL )
	{
		_txtInput->SetSize(size.GetX() - 5, 24);
		_txtInput->SetPosition(wxPoint(2, size.GetY() - 52));
	}
#endif
}

void wxMudClientDlg::OnFileDisconnect(wxCommandEvent& )
{
	Disconnect();
}

void wxMudClientDlg::OnViewSettings(wxCommandEvent& )
{
	if( !_SettingsDlg )
	{
		_SettingsDlg = new wxMudClientSettings(this);
	}
	_SettingsDlg->Show();
}

void wxMudClientDlg::OnLoadAliases(wxCommandEvent& )
{
	LoadAliases();
}

void wxMudClientDlg::OnSaveAliases(wxCommandEvent& )
{
	SaveAliases();
}

void wxMudClientDlg::OnViewGroup(wxCommandEvent& )
{
    if( !_groupWindow )
    {
        _groupWindow = new GroupWindow(this);
    }
    _groupWindow->Show();
}

void wxMudClientDlg::OnViewEquipment(wxCommandEvent& )
{
    if( !_equipmentWindow )
    {
        _equipmentWindow = new EquipmentWindow(this);
    }
    SendText(_("equipment\n"));
    _equipmentWindow->Show();
}

void wxMudClientDlg::OnViewMap(wxCommandEvent& )
{
    if( !_mapWindow )
    {
        _mapWindow = new MapWindow(this);
    }
    _mapWindow->Show();
}

void wxMudClientDlg::OnViewStatus( wxCommandEvent&)
{
    if( !_statusWindow )
    {
        _statusWindow = new StatusWindow(this);
    }
    _statusWindow->Show();
}

void wxMudClientDlg::OnViewHotkeys( wxCommandEvent&)
{
    if( !_hotkeyWindow )
    {
        _hotkeyWindow = new HotkeyWindow(this);
    }
    _hotkeyWindow->Show();
}

void wxMudClientDlg::ShowConnectionString(wxString message)
{
	// Flush text before sending so we don't see the boxes for semicolon replacement.
	// Subtract 1 so we don't render the \n as a box.
	AddStringEvent(message, true, clrWhite );
}

void wxMudClientDlg::OnKey(wxKeyEvent& event)
{
	// TODO: Implement a blinking cursor.

	// TODO: Implement insert and overtype modes.
    int keycode = event.GetKeyCode();

	// Send the buffer and clear it.
	if( keycode == WXK_RETURN || keycode == WXK_NUMPAD_ENTER )
	{
                // We need to call GetValue because GetLabel only works on Windows.
		wxString inputbuf = _txtInput->GetValue();
		_txtInput->Clear();
		// We ifdef for win32 here because the alias expansion is broken on Linux.

		// If we have a pound key, process it as if it were a tintin command.
		// Additionally, here is where we will want to process any alias expansion.
		if( inputbuf.StartsWith(_("#")))
		{
			// Get the keyword to see what we've got
			wxStringTokenizer tok;
			tok.SetString( inputbuf, _(" ") );
			wxString keyword = tok.GetNextToken();
			// Check whether we have an alias
			if( keyword.StartsWith( _("#al") ) || keyword.StartsWith( _("#AL")) )
			{
				// We have an alias, get the keyword.
				wxString alias = tok.GetNextToken();
				// Get the remainder of the string.
				wxString expansion = tok.GetString();
				// Insert the keyword and the expansion into our map.
				if( alias.Length() == 0 )
				{
					ShowAliases();
					_txtInput->Clear();
					return;
				}
				else if( expansion.Length() == 0 )
				{
					ShowAlias(alias);
					_txtInput->Clear();
					return;
				}
				CreateAlias( alias, expansion );
	            wxString tmpBuffer;
	            AddStringEvent(tmpBuffer, true, clrWhite );
	            tmpBuffer = wxString::Format( _("{%s} now aliases {%s}"), alias.c_str(), expansion.c_str() );
				// Nothing to transmit - it was an internal command.
				_txtInput->Clear();
				return;
			}
		}
		else
		{
			inputbuf = CheckAliasExpansion( inputbuf );
		}

		// Create a new character buffer the length of the text we sent and add it
		// to the text buffer to be rendered.
		// TODO: Only add this to the text buffer if we have local echo turned on.
		// TODO: Make sendtext color user-configurable.

		// Flush text before sending so we don't see the boxes for semicolon replacement.
		// Subtract 1 so we don't render the \n as a box.

		// Semicolon replacement.
		AddStringEvent(inputbuf, true, clrWhite );
		// Copy to previous and then clear.
                _previousCommands.push_back(inputbuf);
                _commandLocation = _previousCommands.size();
                inputbuf.Append(_("\n\r"));
		inputbuf.Replace( _(";"), _("\n\r"), true );
		SendText(inputbuf, inputbuf.Length() );

		return;
	}
	// Delete previous char and move back.
	else if( event.GetKeyCode() == WXK_BACK )
	{
		event.Skip(true);
		return;
	}
	// Delete current char and slide text left.
	else if( event.GetKeyCode() == WXK_DELETE )
	{
		event.Skip(true);
		return;
	}
	// Move one to the left.
	else if( event.GetKeyCode() == WXK_LEFT )
	{
		event.Skip(true);
		return;
	}
	// Show the previous input buffer.
	else if( event.GetKeyCode() == WXK_UP )
	{
        if( _commandLocation > 0 && _commandLocation <= _previousCommands.size())
        {
            _commandLocation--;
            _txtInput->SetValue(_previousCommands[_commandLocation]);
        }
		return;
	}
    else if( event.GetKeyCode() == WXK_DOWN )
    {
        if( _previousCommands.size() > 0 && _commandLocation < (_previousCommands.size()-1))
        {
            _commandLocation++;
            _txtInput->SetValue(_previousCommands[_commandLocation]);
        }
        else
        {
            _commandLocation = _previousCommands.size();
            _txtInput->SetValue(_(""));
        }
		return;
    }
	// Move to beginning of text.
	else if( event.GetKeyCode() == WXK_HOME )
	{
		event.Skip(true);
		return;
	}
	// Move to the end of the text.
	else if( event.GetKeyCode() == WXK_END )
	{
		event.Skip(true);
		return;
	}
	// Move one to the right.
	else if( event.GetKeyCode() == WXK_RIGHT )
	{
		event.Skip(true);
		return;
	}
	else
	{
		if( _txtInput != NULL )
		{
			_txtInput->AppendText(wxChar(event.GetKeyCode()));
			_txtInput->SetInsertionPointEnd();
		}
		return;
	}

	event.Skip();
}

void wxMudClientDlg::SendText(wxString text)
{
    SendText( text, text.Length());
}

void wxMudClientDlg::SendText(wxString text, int length )
{
	if( _connected )
	{
	  send(_connection, text.ToAscii(), length, 0 );
	}
}

void wxMudClientDlg::RemoveANSICodes( wxString* string )
{
	// TODO: Remove these using a regular expression.
	string->Replace(_("\x1B[0m"), _("") );
	string->Replace(_("\x1B[30m"), _("") );
	string->Replace(_("\x1B[31m"), _("") );
	string->Replace(_("\x1B[32m"), _("") );
	string->Replace(_("\x1B[33m"), _("") );
	string->Replace(_("\x1B[34m"), _("") );
	string->Replace(_("\x1B[35m"), _("") );
	string->Replace(_("\x1B[36m"), _("") );
	string->Replace(_("\x1B[37m"), _("") );
	string->Replace(_("\x1B[0;30m"), _("") );
	string->Replace(_("\x1B[0;31m"), _("") );
	string->Replace(_("\x1B[0;32m"), _("") );
	string->Replace(_("\x1B[0;33m"), _("") );
	string->Replace(_("\x1B[0;34m"), _("") );
	string->Replace(_("\x1B[0;35m"), _("") );
	string->Replace(_("\x1B[0;36m"), _("") );
	string->Replace(_("\x1B[0;37m"), _("") );
	string->Replace(_("\x1B[1;30m"), _("") );
	string->Replace(_("\x1B[1;31m"), _("") );
	string->Replace(_("\x1B[1;32m"), _("") );
	string->Replace(_("\x1B[1;33m"), _("") );
	string->Replace(_("\x1B[1;34m"), _("") );
	string->Replace(_("\x1B[1;35m"), _("") );
	string->Replace(_("\x1B[1;36m"), _("") );
	string->Replace(_("\x1B[1;37m"), _("") );
}

// Strips \n and \r characters from a string.
void wxMudClientDlg::RemoveNewlines( wxString* string )
{
	string->Replace(_("\n"), _("") );
	string->Replace(_("\r"), _("") );
	return;
}

void wxMudClientDlg::RemoveUnprintableASCIIChars( wxString *string )
{
        if( string == NULL || string->size() < 1 )
        {
            return;
        }
        wxChar ff = wxChar(0xFF);
        wxChar fb = wxChar(0xFB);
        wxChar fd = wxChar(0xFD);
        wxChar fa = wxChar(0xFA);
        wxChar fc = wxChar(0xFC);
        wxChar tab = wxChar(0x09);
        wxChar space = wxChar(0x20);
	string->Replace(&ff, &space, true);
	string->Replace(&fb, &space, true);
	string->Replace(&fc, &space, true);
	string->Replace(&fd, &space, true);
	string->Replace(&fa, &space, true);
	string->Replace(&tab, &space, true);
}

// Strips \n and \r characters and tabs and spaces from a string.
void wxMudClientDlg::RemoveNewlinesAndSpaces( wxString* string )
{
	RemoveNewlines(string);
	string->Replace(_(" "), _("") );
	string->Replace(_("\t"), _("") );
}

// TODO:
// What we should be doing, rather than allowing separate strings in any combination,
// we should instead just grab the entire string from ESC[ to m.
//
// Once we have that, we can check for internal semicolons and other settings and
// process it as a single unit.  This makes sense because an ESC[1;37m doesn't mean
// flush text, change to bold, then flush text, then change to white.  Instead it means
// flush text and then change to bold white.
//
// The ANSI code processing as it is now is very broken and only partially works, only
// some of the time.
void wxMudClientDlg::ProcessANSICodes( wxString* string )
{
	int length = string->Length();
	int incount;
	const wxChar* instring = string->GetData();
	wxString outstring;

	for( incount = 0; incount < length; incount++ )
	{
		if( instring[incount] == '\n' )
		{
			// Newline found, flush all text to screen.
			AddStringEvent(outstring, true, *_foregroundColor );
			outstring.Clear();
			continue;
		}
		else if( instring[incount] == '\r' )
		{
			continue;
		}
		else if( instring[incount] == 27 )
		{
			// Go until we find an m to terminate the string.
			// This may not work properly with ANSI codes at the end of a string.
			// This should allow us to process any number of ANSI codes.
			int ansiStringPtr = 0;
			wxChar* ansiString = new wxChar[length+1];
			memset(ansiString, 0, (sizeof(wxChar) * (length + 1)));
			while( instring[incount] != 'm' && incount < (length) )
			{
				// Copy to our ANSI string buffer.
				if( instring[incount] != 27 && instring[incount] != '[' )
				{
					ansiString[ansiStringPtr] = instring[incount];
					ansiStringPtr++;
				}
				incount++;
			}

			// We found an m at the end of the string.  Skip that too, but DO flush colors if we have them.
			wxColour* newcolor = GetColorFromAnsiCode( ansiString );
			delete[] ansiString;
			if( newcolor != NULL )
			{
				if( outstring.Length() > 0 )
				{
					AddStringEvent(outstring, false, *_foregroundColor );
					outstring.Clear();
				}
				_foregroundColor = newcolor;
			}
			continue;
		}
		outstring.Append(instring[incount]);
	}
	// If we reach the end of the buffer without doing anything, put the string onscreen.
	AddStringEvent(outstring, false, *_foregroundColor );
	outstring.Clear();

	return;
}

void wxMudClientDlg::ProcessXMLTags( wxString string )
{
	wxString outbuf;
	wxString tagbuf;
	unsigned int count;

	// We use a single boolean value to keep track of whether we have a closed
	// tag or not.
	//
	// We keep a second bufer, tagbuf, to build our keyword.
	for( count = 0; count < string.Length(); count++ )
	{
		if( _unclosedTag == false && string[count] == wxChar('<') )
		{
			_unclosedTag = true;
			tagbuf.Append(_("<" ));
		}
		else if( _unclosedTag == true && string[count] == wxChar('>'))
		{
			_unclosedTag = false;
			tagbuf.Append(string[count]);

			// Process tag, flush out buffer, and change mode
			if( tagbuf == wxString( _("<prompt>") ))
			{
				if( _parseMode == MODE_NONE )
				{
					ProcessANSICodes( &outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_PROMPT;
				if( _statusWindow != NULL )
				{
					_statusWindow->_haveTank = false;
					_statusWindow->_haveEnemy = false;
				}
			}
			else if( tagbuf == wxString( _("</prompt>") ))
			{
				if( _parseMode == MODE_PROMPT )
				{
					RemoveANSICodes( &outbuf );
					if( _statusWindow != NULL )
					{
						_statusWindow->ProcessPrompt( outbuf );
					}
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_NONE;
				if( _statusWindow != NULL )
				{
					if( _statusWindow->_haveTank == false )
					{
						_statusWindow->setTankCond( _(" ") );
						_statusWindow->setTankName( _(" ") );
						_statusWindow->setTankPosition( _(" ") );
					}
					if( _statusWindow->_haveEnemy == false )
					{
						// TODO: Make this a single function call to clear.
						_statusWindow->setEnemyName( _(" ") );
						_statusWindow->setEnemyCond( _(" ") );
						_statusWindow->setEnemyPosition( _(" ") );
					}
				}
			}
			else if( tagbuf == wxString( _("<map>") ) || tagbuf == wxString( _("<automap>") ))
			{
				if( _parseMode == MODE_NONE )
				{
					ProcessANSICodes( &outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_MAP;
			}
			else if( tagbuf == wxString( _("</map>") ) || tagbuf == wxString( _("</automap>") ))
			{
				if( _mapWindow )
				{
					RemoveANSICodes( &outbuf );
					_mapWindow->ProcessMap( outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_NONE;
			}
            else if( tagbuf == wxString( _("<group>") ))
            {
                // The problem with the group tag is that there are nested subtags and each groupmember
                // actually has a <hits moves etc> for their entry.
                if( _parseMode == MODE_NONE )
                {
                    ProcessANSICodes( &outbuf );
                }
                outbuf = wxEmptyString;
                tagbuf = wxEmptyString;
                _parseMode = MODE_GROUP;
            }
            else if( tagbuf == wxString( _("</group>") ))
            {
                if( _groupWindow != NULL )
                {
                    RemoveANSICodes(&outbuf);
                    RemoveNewlines(&outbuf);
                    // We may have removed everything from the buffer.
                    _groupWindow->ProcessGroupData( outbuf );
                }
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_NONE;
            }
            else if( tagbuf == wxString( _("<equipment>") ))
            {
                if( _parseMode == MODE_NONE )
                {
                    ProcessANSICodes( &outbuf );
                }
                outbuf = wxEmptyString;
                tagbuf = wxEmptyString;
                _parseMode = MODE_EQUIPMENT;
            }
            else if( tagbuf == wxString( _("</equipment>") ))
            {
                if( _equipmentWindow != NULL )
                {
                    RemoveANSICodes(&outbuf);
                    RemoveNewlines(&outbuf);
                    // We may have removed everything from the buffer.
                    if( outbuf.Length() > 0 )
                    {
                        _equipmentWindow->ProcessEquipmentData( outbuf );
                    }
                }
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_NONE;
            }
			else if( tagbuf == wxString( _("<zone>") ))
			{
				if( _parseMode == MODE_NONE )
				{
					ProcessANSICodes( &outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_ZONE;
			}
			else if( tagbuf == wxString( _("</zone>") ))
			{
				if( _mapWindow != NULL )
				{
					_mapWindow->SetZoneName( outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_NONE;
			}
			else if( tagbuf == wxString( _("<room>") ) || tagbuf == _("<roomTitle>"))
			{
				if( _parseMode == MODE_NONE )
				{
					ProcessANSICodes( &outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_ROOM;
			}
			else if( tagbuf == wxString( _("</room>") ) || tagbuf == _("</roomTitle>"))
			{
				if( _mapWindow != NULL )
				{
					_mapWindow->SetRoomName( outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_NONE;
			}
            else if( tagbuf == wxString( _("<exits>" )))
            {
                if( _parseMode == MODE_NONE )
                {
                    ProcessANSICodes( &outbuf );
                }
                outbuf = wxEmptyString;
                tagbuf = wxEmptyString;
                _parseMode = MODE_ROOMEXIT;
            }
            else if( tagbuf == wxString( _("</exits>" )))
            {
                if( _mapWindow != NULL )
                {
                    _mapWindow->SetExits( outbuf );
                }
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_NONE;
            }
			else if( tagbuf == _("<roomDescription>"))
			{
				if( _parseMode == MODE_NONE )
				{
					ProcessANSICodes( &outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_ROOM_DESCRIPTION;
			}
			else if( tagbuf == _("</roomDescription>"))
			{
				if( _mapWindow != NULL )
				{
					_mapWindow->SetRoomDescription( outbuf );
				}
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
				_parseMode = MODE_NONE;
			}
			else
			{
				if( _parseMode == MODE_NONE )
				{
 					ProcessANSICodes( &outbuf );
					ProcessANSICodes( &tagbuf );
				}
				else if( _parseMode == MODE_PROMPT )
				{
					// Since the whole prompt is inside a tag itself, this is what we send.
					RemoveANSICodes( &tagbuf );
					_statusWindow->ProcessPrompt( tagbuf );
				}
                else if( _parseMode == MODE_GROUP )
                {
                    // We're inside the group string, processing individual member tags.
                    //_unclosedTag = false;
                    // -> Copy tagbuf to the outbuf
                    // -> Continue until we actually see a </group> tag.
                    // -> Keep track of how many times we've hit the group thing to know which line to fill.
                    _unclosedTag = false;
                    int tagbufSize = tagbuf.Length();
                    int counter;
                    for( counter = 0; counter < tagbufSize; counter++ )
                    {
						outbuf.Append(tagbuf[counter]);
                    }
				    tagbuf.Clear();
                    continue;
    //                if( _groupWindow != NULL )
    //                {
    //                    RemoveANSICodes(outbuf);
    //                    RemoveNewlines(outbuf);
    //                    if( strlen(outbuf) > 0 )
    //                    {
    //                        _groupWindow->ProcessGroupData( outbuf );
    //                    }
    //                }
                }
                else if( _parseMode == MODE_EQUIPMENT )
                {
                    // We're inside the eq string
                    //_unclosedTag = false;
                    // -> Copy tagbuf to the outbuf
                    // -> Continue until we actually see a </group> tag.
                    // -> Keep track of how many times we've hit the group thing to know which line to fill.
                    _unclosedTag = false;
                    int tagbufSize = tagbuf.Length();
                    int counter;
                    for( counter = 0; counter < tagbufSize; counter++ )
                    {
						outbuf.Append(tagbuf[counter]);
                    }
				    tagbuf = wxEmptyString;
                    continue;
                }
				outbuf = wxEmptyString;
				tagbuf = wxEmptyString;
            }
		}
		else if( _unclosedTag == true )
		{
			tagbuf.Append(string[count]);
		}
		else
		{
			outbuf.Append(string[count]);
		}
	}

	// We've reached the end.
	//
	// Process the remainder of the buffer.
	if( _parseMode == MODE_NONE )
	{
		ProcessANSICodes( &outbuf );
	}
	else if( _parseMode == MODE_PROMPT )
	{
		RemoveANSICodes( &outbuf );
		_statusWindow->ProcessPrompt( outbuf );
	}
	else if( _parseMode == MODE_GROUP )
	{
        if( _groupWindow != NULL )
        {
            RemoveANSICodes(&outbuf);
            RemoveNewlines(&outbuf);
            if( outbuf.Length() > 0 )
            {
                _groupWindow->ProcessGroupData( outbuf );
            }
        }
	}

	//	_snprintf( buf, INPUT_BUFFER_SIZE, "XML tag size %d found at offset %d: %s.", size, startoffset, xmlbuf );
	//	MessageBox( NULL, buf, "XML Tag", MB_OK );
	//	string = &string[endoffset];
	//}

    return;
}

void wxMudClientDlg::ChangeLogMode( int mode )
{
	_logMode = mode;
}

void wxMudClientDlg::ChangeColorMode( int mode )
{
	_colorMode = mode;
}

wxColour* wxMudClientDlg::GetColorFromAnsiCode( wxString text )
{
    if( text == wxEmptyString )
    {
        return NULL;
    }

	if( text == _("0") )
	{
		return &clrDkwhite;
	}
	else if( text == _("1;37") || text == _("1;5;40;37") )
	{
		return &clrWhite;
	}
	else if( text == _("37") || text == _("0;37") )
	{
		return &clrDkwhite;
	}
	else if( text == _("1;30") )
	{
		return &clrGray;
	}
	else if( text == _("30") || text == _("0;30") )
	{
		return &clrBlack;
	}
	else if( text == _("1;31") || text == _("0;31;40;1") || text == _("31;40;1") ||
		text == _("31;40;1;5") )
	{
		return &clrRed;
	}
	else if( text == _("31") || text == _("0;31") )
	{
		return &clrDkred;
	}
	else if( text == _("1;32") )
	{
		return &clrGreen;
	}
	else if(text == _("32") || text == _("0;32") )
	{
		return &clrDkgreen;
	}
	else if( text == _("1;33") || text == _("1;5;40;33") || text == _("0;33;40;1") )
	{
		return &clrYellow;
	}
	else if( text == _("33") || text == _("0;33") )
	{
		return &clrOrange;
	}
	else if( text == _("1;34") || text == _("0;34;1") || text == _("1;5;40;34") )
	{
		return &clrBlue;
	}
	else if( text == _("34") || text == _("0;34") )
	{
		return &clrDkblue;
	}
	else if( text == _("1;35") || text == _("0;35;40;1") )
	{
		return &clrPurple;
	}
	else if( text == _("35") || text == _("0;35") )
	{
		return &clrDkpurple;
	}
	else if( text == _("1;36") )
	{
		return &clrCyan;
	}
	else if( text == _("36") || text == _("0;36") )
	{
		return &clrDkcyan;
	}
	else
	{
#ifdef _DEBUG
		// TODO: Fix this so that we never get this message.
		//wxMessageBox( text, _("Unrecognized ANSI Code") );
#endif
		return NULL;
	}
}

void wxMudClientDlg::FlushText(wxCommandEvent&)
{
	_pendingStringOutputMutex->Lock();
	StringEntry entry = _pendingStringOutputs->front();
	_pendingStringOutputs->pop_front();
	_pendingStringOutputMutex->Unlock();
	RemoveUnprintableASCIIChars(&entry.Text);
	_txtOutput->BeginTextColour(entry.ForegroundColour);
	if( entry.Newline )
	{
		_txtOutput->WriteText(entry.Text.Append(_("\n")));
	}
	else
	{
		_txtOutput->WriteText(entry.Text);
	}
	_txtOutput->EndTextColour();
	_txtOutput->SetInsertionPointEnd();
	_txtOutput->ShowPosition(_txtOutput->GetLastPosition());
	Refresh();
}

void wxMudClientDlg::SetInputFont( wxString font, int size )
{
	// TODO: Set font for map window too.
	if( _txtInput != NULL )
	{
		wxFont* newFont = new wxFont(font);
		_txtInput->SetFont( *newFont );
	}
}

void wxMudClientDlg::SetDisplayFont( wxString font, int size )
{
	// TODO: Set font for map window too.
	if( _txtOutput != NULL )
	{
		wxFont* newFont = new wxFont(font);
		_txtOutput->SetFont( *newFont );
	}
}

void wxMudClientDlg::Log( wxString logString )
{
	if( _logFile.good() )
	{
        if( _logMode == LOG_HTML )
		{
		    _htmlFile << logString << "<BR>" << std::endl;
		}
		else if( _logMode == LOG_ANSI || _logMode == LOG_TEXT )
		{
			_logFile << logString << std::endl;
		}
	}
}

void wxMudClientDlg::ChangeStatusColor( wxColour& color )
{
	if( _statusWindow != NULL )
	{
		_statusWindow->SetStatusColor( color.Red(), color.Green(), color.Blue() );
	}
}

void wxMudClientDlg::ChangeStatusTitleColor( wxColour& color )
{
	if( _statusWindow != NULL )
	{
	    _statusWindow->SetStatusTitleColor( color.Red(), color.Green(), color.Blue() );
	}
}

void wxMudClientDlg::SaveAliases()
{
	wxFileDialog fdialog( NULL, _T("Choose a file to Save"), _T("."), _T("Aliases"), _T("Alias Files (*.alias) |*.alias||"), wxFD_SAVE );

	wxString fileName;

	if( fdialog.ShowModal() == wxID_OK )
	{
		fileName = fdialog.GetPath();
		wxFileOutputStream* strm = new wxFileOutputStream(fileName);
		_aliasData->Save( *strm );
        strm->Close();
	}
}

void wxMudClientDlg::LoadAliases()
{
	wxFileDialog fdialog( NULL, _T("Choose a file to Load"), _T("."), _T("Aliases"), _T("Alias Files (*.alias) |*.alias||"), wxFD_OPEN );

	wxString fileName;

	if( fdialog.ShowModal() == wxID_OK )
	{
		fileName = fdialog.GetPath();
		wxFileInputStream* strm = new wxFileInputStream(fileName);
		_aliasData = new wxFileConfig( *strm );
	}

    ShowAliases();

    wxString* str = new wxString;
    wxString* str2 = new wxString;
	if( _aliasData->Read(_("button0"), str ))
    {
        _aliasData->Read(_("button0name"), str2 );
        _hotkeyWindow->SetHotkey(0, *str, *str2);
    }
	if( _aliasData->Read(_("button1"), str ))
    {
        _aliasData->Read(_("button1name"), str2 );
        _hotkeyWindow->SetHotkey(1, *str, *str2);
    }
	if( _aliasData->Read(_("button2"), str ))
    {
        _aliasData->Read(_("button2name"), str2 );
        _hotkeyWindow->SetHotkey(2, *str, *str2);
    }
	if( _aliasData->Read(_("button3"), str ))
    {
        _aliasData->Read(_("button3name"), str2 );
        _hotkeyWindow->SetHotkey(3, *str, *str2);
    }
	if( _aliasData->Read(_("button4"), str ))
    {
        _aliasData->Read(_("button4name"), str2 );
        _hotkeyWindow->SetHotkey(4, *str, *str2);
    }
	if( _aliasData->Read(_("button5"), str ))
    {
        _aliasData->Read(_("button5name"), str2 );
        _hotkeyWindow->SetHotkey(5, *str, *str2);
    }
	if( _aliasData->Read(_("button6"), str ))
    {
        _aliasData->Read(_("button6name"), str2 );
        _hotkeyWindow->SetHotkey(6, *str, *str2);
    }
	if( _aliasData->Read(_("button7"), str ))
    {
        _aliasData->Read(_("button7name"), str2 );
        _hotkeyWindow->SetHotkey(7, *str, *str2);
    }
	if( _aliasData->Read(_("button8"), str ))
    {
        _aliasData->Read(_("button8name"), str2 );
        _hotkeyWindow->SetHotkey(8, *str, *str2);
    }
	if( _aliasData->Read(_("button9"), str ))
    {
        _aliasData->Read(_("button9name"), str2 );
        _hotkeyWindow->SetHotkey(9, *str, *str2);
    }
	if( _aliasData->Read(_("button10"), str ))
    {
        _aliasData->Read(_("button10name"), str2 );
        _hotkeyWindow->SetHotkey(10, *str, *str2);
    }
	if( _aliasData->Read(_("button11"), str ))
    {
        _aliasData->Read(_("button11name"), str2 );
        _hotkeyWindow->SetHotkey(11, *str, *str2);
    }
	if( _aliasData->Read(_("button12"), str ))
    {
        _aliasData->Read(_("button12name"), str2 );
        _hotkeyWindow->SetHotkey(12, *str, *str2);
    }
	if( _aliasData->Read(_("button13"), str ))
    {
        _aliasData->Read(_("button13name"), str2 );
        _hotkeyWindow->SetHotkey(13, *str, *str2);
    }
	if( _aliasData->Read(_("button14"), str ))
    {
        _aliasData->Read(_("button14name"), str2 );
        _hotkeyWindow->SetHotkey(14, *str, *str2);
    }
	if( _aliasData->Read(_("button15"), str ))
    {
        _aliasData->Read(_("button15name"), str2 );
        _hotkeyWindow->SetHotkey(15, *str, *str2);
    }
}

void wxMudClientDlg::SetMapTiled( bool state )
{
	if( _mapWindow != NULL )
	{
		_mapWindow->SetMapTiled( state );
	}
}

void wxMudClientDlg::ProcessNetwork()
{
	// Process the network.
	if( _connected )
	{
		char recvbuf[(RECEIVE_BUFFER_SIZE+1)];
		memset( recvbuf, 0, (RECEIVE_BUFFER_SIZE+1) );
		int error = recv(_connection, recvbuf, RECEIVE_BUFFER_SIZE, 0 );
		wxString received = wxString::FromAscii(recvbuf); 
		wxString output;

		if( error > 0 )
		{
			// Log all packets to file if logging is on
            if( received.Contains( _("Enter your terminal type (0 for ASCII, 1 for ANSI, 9 for Quick): " )))
            {
                send(_connection, "5\n", 2, 0 );
                return;
            }
			if( _logMode == LOG_ANSI && _logFile.good() )
			{
				Log(received);
			}
			// Call a function to process our received data.
			if( _colorMode == COLORMODE_NONE )
			{
				RemoveANSICodes(&received);
			}
			if( _colorMode == COLORMODE_NONE || _colorMode == COLORMODE_RAW )
			{
				// For when we are not processing our text.
				int incount = 0;
				for( incount = 0; received.Length(); incount++ )
				{
				   if( received[incount] == '\r' )
				   {
					   continue;
				   }
				   else if( recvbuf[incount] != '\n' )
				   {
						output.Append(received[incount]);
				   }
				   else
				   {
						AddStringEvent(output, true, *_foregroundColor );
						output.Clear();
						_foregroundColor = &clrWhite;
				   }
				}
				AddStringEvent( output, false, *_foregroundColor );
			}
			else
			{
				ProcessXMLTags(received);
			}
		}
        // We had an error in our call to recv(), deal with it:
        else
        {
			HandleSocketError();
	    }
	}
}

void wxMudClientDlg::HandleSocketError()
{
#ifdef WIN32
			int errorNo = WSAGetLastError();
			if( errorNo != WSAEWOULDBLOCK )
#else
			int errorNo;
			int len = sizeof(int);
			getsockopt( _connection, SOL_SOCKET, SO_ERROR, (void *)&errorNo, (socklen_t*)&len );
			// TODO: Figure out what file to include to get this to work.
			//if( errorNo != E_WOULDBLOCK )
			if( errorNo != 0 )
#endif
			{
				wxString errBuf;
				if( errorNo == 10054)
				{
					errBuf = wxString::Format( _("Socket Error: %d (Connection Reset By Peer)"), errorNo );
				}
				else if( errorNo == 10060 )
				{
					errBuf = wxString::Format( _("Socket Error: %d (Connection Timed Out)"), errorNo );
				}
				else if( errorNo == 10061 )
				{
					errBuf = wxString::Format( _("Socket Error: %d (Connection Refused)"), errorNo );
				}
				else
				{
					errBuf = wxString::Format( _("Socket Error: %d"), errorNo );
				}
				AddStringEvent(errBuf, true, clrWhite );
				_connected = false;
			}
}

void wxMudClientDlg::ShowAliases()
{
	// Just print all of our aliases and return.
	wxString key;
	long index;
	wxArrayString keyNames;
	bool go = _aliasData->GetFirstEntry(key, index);
	while( go )
	{
		keyNames.Add(key);
		go = _aliasData->GetNextEntry(key,index);
	}

	// Store keys in alias list.
    wxColour* tmpColor = _foregroundColor;
	_foregroundColor = &clrWhite;
    for( unsigned int i = 0; i < keyNames.Count(); i++ )
	{
		wxString value;
		_aliasData->Read( keyNames[i], &value );
        wxString aliasText = wxString::Format( _("ALIAS: {%s} --> {%s}\0"), keyNames[i].c_str(), value.c_str() );
		AddStringEvent(aliasText, true, clrWhite );
	}
	_foregroundColor = tmpColor;
	// Not a sent command, so we just clear the window without adding it to our
	// command history.
	//_inputWindow->Clear();
	return;
}

void wxMudClientDlg::CreateAlias(wxString alias, wxString expansion)
{
    _aliasData->Write(alias, expansion);
	// Subtract 1 so we don't render the \n as a box.
}

void wxMudClientDlg::ShowAlias(wxString alias)
{
	// Find the alias we're referring to and print it.
    wxString* value = new wxString;
    _aliasData->Read(alias, value);
	if( value->Length() > 0 )
	{
		// Print this result to the screen.
        wxString output = wxString::Format( _("ALIAS: {%s} --> {%s}."), alias.c_str(), value->c_str() );
		AddStringEvent(output, true, clrWhite );
	}
	else
	{
		AddStringEvent(wxString::Format(_("No alias defined for %s."), alias.c_str()), true, clrWhite );
	}
}

wxString wxMudClientDlg::CheckAliasExpansion(wxString input)
{
	if( input.Length() < 1 )
		return input;
	// Get the first word of the string.
	wxStringTokenizer tok;
	tok.SetString( input.c_str(), _(" ") );
	wxString keyword = tok.GetNextToken();
	// Set the inputbuffer to the rest of the string.
	wxString tmpBuffer = tok.GetString();
    tok.SetString( tmpBuffer, _("\n") );
	tmpBuffer = tok.GetNextToken();
	// Look up the expansion of the keyword.  Blank the last char if it is an '\r' or '\n' so we can get a good search.
    int keylength = (int)keyword.Length();
    if( keyword[(keylength-1)] == '\n' || keyword[(keylength-1)] == '\r' )
    {
        keyword[(keylength-1)] = '\0';
    }
    wxString* value = new wxString();
    if( _aliasData->Read(keyword, value) )
	{
		// Concatenate the expansion with the rest of the string.
		if( tmpBuffer.Length() > 0 )
		{
			input = wxString::Format( _("%s %s\n\0"), value->c_str(), tmpBuffer.c_str() );
            return input;
		}
		else
		{
			input = wxString::Format(_("%s\n\0"), value->c_str());
			return input;
		}
	}
	else
	{
		// No aliases found, just give them back what they sent us.
		return input;
	}
	return input;
}

wxMudClientDlg::~wxMudClientDlg()
{
	_done = true;
	Pause();
        wxMilliSleep(30);
	Destroy();
	wxMilliSleep(30);
}

void * wxMudClientDlg::Entry()
{
	while( !this->TestDestroy() && !this->_done)
	{
		ProcessNetwork();

		// throttle to keep from flooding the event queue
		wxMilliSleep(20);
	}
	return 0;
}

void wxMudClientDlg::ConnectToMyMUD()
{
	// Initialize TCP/IP Socket
	_connection = socket( AF_INET, SOCK_STREAM, 0 );
	memset( &_servaddr, 0, sizeof(_servaddr));
	_servaddr.sin_family = AF_INET;
	_servaddr.sin_port = htons( _port );
	struct hostent *host = gethostbyname( _hostname.ToAscii() );
	memcpy( (void *)&_servaddr.sin_addr, (void *)host->h_addr_list[0], sizeof( _servaddr.sin_addr ) );
	int error = connect( _connection, (const struct sockaddr *)&_servaddr, sizeof(_servaddr) );
	// Error on Connect, get number
	if( error )
	{
		HandleSocketError();
	}
	else
	{
		// make it a nonblocking socket
#ifdef WIN32
        unsigned long nonblocking = 0;
		ioctlsocket( _connection, FIONBIO, &nonblocking );
#else
		fcntl( _connection, F_SETFL, O_NONBLOCK );
#endif
		_connected = true;
	}
}

void wxMudClientDlg::ConnectToLocalhost()
{
	// Initialize TCP/IP Socket
        std::cout << "Creating socket." << std::endl;
	_connection = socket( AF_INET, SOCK_STREAM, 0 );
	memset( &_servaddr, 0, sizeof(_servaddr));
	_servaddr.sin_family = AF_INET;
	_servaddr.sin_port = htons( _port );
        std::cout << "Getting hostname." << std::endl;
	struct hostent *host = gethostbyname( "localhost" );
	memcpy( (void *)&_servaddr.sin_addr, (void *)host->h_addr_list[0], sizeof( _servaddr.sin_addr ) );
        std::cout << "Trying to connect." << std::endl;
	int error = connect( _connection, (const struct sockaddr *)&_servaddr, sizeof(_servaddr) );
	// Error on Connect, get number
	if( error )
	{
		HandleSocketError();
	}
	else
	{
		// make it a nonblocking socket
                std::cout << "Setting socket to nonblocking." << std::endl;
#ifdef WIN32
        unsigned long nonblocking = 1;
		ioctlsocket( _connection, FIONBIO, &nonblocking );
#else
		fcntl( _connection, F_SETFL, O_NONBLOCK );
#endif
		_connected = true;
	}
        std::cout << "Finishing wxMudClientDlg::Connect()" << std::endl;
}

void wxMudClientDlg::Disconnect()
{
	// Disconnect the TCP/IP Socket
#ifdef WIN32
	closesocket( _connection );
#else
	close( _connection );
#endif
	_connected = false;
}
