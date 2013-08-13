ModernMUD Project Goals
-----------------------

A modern MUD engine written in C# with support for running on both Windows and Linux servers.

Uses Human-readable XML for content and data storage and has robust exception handling that
lets you find errors a lot easier than vintage-style MUDs.

A robust tool set including:

- A zone editor.
- A map generator.
- A spell editor.
- A help file editor.
- An ANSI screen editor.
- One (or more) graphical client options.

Documentation is important. Not only should the code be written in a self-documenting style
(meaningful variable, class, and function names), it should be documented via XML comments
and there should be separate project documentation including both overview and how-to-use
documentation.

Non-Goals
---------

- Using all of the latest .NET platform features. We want to maintain cross-platform support,
  so things that are not available in Mono should not be used.
