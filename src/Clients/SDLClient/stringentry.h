#pragma once

// Struct to hold individual string entries for the rendering screen.
struct StringEntry
{
	SDL_Color color;
	char* string;
	bool newline;
};