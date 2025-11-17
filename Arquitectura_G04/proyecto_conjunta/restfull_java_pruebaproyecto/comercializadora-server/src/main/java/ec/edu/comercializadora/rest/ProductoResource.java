package ec.edu.comercializadora.rest;

import ec.edu.comercializadora.entity.Producto;
import ec.edu.comercializadora.service.FacturaService;
import jakarta.inject.Inject;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

import java.util.List;

@Path("/products")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class ProductoResource {
    
    @Inject
    private FacturaService facturaService;
    
    @GET
    public Response listarProductos() {
        try {
            List<Producto> productos = facturaService.listarProductos();
            return Response.ok(productos).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                .entity("{\"error\": \"" + e.getMessage() + "\"}").build();
        }
    }
}
