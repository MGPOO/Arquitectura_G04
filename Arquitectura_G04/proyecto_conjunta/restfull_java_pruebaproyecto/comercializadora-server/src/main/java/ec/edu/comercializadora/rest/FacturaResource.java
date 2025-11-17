package ec.edu.comercializadora.rest;

import ec.edu.comercializadora.dto.FacturaRequest;
import ec.edu.comercializadora.entity.Factura;
import ec.edu.comercializadora.service.FacturaService;
import jakarta.inject.Inject;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

@Path("/invoices")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class FacturaResource {
    
    @Inject
    private FacturaService facturaService;
    
    @POST
    public Response crearFactura(FacturaRequest request) {
        try {
            Factura factura = facturaService.crearFactura(request);
            return Response.ok(factura).build();
        } catch (Exception e) {
            return Response.status(Response.Status.BAD_REQUEST)
                .entity("{\"error\": \"" + e.getMessage() + "\"}").build();
        }
    }
    
    @GET
    @Path("/{idFactura}")
    public Response obtenerFactura(@PathParam("idFactura") Integer idFactura) {
        try {
            Factura factura = facturaService.obtenerFactura(idFactura);
            
            if (factura == null) {
                return Response.status(Response.Status.NOT_FOUND)
                    .entity("{\"error\": \"Factura no encontrada\"}").build();
            }
            
            return Response.ok(factura).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                .entity("{\"error\": \"" + e.getMessage() + "\"}").build();
        }
    }
}
