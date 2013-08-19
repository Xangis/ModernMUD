Races Directory
---------------

This directory contains the XML race definition files.

It also includes the Races.list file, which is the list of races to be loaded
at runtime. This lets you swap races into and out of the game across boots.

There are some things that make race files not completely independent. In Limits.cs,
there are two definitions, MAX_RACE and MAX_PC_RACE that will need to be changed if
you add more races. There is also MAX_PC_RACE that is the highest race that a player
can use to create a character (yes, player races have to be first in the list). At
some point things should be more dynamic and not tied to hard-coded definitions,
but we're not there yet.

There is also a file called "_transform.xsl". This lets you view the race files
in a web browser in a player-friendly manner. This works with Opera, Chrome/Chromium,
and Internet Explorer, but Firefox has buggy handling of XSL stylesheets and didn't
work with it when tested.
