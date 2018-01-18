package guessMyMusicApp.beans;

public class IpAddress implements IIpAddress {

    public IpAddress() {

    }

    private String ipAddress;

    public String getIpAddress() {
        return ipAddress;
    }

    public void setIpAddress(String ip) {
        this.ipAddress = ip;
    }
}
