package ec.edu.banquito.rest;

import ec.edu.banquito.entity.Cliente;
import ec.edu.banquito.service.CreditoService;
import jakarta.inject.Inject;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

@Path("/clients")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class ClienteResource {
    
    @Inject
    private CreditoService creditoService;
    
    @GET
    @Path("/{cedula}")
    public Response obtenerCliente(@PathParam("cedula") String cedula) {
        try {
            Cliente cliente = creditoService.obtenerClientePorCedula(cedula);
            
            if (cliente == null) {
                return Response.status(Response.Status.NOT_FOUND)
                    .entity("{\"error\": \"Cliente no encontrado\"}").build();
            }
            
            return Response.ok(cliente).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                .entity("{\"error\": \"" + e.getMessage() + "\"}").build();
        }
    }
}
