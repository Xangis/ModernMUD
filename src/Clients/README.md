# Clients

This directory contains a few different clients that could be used to connect to your MUD.

### WPFMudClient

The WPFClient is the most advanced, and uses WPF with .NET 3.5. It does not work with Mono
and will not work on OSX or Linux, but may be your best choice for a Windows-only client.

### wxMudClient

The wxMudClient is the second most advanced. It has some issues and does not work perfectly,
but it does build and run on Linux, and builds on OSX (though it has not been tested much
on OSX). It is written in C++ and uses the wxWidgets libraries, which are available at
http://www.wxwidgets.org.

As an alternative, there is another open source project called wxMUD, which might be
a good starting point for a custom client:

http://wxmud.sourceforge.net/

### SDLClient

The SDLClient contains a basic SDL-based telnet client that was released for webOS as
"Telnet". It also runs on Linux and could probably be ported to any platform that SDL
supports with relative ease, since it's a simple program that doesn't do much. This may
be your best starting point if you want to develop an Android client.

### SilverlightClient

The Silverlight client is the least developed, and requires running the PolicySocketServer
on the same server as the MUD (Silverlight requires a "policy server" to tell it that it's
OK to connect to a server, and can only connect to a limited range of IP ports. That's why
the MUD defaults to running on port 4502 - it's one of the ports supported by Silverlight.
It could, however, be used as an in-browser MUD client, unlike the other three. Since 
Silverlight is on the way out, I don't recommend using it or developing it further.

## Notes

All four clients probably still have existing ANSI parsing and layout issues. Only the
first two support graphical maps, and the wxMudClient might be a version or two behind on
the protocol and tile layout.
