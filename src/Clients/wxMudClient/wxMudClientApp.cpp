#include "wxMudClientApp.h"
#ifdef __WXMAC__
#include <ApplicationServices/ApplicationServices.h>
#endif

wxMudClientApp::wxMudClientApp()
{
#ifdef __WXMAC__
ProcessSerialNumber PSN;
GetCurrentProcess(&PSN);
TransformProcessType(&PSN,kProcessTransformToForegroundApplication);
#endif
}

bool wxMudClientApp::OnInit()
{
    // create the wxMudClientDlg
    frame = new wxMudClientDlg;
    frame->SetClientSize(665, 615);
    frame->Show();

    // Our wxMudClientDlg is the Top Window
    SetTopWindow(frame);

    // initialization should always succeed
    return true;
}

int wxMudClientApp::OnRun()
{
    // start the main loop
    return wxApp::OnRun();
}

int wxMudClientApp::OnExit()
{
    // return the standard exit code
    return wxApp::OnExit();
}

IMPLEMENT_CLASS(wxMudClientApp, wxApp)
IMPLEMENT_APP(wxMudClientApp)

