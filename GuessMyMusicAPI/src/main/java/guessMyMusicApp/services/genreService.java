package guessMyMusicApp.services;

import guessMyMusicApp.storage.GenreStorage;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import java.util.*;

@Path("/genres")
public class genreService {

    @GET
    @Path("/interpreters/{genre}")
    @Produces({MediaType.APPLICATION_JSON})
    public Collection<String> getInterpretsForGenre(@PathParam("genre") String genre) {
        return GenreStorage.getInstance().getInterpretersForSpecificGenre(genre);
    }

    @GET
    @Path("/genreInfo/{genre}")
    @Produces({MediaType.TEXT_PLAIN})
    public Response getGenreInfo(@PathParam("genre") String genre) {
        return Response.ok(GenreStorage.getInstance().getGenreInfo(genre)).build();
    }
}
