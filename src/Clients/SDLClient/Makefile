# Main executable file
PROGRAM = Telnet

# Directory containing library portions of code.
INCLUDEDIR = /usr/include/SDL/ 

# Object files
OBJECTS = Colors.o InputWindow.o SDLChildWindow.o SDLPanel.o TextWindow.o Main.o ConnectBar.o
LIBDIR = /usr/lib/

CXX = g++ -DNOHOTKEYS

.SUFFIXES:	.o .cpp

.cpp.o :
	$(CXX) -c -I$(INCLUDEDIR) -o $@ $<

all:    $(PROGRAM)

$(PROGRAM):	$(OBJECTS)
	$(CXX) -o $(PROGRAM) -L$(LIBDIR) $(OBJECTS) -lSDL -lSDL_image -lSDL_ttf -lrt

clean: 
	rm -f *.o $(PROGRAM)
