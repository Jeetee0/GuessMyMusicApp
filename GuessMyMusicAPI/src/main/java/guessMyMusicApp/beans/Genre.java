package guessMyMusicApp.beans;

import java.util.List;

public class Genre {

    private String name;
    private String infoText;
    private List<String> interpretes;

    public Genre (String name, String infoText, List<String> interpretes) {
        this.name = name;
        this.infoText = infoText;
        this.interpretes = interpretes;
    }

    public Genre() {

    }

    public String getName() {
        return name;
    }

    public String getInfoText() {
        return infoText;
    }

    public List<String> getInterpretes() {
        return interpretes;
    }

}
