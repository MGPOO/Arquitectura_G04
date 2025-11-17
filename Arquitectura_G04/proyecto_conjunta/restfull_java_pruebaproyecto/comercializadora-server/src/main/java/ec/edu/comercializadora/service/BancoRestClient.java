package ec.edu.comercializadora.service;

import ec.edu.comercializadora.dto.EvaluacionCreditoRequest;
import ec.edu.comercializadora.dto.EvaluacionCreditoResponse;
import jakarta.ejb.Stateless;
import jakarta.ws.rs.client.Client;
import jakarta.ws.rs.client.ClientBuilder;
import jakarta.ws.rs.client.Entity;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

@Stateless
public class BancoRestClient {
    
    private static final String BANCO_URL = "http://localhost:8080/banquito-server/api";
    
    public EvaluacionCreditoResponse evaluarCredito(EvaluacionCreditoRequest request) {
        Client client = ClientBuilder.newClient();
        
        try {
            Response response = client.target(BANCO_URL)
                .path("/credits/evaluate")
                .request(MediaType.APPLICATION_JSON)
                .post(Entity.json(request));
            
            if (response.getStatus() == 200) {
                return response.readEntity(EvaluacionCreditoResponse.class);
            } else {
                EvaluacionCreditoResponse errorResponse = new EvaluacionCreditoResponse();
                errorResponse.setAprobado(false);
                errorResponse.setMotivo("Error al comunicarse con el banco: " + response.getStatus());
                return errorResponse;
            }
        } catch (Exception e) {
            EvaluacionCreditoResponse errorResponse = new EvaluacionCreditoResponse();
            errorResponse.setAprobado(false);
            errorResponse.setMotivo("Error de conexión con el banco: " + e.getMessage());
            return errorResponse;
        } finally {
            client.close();
        }
    }
}
