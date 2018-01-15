package guessMyMusicApp.raspberryPi.services;

import guessMyMusicApp.raspberryPi.storage.GenreStorage;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import java.util.*;

@Path("/genres")
public class genreService {

    @GET
    @Path("/interprets/{genre}")
    @Produces({MediaType.APPLICATION_JSON})
    public Collection<String> getInterpretsForGenre(@PathParam("genre") String genre) {
        return GenreStorage.getInstance().getInterpretesForSpecificGenre(genre);
    }

    @GET
    @Path("/genreInfo/{genre}")
    @Produces({MediaType.TEXT_PLAIN})
    public Response getGenreInfo(@PathParam("genre") String genre) {
        return Response.ok(GenreStorage.getInstance().getGenreInfo(genre)).build();
    }
}
