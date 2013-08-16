wxMudClient
-----------

The wxMudClient requires wxWidgets 2.9 or newer in order to build. It relies on
the wxRichTextCtrl that was not available before that version.

To customize the client for your MUD, edit MudSettings.h add set your MUD name,
host name, port, and website address. These are used in the title window, about
box, and connect menu entry.

== Build Notes ==

Windows: Open the Visual Studio project, configure your wxWidgets include and
library directories, then build.

Linux: Run make. You may need to change the makefile to use the correct
wx-config file, depending on where yours is located.

OSX: This should build with any version of Xcode that supports 10.5 (or newer?).
To build, run make -f Makefile.mac. As in the case of Linux, you may need to
change the makefile to use the correct wx-config, depending on where yours is
located. For versions of Xcode 4.x or newer, the command-line tools are not
installed, in which case you'll need to go to Xcode -> Preferences, go to the
Downloads tab, and then install the Command Line Tools. You will also
probably run into complications because wxWidgets wants SDK version 10.5 and
the OSX makefile is targeted to 10.5. I'm not an OSX developer (nor do I want
to be one), so if you want to contribute improvements to the build process I'd
be happy to accept pull requests. This might also be relevant:
http://stackoverflow.com/questions/5378518/how-to-add-base-sdk-for-10-5-in-xcode-4
