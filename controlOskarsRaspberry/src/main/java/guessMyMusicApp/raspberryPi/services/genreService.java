package guessMyMusicApp.raspberryPi.services;

import com.fasterxml.jackson.databind.ObjectMapper;
import guessMyMusicApp.raspberryPi.beans.Genre;
import guessMyMusicApp.raspberryPi.storage.InterpreteStorage;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.*;

@Path("/requestInterprets")
public class genreService {

    @GET
    @Path("/{genre}")
    @Produces({MediaType.APPLICATION_JSON})
    public Collection<String> getInterpretsForGenre(@PathParam("genre") String genre) {
        return InterpreteStorage.getInstance().getInterpretesForSpecificGenre(genre);
    }
}
