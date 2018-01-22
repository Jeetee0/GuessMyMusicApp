# GuessMyMusic
This is a project for the HTW Berlin in the module: Mobile Anwendungen.

I am creating an App, where you can determine the BPM of a Song and then select the genre you are listening to.

I connected this project with my other Project: FrameModifier9001 (https://github.com/Jeetee0/FrameModifier9001).
In my app you can connect via ssh to my raspberry pi to control the FrameViewer from the other project.
Also i created a webservice in java to control the FrameViewer via REST communication.
I am also calling this webservice to get interprets that represent a specific genre.

In German:
Mit der App "GuessMyMusic" kann man herausfinden, welche "beats per minute"-Anzahl seine Lieblingssongs besitzen.
Um das herauszufinden, wurde ein BPM-Tapper programmiert. Seine wichtigen "Taps" kann man abspeichern.
Diese werden automatisch in ein Musik Genre eingeordnet. Man kann seine "Taps" einsehen und editieren.
Zusätzlich gibt es eine Übersicht der (hauptsächlich elektronischen) Genres, 
in der man Zusatzinformationen zu dem ausgewählten Genre bekommt.

Für mich privat verband ich dieses Projekt mit einem Anderen. Ich baute eine Verbindung zu meinem 
Raspberry Pi auf, um die dort angeschlossenen LED Matrizen anzusteuern und die Muster, passend 
im Takt zur Musik zu ansteuern zu können.
