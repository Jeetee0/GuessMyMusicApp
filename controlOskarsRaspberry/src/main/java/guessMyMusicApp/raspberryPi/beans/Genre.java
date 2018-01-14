package guessMyMusicApp.raspberryPi.beans;

import java.util.Collection;
import java.util.List;

public class Genre {

    public String name;
    private List<String> interpretes;

    public Genre (String name, List<String> interpretes) {
        this.name = name;
        this.interpretes = interpretes;
    }

    public Genre() {

    }

    public List<String> getInterpretes() {
        return interpretes;
    }

}
