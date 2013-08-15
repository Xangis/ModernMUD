#ifndef _WXMUDCLIENTAPP_H_
#define _WXMUDCLIENTAPP_H_

#include "wxMudClientDlg.h"

/*******************************************************************************
// wxMudClientApp Class
*******************************************************************************/

class wxMudClientApp : public wxApp {
    DECLARE_CLASS(wxMudClientApp)

private:
    wxMudClientDlg *frame;

public:
    wxMudClientApp();
    /**
     * Called to initialize this wxMudClientApp.
     *
     * @return true if initialization succeeded; false otherwise.
     */
    bool OnInit();

    /**
     * Called to run this wxMudClientApp.
     *
     * @return The status code (0 if good, non-0 if bad).
     */
    int OnRun();

    /**
     * Called when this wxMudClientApp is ready to exit.
     *
     * @return The exit code.
     */
    int OnExit();
};

DECLARE_APP(wxMudClientApp)

#endif
