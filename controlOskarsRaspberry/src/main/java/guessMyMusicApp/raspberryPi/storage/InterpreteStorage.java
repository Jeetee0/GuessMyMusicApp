package guessMyMusicApp.raspberryPi.storage;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import guessMyMusicApp.raspberryPi.beans.Genre;

import java.io.IOException;
import java.io.InputStream;
import java.util.*;

public class InterpreteStorage {
    //use map to store interprets per genre
    private static InterpreteStorage instance = null;
    private static Map<String, Genre> storage;

    private InterpreteStorage() {

        //get "database" from json, read file and parse into hashmap storage
        storage = new HashMap<String, Genre>();
        String jsonFile = "GenreMap.json";
        ObjectMapper objectMapper = new ObjectMapper();
        InputStream input = this.getClass().getClassLoader().getResourceAsStream(jsonFile);

        try {
            List<Genre> genreList = (List<Genre>) objectMapper.readValue(input, new TypeReference<List<Genre>>() {});

            //parse into storage
            for (Genre genre: genreList) {
                storage.put(genre.name, genre);
            }

        } catch (IOException e) {
            System.out.println("could not read json file.");
        }
    }

    public synchronized static InterpreteStorage getInstance() {
        if (instance == null) {
            instance = new InterpreteStorage();
        }
        return instance;
    }

    public Collection<String> getInterpretesForSpecificGenre(String genre) {
        if (storage.get(genre) != null)
            return storage.get(genre).getInterpretes();
        else
            return null;
    }

}
