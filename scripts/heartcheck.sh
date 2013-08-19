#!/bin/bash -f
#
# This is an example script that kills the MUD process if the HEARTBEAT file
# has not been updated within a certain time frame. It is only an example and
# will likely have to be changed to work with your particular system
# environment.
#
# This is used to catch MUD hangs/infinite loops, which was a problem in the
# early days of Basternae II, but may not be an issue now. It is provided
# here for your convenience should you need such a thing.
#
# Obviously only useful on *NIX systems.

# cd into the directory containing the HEARTBEAT file
cd  ../bin

# Following command will test for a file named "HEARTBEAT" that
# has a status change greater than $BEATTIME minutes. IF it finds
# it, it just displays the filename ala "ls -al" format so we ask
# grep to check the output for us.  If grep finds "HEARTBEAT" in
# "find"'s output, then the HEARTBEWAT file isn't getting updated
# for whatever reason and executes "killmud" script.
#
# find . -cmin $BEATTIME -name heartbeat -ls  #command template
#
# cmin set to 3 minutes.  HEARTBEAT is currently updated every two mins.
#
if `/usr/bin/find . -cmin +3 -name HEARTBEAT -ls | grep -q HEARTBEAT`;
then
	# On an Ubuntu Linux system, this command gets the process ID of the MUD
	# and kills it. You may need to change it depending on the output of your
	# ps command. TEST IT before running, mistakes can crash entire servers.
    `ps x|grep ModernMUD.exe|grep -v 'grep'|cut -b 1-5|xargs kill`
	# Assumes you have the mail command at /bin/mail and an account configured
	# to send mail to the specified user. Change the email address to match your
	# admins.
    /bin/mail -s "MUD heartbeat stopped.  Process killed!" user@example.com
fi
