Clients
-------

This directory contains a few different clients that could be used to connect to your MUD.

The WPFClient is the most advanced, and uses WPF with .NET 3.5. It does not work with Mono
and will not work on OSX or Linux.

The wxMudClient is the second most advanced. It has some issues and does not work perfectly,
but it does build and run on Linux, and probably would run on OSX. It is written in C++ and
uses the wxWidgets libraries.

The Silverlight client is the least developed, and requires running the PolicySocketServer
to be running on the same server as the MUD. It could, however, be used as an in-browser
MUD client, unlike the other two.

All three clients probably still have existing ANSI parsing and layout issues. Only the
first two support graphical maps, and the wxMudClient is a version or two behind on the
protocol and tile layout.
