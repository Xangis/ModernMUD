Modern MUD Editor Installer
---------------------------

This folder contains an InnoSetup project file (.iss) that can be used to build
the installable version of the ModernMUD editor. The build output goes into the
parent directory (/bin).

It also contains a file called package.sh that can be used to build a compressed
tarball for distribution to OSX and Linux systems. Mono is required to run the
editor on those systems. To run the editor, execute ./start.sh after extracting
the tarball.

The installer is named for the version being built, so be sure to check that the
version set in the .iss or package.sh file matches the version you're building,
otherwise you might build an installer called ModernMUDEditor_v0.61.exe that
contains version 0.66, and that would just be silly.

InnoSetup is free and available here: http://www.jrsoftware.org/isinfo.php
