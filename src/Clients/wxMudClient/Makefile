# wxMudClient makefile.  Requires wxWidgets.
# by default, wx-config from the PATH is used
WX_CONFIG := /usr/local/bin/wx-config

# Main executable file
PROGRAM = Release/wxMudClient

# Object files
OBJECTS = EquipmentWindow.o StatusWindow.o wxMudClientApp.o wxMudClientSettings.o GroupWindow.o MapWindow.o wxMudClientDlg.o HotkeyEditor.o Colors.o wxOwnerDrawStaticBitmap.o HotkeyWindow.o

CXX = $(shell $(WX_CONFIG) --cxx -ggdb -Wall -R/usr/local/lib)

.SUFFIXES:	.o .cpp

.cpp.o :
	$(CXX) -c -ggdb `$(WX_CONFIG) --cxxflags` -Wall -o $@ $<

all:    $(PROGRAM)

$(PROGRAM):	$(OBJECTS)
	$(CXX) -o $(PROGRAM) $(OBJECTS) `$(WX_CONFIG) --libs` -lrt -lwx_gtk2u_richtext-2.9

clean: 
	rm -f *.o $(PROGRAM)
