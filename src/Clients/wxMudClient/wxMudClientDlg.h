#ifndef _WXMUDCLIENTDLG_H_
#define _WXMUDCLIENTDLG_H_

#include <iostream>
#include <fstream>
#include <deque>
#include <vector>
#include <wx/wxprec.h>

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif
#include <wx/dcbuffer.h>
#include <wx/image.h>
#include "wx/panel.h"
#include "wx/fileconf.h"
#include "wx/richtext/richtextctrl.h"

#include "wxMudClientSettings.h"
#include "GroupWindow.h"
#include "EquipmentWindow.h"
#include "StatusWindow.h"
#include "HotkeyWindow.h"
#include "StringEntry.h"

#ifdef WIN32
#include "winsock.h"
#else
#include <sys/socket.h>
#include <unistd.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <resolv.h>
#include <sys/time.h>
#include <sys/types.h>
#include <sys/signal.h>
#include <errno.h>
#include <time.h>
#include <fcntl.h>
#include <netdb.h>
#include <signal.h>
#include <sys/stat.h>
#endif
#include "StatusWindow.h"
#include "MapWindow.h"
#include "GroupWindow.h"
#include "EquipmentWindow.h"
#include "HotkeyWindow.h"
#include "Modes.h"
#include <list>
#include <string>
#include <map>
#include "MudSettings.h"

#define RECEIVE_BUFFER_SIZE 1500
#define INPUT_BUFFER_SIZE 600

/*******************************************************************************
// wxMudClientDlg Class
*******************************************************************************/

class wxMudClientDlg : public wxFrame, public wxThread
{
    DECLARE_CLASS(wxMudClientDlg)
    DECLARE_EVENT_TABLE()

public:
	void AddStringEvent(wxString text, bool newline, wxColour color );
	~wxMudClientDlg();
    wxMudClientDlg();
    void * Entry();
	void SetDisplayFont( wxString font, int size );
	void SetInputFont( wxString font, int size );
	void ConnectToMyMUD();
	void ConnectToLocalhost();
	void Disconnect();
	void HandleSocketError();
	void Log( wxString logString );
	void OnKey(wxKeyEvent& event);
	static void RemoveANSICodes( wxString* string );
    void RemoveNewlines( wxString* string );
	void RemoveUnprintableASCIIChars( wxString* string );
    void RemoveNewlinesAndSpaces( wxString* string );
	void ProcessANSICodes( wxString* string );
	void ChangeLogMode( int mode );
	void ChangeColorMode( int mode );
	void FlushText( wxCommandEvent& event );
	void ProcessXMLTags( wxString string );
    void ChangeStatusColor( wxColour& color );
    void ChangeStatusTitleColor( wxColour& color );
	wxColour* GetColorFromAnsiCode( wxString text );
	void LoadAliases();
	void SaveAliases();
	void SetMapTiled( bool state );
	void ProcessNetwork();
	void ShowAliases();
    void ShowConnectionString(wxString message);
	void CreateAlias(wxString alias, wxString expansion);
	void ShowAlias(wxString alias);
	void SendText(wxString text, int length);
    void SendText(wxString text);
    wxString CheckAliasExpansion(wxString input);
	void OnSize( wxSizeEvent& size);
private:
    void OnFileExit(wxCommandEvent &event);
    void OnHelpAbout(wxCommandEvent &event);
	void OnFileConnect(wxCommandEvent &event);
	void OnFileDisconnect(wxCommandEvent &event);
	void OnSaveAliases(wxCommandEvent &event );
	void OnLoadAliases(wxCommandEvent &event );
    void OnViewGroup(wxCommandEvent &event );
    void OnViewStatus(wxCommandEvent &event );
    void OnViewMap(wxCommandEvent &event );
    void OnViewEquipment(wxCommandEvent &event );
	void OnViewSettings(wxCommandEvent &event );
	void OnViewHotkeys(wxCommandEvent &event );
	void OnConnectLocalhost(wxCommandEvent &event );
	wxMudClientSettings* _SettingsDlg;
	GroupWindow* _groupWindow;
    EquipmentWindow * _equipmentWindow;
    MapWindow * _mapWindow;
    StatusWindow * _statusWindow;
	HotkeyWindow * _hotkeyWindow;
	wxRichTextCtrl * _txtOutput;
	wxTextCtrl * _txtInput;
	wxString _hostname;
	int _port;
	struct sockaddr_in _servaddr;
	intptr_t _connection;
	bool _connected;
	// True if a closing XML tag hasn't been found for an opening one.
	bool _unclosedTag;
	bool _mccpEnabled; // Is MUD Client Compression Protocol enabled?
	int _colorMode;
	int _logMode;
	int _parseMode;
	std::ofstream _logFile;
	std::ofstream _htmlFile;
	wxColour * _foregroundColor;
	wxColour * _backgroundColor;
	wxFileConfig* _aliasData;
	// Figure out what data type to make this - it should be a struct of some sort
	// so we know what color to make the text.  Newlines and intra-line text changes
	// could be difficult.
	//std::map<wxString, wxString> _aliases;
	std::deque<StringEntry>* _pendingStringOutputs;
	wxMutex* _pendingStringOutputMutex;
	wxFont* _outputFont;
	wxPanel* _panel;
	wxIcon _icon;
    bool _done;
    std::vector<wxString> _previousCommands;
    unsigned int _commandLocation;
};

#endif
