History of ModernMUD
--------------------

Q: What do you mean, history? Isn't this "ModernMUD"? Modern means discarding
everything that has come before and starting over, doesn't it?

A: What are you, a moron? Modern means incorporating any and all history in a
way that lets you synthesize something that's greater than the sum of its
parts.

Obligatory snark out of the way, knowing a bit about the history of ModernMUD
will help you understand more about the design and why it's so similar to the
DIKU MUDs you know and love.

The creator of ModernMUD (and the Magma MUD codebase that came before it)
learned to program on a Commodore VIC-20 and wrote his first complex programs
using GW-BASIC on DOS 2.11. After doing some FORTRAN programming (back when
you were supposed to capitalize it) during high school, he discovered C. In
1994, learning C was sort of like obtaining the keys to the universe. There was
nothing you couldn't do.

Being a D&D nerd and a C programmer in the heyday of MUDs meant that your life
would ABSOLUTELY AND COMPLETELY be swallowed by online RPGs to the point where
that's all you would do. It was the precursor to Everquest and World of Warcraft
addiction.

Codebases like DIKU, Merc, Envy, Copper, Sequent, and SMAUG introduced a whole
generation to MUD development. A combination of brilliant and stupid design
decisions existed in every codebase because most MUDs were written by college
students.

These are the codebases that ModernMUD's developer learned from. His first
"real" MUD was Illustrium Arcana, built from Envy 2.2. Its most notable feature
is that few MUDs were able to crash as much as it did. When Basternae 1 had to
shut down due to licensing issues, he reverse-engineered a codebase from the
zone format, using UltraEnvy 0.87j as the initial codebase (and a big "Thank
you" goes to Vasco Costa" for his part in creating such an easy-to-adapt
codebase).

The hardest part about running and developing for DIKU-derived MUDs was
debugging. MUDs are typically developed by college students with lots of time
and intelligence, but not much experience and deep subject knowledge. This
dynamic creates lots of creative ideas being implemented, but it also results
in a vast ecosystem of bugs and glitches. While SEGMENTATION FAULT, CORE DUMPED
may be a great way to say that there's a BIG PROBLEM, it doesn't give much of
an indication about how you could fix that problem.

Debugging is one of the great things about C# and the .NET platform - it has
most of the good parts of C and C++ in it, but it adds wonderful things related
to exception handling and stack traces. When the world explodes, it's easier
to figure out *why* the world exploded.

When you add the debugging capabilities of C# to the fact that it natively
supports XML serialization and that is has near-C performance characteristics,
you have a winning choice for MUD development. You'll find lots of people who
think that SQL is a great storage format for a MUD, but unless they're running
a NoSQL database like MongoDB, they'll eventually run into formatting/extension
problems with data files just like the old DIKU codebase did. Things like JSON
and XML alleviate (but don't completely eliminate) versioning problems and make
forward movement a lot easier. Having file formats that a Human can edit and/or
tinker with is a good thing for a MUD.

When you combine the benefits of C# with the need to support legacy areas
(zones) from DIKU, Envy, and even MAGMA MUDs, you end up with a codebase whose
input and output deeply resemble the work of the people who created DIKU, but
whose implemenation, aside from area-related data formats, looks entirely
different. That's the wonderful thing about incorporating the past into the
FUTURE: you get to use everything that has come before, especially data, but
in a way that lets you do sane things with exceptions and unexpected behavror.

