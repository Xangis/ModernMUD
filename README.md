# ModernMUD

(Formerly Basternae 3)

A modern multiplatform multi-user dungeon engine written in C# and .NET with a full toolset.

By multiplatform, I mean that the compiled MUD runs on Windows and Linux, but I do not
recommend compiling using any tool other than Visual Studio. There are subtle differences
in the output binaries that can cause trouble (i.e. terminal glitches) if you build on
Linux using xbuild/gmcs and it's hard to get a good build environment working on Linux for
this project. You're better off building on Windows, copying the executable to Linux, then
running it (that's how Basternae 3 did it). This has not been tested on OSX, but would probably 
work about the same as on Linux.

Some of the editing tools do not work fully under Mono, due to this RTF colored text rendering 
bug in Mono core:

https://bugzilla.novell.com/show_bug.cgi?id=593951

The engine itself uses C# .NET 2.0. There are four clients, one using C# and Silverlight,
one using WPF and C# .NET 3.5, one using C++ and wxWidgets, and one using C++ and SDL.
None of them are fully feature-complete, but the WPF client is in the best shape of the
three (and the Silverlight client worst). There is still work to be done on the ANSI color
parsing in all of them.

Documentation is in the /docs folder. Starting with the README.md file in that folder is
a good first step.

ModernMUD was in production use on a Linux server for Basternae 3 MUD, but Basternae has 
been discontinued.

For a less-modern multiplatform MUD engine written in C, see Magma here:

https://github.com/Xangis/magma

For a tool to convert MUD zones in other formats (Magma, Basternae 2) to ModernMUD format:

https://github.com/Xangis/ModernMUDConverter

# Building

## Building on Windows

There is a solution file at src/ModernMUD.sln. It's in an older version of Visual Studio,
so if you have a newer version, you'll be asked to convert it. It should convert fine.

The project relies on the .NET framework 2.0 or newer, so you'll need to have that.
It's very unlikely that you won't have that if you have Visual Studio.

## Building on Linux

At a minimum, you'll need to install xbuild and some other dependencies:

sudo apt-get install xbuild libmono-microsoft-build-tasks-v4.0-4.0-cil mono-gmcs libmono-system-data2.0-cil libmono-system-windows-forms4.0

# Running

Just run ModernMUD.exe and the MUD should be running on port 4502. If 
connecting from the same machine that the program is running from, you can run 
"telnet localhost 4502" to connect.

On Windows, you will probably be shown a Windows Firewall prompt asking whether
to allow access to the program. You should allow it.

# Development Status

ModernMUD is not under active development, but I do maintain it. I'll accept pull requests
if you have changes that will make it more useful.
