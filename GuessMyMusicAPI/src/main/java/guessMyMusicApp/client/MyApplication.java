package guessMyMusicApp.client;

import org.glassfish.jersey.server.ResourceConfig;

public class MyApplication extends ResourceConfig {

    public MyApplication() {
        register(new DependencyBinder());
        packages("guessMyMusicApp.services");
    }

}
