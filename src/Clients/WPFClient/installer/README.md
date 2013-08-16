This folder contains an Inno Setup project for building a Windows installer for
the WPFMudClient.

InnoSetup is free and available here:
http://www.jrsoftware.org/isinfo.php

The installer will be placed in a directory called "Output" in this directory.

You can customize the name of the menu entry, icons, and other parts of the
installation to suit your MUD, but one thing you should definitely do is
change the application GUID in the installer if you customize it. Otherwise
another MUD client compiled from this source could replace your client when
it is installed.

