ModernMUD
=========

A modern multiplatform multi-user dungeon engine written in C# and .NET with a full toolset.

For a less-modern multiplatform MUD engine written in C, see Magma here:

https://github.com/Xangis/magma

By multiplatform, I mean that the compiled MUD runs on Windows and Linux, but I do not
recommend compiling using any tool other than Visual Studio. There are subtle differences
in the output binaries that can cause trouble (i.e. terminal glitches) if you build on
Linux. This has not been tested on OSX, but would probably work about the same as on Linux.

In addition, some of the editing tools do not work fully under Mono, due to this RTF
colored text rendering bug in Mono core:

https://bugzilla.novell.com/show_bug.cgi?id=593951

The engine itself uses .NET 2.0. There are four clients, one using Silverlight, one using
WPF and .NET 3.5, one using C++ and wxWidgets, and one using SDL. None of them are fully
feature-complete, but the WPF client is in the best shape of the three (and the Silverlight
client worst). There is still work to be done on the ANSI color parsing in all of them.

ModernMUD is in use for Basternae 3 MUD, which you can find here:

http://basternae.org

