cd ~/Development/git/GuessMyMusicApp/controlRasPi 
mvn clean package
#sleep 5s
cd ~
cp ~/Development/git/GuessMyMusicApp/controlRasPi/target/controlRasPi.war ~/Development/apache-tomcat-8.5.23/webapps/
scp -r ~/Development/apache-tomcat-8.5.23/webapps/controlRasPi.war pi@192.168.178.30:Development/apache-tomcat-8.5.24/webapps/