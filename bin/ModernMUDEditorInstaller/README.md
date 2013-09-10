Modern MUD Editor Installer
---------------------------

This folder contains an InnoSetup project file (.iss) that can be used to build
the installable version of the ModernMUD editor. The build output goes into the
parent directory (/bin).

The installer is named for the version being built, so be sure to check that the
versions set in the .iss file matches the version you're building, otherwise you
might build an installer called ModernMUDEditor_v0.61.exe that contains version
0.66, and that would just be silly.

InnoSetup is free and available here: http://www.jrsoftware.org/isinfo.php
