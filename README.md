ModernMUD
=========

A modern multiplatform multi-user dungeon engine written in C# and .NET with a full toolset.

For a less-modern multiplatform MUD engine written in C, see Magma here:

https://github.com/Xangis/magma

By multiplatform, I mean that the compiled MUD runs on Windows and Linux, but I do not
recommend compiling using any tool other than Visual Studio. There are subtle differences
in the output binaries that can cause trouble (i.e. terminal glitches) if you build on
Linux. This has not been tested on OSX.

In addition, some of the editing tools do not work fully under Mono, due to this RTF
colored text rendering bug in Mono core:

https://bugzilla.novell.com/show_bug.cgi?id=593951

