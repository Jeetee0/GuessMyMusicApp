package raspberryPi.services;


import org.apache.commons.io.FileUtils;
import org.apache.commons.io.IOUtils;
import raspberryPi.shell.ShellController;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import java.io.*;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.List;

@Path("/FrameViewer")
public class FileHandlingService {

    @GET
    @Path("/getFilenames")
    @Produces({MediaType.TEXT_PLAIN})
    public Response getFilenames() {
        //String workingDir = "/Users/d064467/Development/git/FrameModifier9001/FrameViewer/txtFiles";
        String workingDir = "/home/pi/Projects/FrameModifier9001/FrameViewer/txtFiles/";
        List<String> commands = new ArrayList<String>();

        //command lists all the files in the directory
        commands.add("/bin/sh");
        commands.add("-c");
        commands.add("ls -p | grep -v /");

        String responseFromShell = ShellController.executeCommand(workingDir, commands, true);

        // convert to array
        //String[] filenameArray = responseFromShell.split("\n");

        return Response.ok(responseFromShell).build();
    }

    @GET
    @Path("getPattern/{filename}")
    @Produces({MediaType.TEXT_PLAIN})
    public Response getFile(@PathParam("filename") String filename) {
        //String workingDir = "/Users/d064467/Development/git/FrameModifier9001/FrameViewer/txtFiles/";
        String workingDir = "/home/pi/Projects/FrameModifier9001/FrameViewer/txtFiles/";

        String fileAsString = "";

        try {
            FileInputStream inputStream = new FileInputStream(workingDir + filename);
            try {
                fileAsString = IOUtils.toString(inputStream, "UTF-8");
            } finally {
                inputStream.close();
            }

        } catch (FileNotFoundException e) {
            fileAsString = "file not found";
        } catch (IOException e) {
            fileAsString = "IOexception";
        }

        return Response.ok(fileAsString).build();
    }

    @POST
    @Path("/savePattern/{filename}")
    public Response saveFile(String fileInLines, @PathParam("filename") String filename) {
        //String workingDir = "/Users/d064467/Development/git/FrameModifier9001/FrameViewer/txtFiles/";
        String workingDir = "/home/pi/Projects/FrameModifier9001/FrameViewer/txtFiles/";

        String response = "";

        File file;
        try {
            file = new File(workingDir + filename);

            if (file.exists())
                file.delete();

            file.createNewFile();

            FileUtils.writeByteArrayToFile(file, fileInLines.getBytes());

            response += "Successfully saved file\n";
            response += "saved to path: " + workingDir + "\n";
            response += "saved with filename: " + filename + "\n\n";

        } catch (IOException e) {
            e.printStackTrace();
            response = "could not save file";
        }

        return Response.ok(response).build();
    }
}
