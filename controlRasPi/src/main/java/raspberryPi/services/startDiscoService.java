package raspberryPi.services;

import raspberryPi.shell.shellController;

import java.util.ArrayList;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import java.util.List;

// we have the two python scripts: disco.py & lauflicht.py that we can start
// in addition we can add some parameters to select the pattern we want to show, set delaytime between frames,
// add a startmessage etc.

//this service executes the right script with the given parameters as queryParams

@Path("/disco")
public class startDiscoService {

    @GET
    @Path("/standard")
    @Produces({MediaType.TEXT_PLAIN})
    public Response startDisco(@QueryParam("cycles") String cycles, @QueryParam("delay") String delay,
                               @QueryParam("mirror") String mirror, @QueryParam("msg") String msg,
                               @QueryParam("filename") String filename) {

        //to start disco we have to execute the following command at the set workingDir: python disco.py
        String workingDir = "/home/pi/Projects/FrameModifier9001/FrameViewer/";
        List<String> commands = new ArrayList<String>();
        commands.add("python");
        commands.add("disco.py");

        //add arguments if given
        if (cycles != null) {
            commands.add("--cycles");
            commands.add(cycles);
        }
        if (delay != null) {
            commands.add("--delay");
            commands.add(delay);
        }
        if (filename != null) {
            commands.add("--filename");
            commands.add(filename);
        }
        if (mirror != null) {
            commands.add("--mirror");
            commands.add(mirror);
        }
        if (msg != null) {
            commands.add("--msg");
            commands.add(mirror);
        }

        shellController.executeCommand(workingDir, commands, false);

        String executedCommand = "";
        for (String command : commands) {
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
    @Path("/standard/wait")
    @Produces({MediaType.TEXT_PLAIN})
    public Response startDiscoAndWaitForResponse(@QueryParam("cycles") String cycles, @QueryParam("delay") String delay,
                                                 @QueryParam("mirror") String mirror, @QueryParam("msg") String msg,
                                                 @QueryParam("filename") String filename) {

        //to start disco we have to execute the following command at the set workingDir: python print_frames.py
        String workingDir = "/home/pi/Projects/FrameModifier9001/FrameViewer/";
        List<String> commands = new ArrayList<String>();
        commands.add("python");
        commands.add("disco.py");

        //add arguments if given
        if (cycles != null) {
            commands.add("--cycles");
            commands.add(cycles);
        }
        if (delay != null) {
            commands.add("--delay");
            commands.add(delay);
        }
        if (filename != null) {
            commands.add("--filename");
            commands.add(filename);
        }
        if (mirror != null) {
            commands.add("--mirror");
            commands.add(mirror);
        }
        if (msg != null) {
            commands.add("--msg");
            commands.add(msg);
        }

        String responseFromShell = shellController.executeCommand(workingDir, commands, true);

        String executedCommand = "";
        for (String command : commands) {
            executedCommand += command + " ";
        }

        //build response for user
        StringBuilder response = new StringBuilder();
        response.append("Raspberry pi started the Disco. Hell yeah!\n");
        response.append("executed command at: " + workingDir + "\n");
        response.append("executed command: " + executedCommand + "\n\n");
        response.append("response:\n" + responseFromShell);
        return Response.ok(response.toString()).build();
    }

    @GET
    @Path("/lauflicht")
    @Produces({MediaType.TEXT_PLAIN})
    public Response startWalkingLight(@QueryParam("cycles") String cycles, @QueryParam("delay") String delay, @QueryParam("start") String start) {
        //to start disco we have to execute the following command at the set workingDir: python print_frames.py
        String workingDir = "/home/pi/Projects/FrameModifier9001/FrameViewer/";
        List<String> commands = new ArrayList<String>();
        commands.add("python");
        commands.add("lauflicht.py");

        //add arguments if given
        if (cycles != null) {
            commands.add("--cycles");
            commands.add(cycles);
        }
        if (delay != null) {
            commands.add("--delay");
            commands.add(delay);
        }
        if (start != null) {
            commands.add("--start");
            commands.add(start);
        }

        String responseFromShell = shellController.executeCommand(workingDir, commands, true);

        String executedCommand = "";
        for (String command : commands) {
            executedCommand += command + " ";
        }

        //build response for user
        StringBuilder response = new StringBuilder();
        response.append("Raspberry pi started the Disco. Hell yeah!\n");
        response.append("executed command at: " + workingDir + "\n");
        response.append("executed command: " + executedCommand + "\n\n");
        response.append("response:\n" + responseFromShell);
        return Response.ok(response.toString()).build();
    }

}
