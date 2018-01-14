cd ~/Development/git/GuessMyMusicApp/controlOskarsRaspberry 
mvn clean package
#sleep 5s
cd ~
cp ~/Development/git/GuessMyMusicApp/controlOskarsRaspberry/target/controlOskarsRaspberryPi.war ~/Development/apache-tomcat-8.5.23/webapps/
scp -r ~/Development/apache-tomcat-8.5.23/webapps/controlOskarsRaspberryPi.war pi@192.168.178.30:Development/apache-tomcat-8.5.24/webapps/