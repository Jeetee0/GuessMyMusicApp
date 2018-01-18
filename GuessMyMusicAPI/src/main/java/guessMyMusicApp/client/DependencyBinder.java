package guessMyMusicApp.client;

import guessMyMusicApp.beans.IIpAddress;
import guessMyMusicApp.beans.IpAddress;
import org.glassfish.hk2.utilities.binding.AbstractBinder;

import javax.inject.Singleton;

public class DependencyBinder extends AbstractBinder {

    @Override
    protected void configure() {

        bind(IpAddress.class).to(IIpAddress.class).in(Singleton.class);
    }
}
