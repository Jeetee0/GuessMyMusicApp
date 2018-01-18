#!/bin/bash
cd /home/pi/

STATE="error";

while [	$STATE == "error"	]
do
	sleep 2
	STATE=$(ping -q -w 1 -c 1 `ip r | grep default | cut -d ' ' -f 3` > /dev/null && echo ok || echo error)
done

ifconfig wlan0 | grep "inet " | awk -F'[: ]+' '{ print $3 }' >> ipaddress.txt
python send_ip_address.py
cd /