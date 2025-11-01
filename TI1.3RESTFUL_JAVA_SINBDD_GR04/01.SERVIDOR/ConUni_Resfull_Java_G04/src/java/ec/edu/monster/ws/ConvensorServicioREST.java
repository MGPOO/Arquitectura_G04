/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/GenericResource.java to edit this template
 */
package ec.edu.monster.ws;

import ec.edu.monster.servicios.ConversorServicios;
import jakarta.ws.rs.core.Context;
import jakarta.ws.rs.core.UriInfo;
import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.GET;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.PUT;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.QueryParam;
import jakarta.ws.rs.core.MediaType;

/**
 * REST Web Service
 *
 * @author Rabedon1
 */
@Path("convensorservice")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class ConvensorServicioREST {
    
     private final ConversorServicios service = new ConversorServicios();
     
      public ConvensorServicioREST() {
    }
      
    @GET
    @Path("ping")
    @Produces(MediaType.TEXT_PLAIN)
    public String ping() {
        return "Servicio REST de conversion funcionando correctamente";
    }

    // ================= Longitud =================
    // GET ejemplo: /api/conversor/longitud?valor=10&origen=metros&destino=kilometros
    @GET
    @Path("longitud")
    public double convertirLongitud(
            @QueryParam("valor") double valor,
            @QueryParam("origen") String origen,
            @QueryParam("destino") String destino) {
        return service.convertirLongitud(valor, origen, destino);
    }

    // ================= Masa =================
    // GET ejemplo: /api/conversor/masa?valor=5&origen=kilogramos&destino=libras
    @GET
    @Path("masa")
    public double convertirMasa(
            @QueryParam("valor") double valor,
            @QueryParam("origen") String origen,
            @QueryParam("destino") String destino) {
        return service.convertirMasa(valor, origen, destino);
    }

    // ================= Temperatura =================
    // GET ejemplo: /api/conversor/temperatura?valor=100&origen=celsius&destino=fahrenheit
    @GET
    @Path("temperatura")
    public double convertirTemperatura(
            @QueryParam("valor") double valor,
            @QueryParam("origen") String origen,
            @QueryParam("destino") String destino) {
        return service.convertirTemperatura(valor, origen, destino);
    }

}
