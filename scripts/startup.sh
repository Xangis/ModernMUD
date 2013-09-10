#!/bin/bash
if [ -r SHUTDOWN.TXT ]; then rm -f SHUTDOWN.TXT; fi

while [ 1 ]
do
    mv ../log/ModernMUD.log ../log/ModernMUD`date +%Y-%m-%dT%H:%M:%S`.log

    # Run Basternae.
    cd ../bin;mono ModernMUD.exe > ../log/ModernMUD.log

    # Restart, giving old connections a chance to die.
    if [ -r SHUTDOWN.TXT ]; then
        rm -f SHUTDOWN.TXT
    fi

    sleep 10
done
