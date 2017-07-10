# Area Directory

This is where the MUD engine looks for area XML files.

The Area.list file tells the MUD which areas to load.

The file required.are.xml contains objects, rooms, and mobiles required by the
MUD engine in order to function. This includes items such as newbie equipment,
basic food and drink, and spell-related object templates. It also includes
basic spell-related mobiles such as elementals, undead, shadow/illusion
creatures, and basic mobile templates. It's unfortunate that the code requires
a base area, and that objects for spells that may or may not be implemented
are required on every MUD, so hopefully we'll come up with something better
at some point.

Some sample areas are included:

elf_hometown.are.xml - Cherdwyn, a hometown for Grey Elves.

mino_strong.are.xml - The Minotaur stronghold.

thri.are.xml - Thannik'Tzil, a hometown for the Thri-Kreen race.

vkobolds.are.xml - A Kobold town.

They are included in the Area.list file. To remove them, delete the lines from
the Area.list file.
