SDLClient
---------

This is an SDL-based telnet client. It was originally written for HP webOS 3.0
TouchPad tablets and allowed you to do some basic MUDding and telnet on them.

It also works on Linux and could also be ported to other operating systems with
relative ease since SDL is multiplatform.

It was designed with the webOS on-screen keyboard in mind, so the text box is
only drawn partway down the screen. You can click the "+" at the top right to
expand the text area to the full screen size. This is so you can use the full
screen when you have an external bluetooth keyboard.

You can also change the linefeed mode by clicking the "CR" button, which
toggles among sending CR, LF, and CR+LF characters as line endings.

To build for Linux, run "make". To build for webOS, run "make -f Makefile.webos".
You'll need to have the webOS SDK installed, of course. To run it, run the
"Telnet" executable that is generated.

The telnet application is still available in the webOS app catalog as far as I
know. It suffered some ugly problems due to SDL orientation bugs in webOS - 
they had the orientation flags backward and the app had to work around them,
then when they finally fixed the orientation problem that had been worked
around in the 3.0.4 or 3.0.5 patch, it broke the orientation again. The last
version of Telnet (1.0.5), long after HP discontinued the webOS hardware, 
finally had the right behavior on a bug-free SDL implementation.
