ModernMUD Architecture
----------------------

ModernMUD is written in C# .NET 2.0 and uses XML files for configuration and 
data storage.

Area files are stored in the /area directory. The Area.list tells the MUD which
areas to load at boot-time, and areas can be added or removed by changing
entries in that file.

Race and class definitions are stored in XML files (in /races and /classes)
and behave much the same as areas - you can tell the MUD which races and
classes to load by editing Races.list and Classes.list.

Skills and spells are also handled in the same way (in /skills and /spells)
and are controlled by a load list. There's one particular thing to note about
spells, though - they can optionally contain source code that is compiled at
boot time. This lets you use C# for scripting in-game spellcasting behavior
and lets you do things you couldn't otherwise do with plain old XML.

Player files are stored in /player sorted into directories by first letter.

Log files are stored in /log. If something goes wrong and the server crashes,
check the log files for exceptions. ModernMUD has some pretty good/thorough
logging, so you'll likely be able to find the source of a problem in the logs.

Configuration files reside in the /sys directory and include things like the
global MUD settings file Sysdata.xml, the social definitions in Socials.xml,
and the corpse save file in Corpses.xml. See the README in that directory for
more info about what each file is for.

Even though races, classes, spells, and skills are defined in XML files, there
are a few things about them that are hard-coded in the game source, like the
enumerations defining the class ID numbers. Over time we plan to make the game
configuration as independent of the game as possible so that you don't need to
worry about predefined class lists in source code.

Resource Usage
--------------

The MUD boots in 30 seconds on my Windows 8 virtual machine with 2000 mobs, 
2000 objects, and 14000 rooms loaded. About half of that time is spent 
compiling spell code. It consumes 36MB of RAM and less than 5% of CPU.

On an Ubuntu Server Linode instance, the same MUD boots in about 25 seconds and
consumes about 70 MB of RAM when running via mono with CPU usage at about 5%.

DLLs and Source Projects
------------------------

The ModernMUD solution for Visual Studio has quite a few projects. Here's what
they are:

--- CORE LIBRARIES ---

HelpData - A DLL that contains the help entry class.

MUDEngine - The core game logic for ModernMUD. Depends on ZoneData, HelpData,
and Screens.

Screens - A DLL that contains the Screens class and color code definitions.

ZoneData - Defines the zone (area) format for the game and the data types for
all in-game entities (mobiles, objects, rooms, etc.)

--- TOOLS ---

HelpEditor - A standalone help file editor, depends on HelpData.

MapGenerator - A standalone surface map generator tool. Depends on ZoneData.

ModernMUDEditor - A standalone zone editor. Depends on Screens and ZoneData.

MUDScreenEditor - A standalone ANSI screen editor tool. Depends on Screens.

SpellEditor - A standalone magical spell editor. Depends on MUDEngine and
ZoneData.
