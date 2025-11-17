package ec.edu.banquito.rest;

import ec.edu.banquito.dto.CuotaCreditoDTO;
import ec.edu.banquito.dto.EvaluacionCreditoRequest;
import ec.edu.banquito.dto.EvaluacionCreditoResponse;
import ec.edu.banquito.entity.Cliente;
import ec.edu.banquito.service.CreditoService;
import jakarta.inject.Inject;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

import java.util.List;

@Path("/credits")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class CreditoResource {
    
    @Inject
    private CreditoService creditoService;
    
    @POST
    @Path("/evaluate")
    public Response evaluarCredito(EvaluacionCreditoRequest request) {
        try {
            EvaluacionCreditoResponse response = creditoService.evaluarCredito(request);
            return Response.ok(response).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                .entity("{\"error\": \"" + e.getMessage() + "\"}").build();
        }
    }
    
    @GET
    @Path("/{idCredito}/schedule")
    public Response obtenerTablaAmortizacion(@PathParam("idCredito") Integer idCredito) {
        try {
            List<CuotaCreditoDTO> cuotas = creditoService.obtenerTablaAmortizacion(idCredito);
            
            if (cuotas.isEmpty()) {
                return Response.status(Response.Status.NOT_FOUND)
                    .entity("{\"error\": \"Crédito no encontrado\"}").build();
            }
            
            return Response.ok(cuotas).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                .entity("{\"error\": \"" + e.getMessage() + "\"}").build();
        }
    }
}
