This directory contains an InnoSetup project to build a Windows installer for
wxMudClient. 

InnoSetup is free and available here:
http://www.jrsoftware.org/isinfo.php

Be sure to include the right Visual C++ distributable with your installer. The
vcredist_x86.exe for Visual Studio 2010 is included, but you'll need to grab
another one if you use a different version.

The installer will be placed in a directory called "Output" in this directory.

You can customize the name of the menu entry, icons, and other parts of the
installation to suit your MUD, but one thing you should definitely do is
change the application GUID in the installer if you customize it. Otherwise
another MUD client compiled from this source could replace your client when
it is installed.
