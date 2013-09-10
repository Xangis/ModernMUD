#!/bin/sh
#
# Builds a compressed tarball for distribution to Linux and OSX systems.
#
# Change the VERSION variable as the program version increases.
VERSION=0.59
mkdir -p package/races package/classes package/area package/bin
cp ../ModernMUDEditor.exe package/bin
cp ../editorhelp.chm package/bin
cp ../Screens.dll package/bin
cp ../ZoneData.dll package/bin
cp ../../classes/* package/classes
cp ../../races/* package/races
cp area/* package/area
cp start.sh package
cd package
tar cvfz ModernMUDEditor_v$VERSION.tgz *
mv ModernMUDEditor_v$VERSION.tgz ../..
cd ..
rm -fR package
