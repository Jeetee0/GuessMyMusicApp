package guessMyMusicApp.raspberryPi.services;

import guessMyMusicApp.raspberryPi.shell.shellController;

import java.util.ArrayList;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import java.util.List;

@Path("/disco")
public class startDiscoService {

    @GET
    @Produces({ MediaType.TEXT_PLAIN })
    public Response startDisco(@QueryParam("cycles") String cycles, @QueryParam("delay") String delay, @QueryParam("msg") String msg, @QueryParam("filename") String filename) {

        //to start disco we have to execute the following command at the set workingDir: python print_frames.py
        String workingDir = "/home/pi/Projects/FrameModifier9001/FrameViewer/";
        List<String> commands = new ArrayList<String>();
        commands.add("python");
        commands.add("print_frames.py");

        //add arguments if given
        if (cycles != null) {
            commands.add("--cycles");
            commands.add(cycles);
        }
        if (delay != null) {
            commands.add("--delay");
            commands.add(delay);
        }
        if (msg != null) {
            commands.add("--msg");
            commands.add(msg);
        }
        if (filename != null) {
            commands.add("--filename");
            commands.add(filename);
        }
        
        shellController.executeCommand(workingDir, commands, false);

        String executedCommand = "";
        for (String command: commands) {
            executedCommand += command + " ";
        }

        //build response for user
        StringBuilder response = new StringBuilder();
        response.append("Raspberry pi started the Disco. Hell yeah!\n");
        response.append("executed command at: " + workingDir + "\n");
        response.append("executed command: " + executedCommand + "\n\n");

        return Response.ok(response.toString()).build();
    }

    @GET
    @Path("/wait")
    @Produces({ MediaType.TEXT_PLAIN })
    public Response startDiscoAndWaitForResponse(@QueryParam("cycles") String cycles, @QueryParam("delay") String delay, @QueryParam("msg") String msg, @QueryParam("filename") String filename) {

        //to start disco we have to execute the following command at the set workingDir: python print_frames.py
        String workingDir = "/home/pi/Projects/FrameModifier9001/FrameViewer/";
        List<String> commands = new ArrayList<String>();
        commands.add("python");
        commands.add("print_frames.py");

        //add arguments if given
        if (cycles != null) {
            commands.add("--cycles");
            commands.add(cycles);
        }
        if (delay != null) {
            commands.add("--delay");
            commands.add(delay);
        }
        if (msg != null) {
            commands.add("--msg");
            commands.add(msg);
        }
        if (filename != null) {
            commands.add("--filename");
            commands.add(filename);
        }

        String responseFromShell = shellController.executeCommand(workingDir, commands, true);

        String executedCommand = "";
        for (String command: commands) {
            executedCommand += command + " ";
        }

        //build response for user
        StringBuilder response = new StringBuilder();
        response.append("Raspberry pi started the Disco. Hell yeah!\n");
        response.append("executed command at: " + workingDir + "\n");
        response.append("executed command: " + executedCommand + "\n\n");
        response.append("response: " + responseFromShell);
        return Response.ok(response.toString()).build();
    }
}
