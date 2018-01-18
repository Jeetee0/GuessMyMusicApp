package guessMyMusicApp.services;

import guessMyMusicApp.beans.IIpAddress;

import javax.inject.Inject;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.core.Response;

@Path("/ip")
public class IpService {

    @Inject
    private IIpAddress ipAddress;

    @GET
    public Response getIpAddressOfRaspberryPi() {
        String ip = ipAddress.getIpAddress();
        if (ip != null)
            return Response.ok(ip).build();
        return Response.status(Response.Status.NOT_FOUND).build();
    }

    @POST
    @Path("/{raspiIp}")
    public Response saveIpAddressOfRaspberryPi(@PathParam("raspiIp") String ip) {
        ipAddress.setIpAddress(ip);
        return Response.ok().build();
    }
}
