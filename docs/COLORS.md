ANSI Colors
-----------

You may use these color escapes in any string which is shown with the 
'SocketConnection.Act' or 'CharData.SendText' functions.  This includes
anything that is in an area file and most in-game communication commands.

== Foreground colors ==

&+l	for black foreground (don't use - nobody can see it)
&+r	for red foreground
&+g	for green foreground
&+y	for orange (or brown) foreground
&+b	for blue foreground
&+m	for purple foreground
&+c	for cyan foreground
&+w	for white (or gray) foreground
&+L	for dark grey foreground
&+R	for bright red/pink foreground
&+G	for bright green foreground
&+Y	for yellow foreground
&+B	for bright blue foreground
&+M	for bright purple foreground
&+C	for bright cyan foreground
&+W	for bright white foreground

== Background Colors ==

&-l for black background
&-g for green background
&-m for magenta background
&-r for red background
&-w for white background
&-y for yellow background
&-b for blue background
&-c for cyan background

Background colors generally look pretty terrible, so should only be used
sparingly and/or in special circumstances.

Capital letters can also be used (&-L). Those will produce the same color
as the lowercase letter.

== Color resets ==

&n or &N will reset color to the default color, which on most terminals
will be white.  These reset codes should be used at the end of a string to
prevent color bleeding.  Any strings in the source code that include tokens
like $p and $N should have &n placed immediately following them in order to
prevent color bleeding.  Color bleeding, in case you don't know, is when text
that is supposed to be white or the default color is the last color used
instead.  It looks tacky.

