package ec.edu.monster.ws;

import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import jakarta.jws.WebService;
import ec.edu.monster.servicios.ConversorUnidadesService;

/**
 *
 * @author Rabedon1
 */
@WebService(serviceName = "ConversorService")
public class ConversorService {
    
    private final ConversorUnidadesService service = new ConversorUnidadesService();
    
    @WebMethod(operationName = "convertirLongitud")
    public double convertirLongitud(
            @WebParam(name = "valor") double valor,
            @WebParam(name = "unidadOrigen") String origen,
            @WebParam(name = "unidadDestino") String destino) {
        return service.convertirLongitud(valor, origen, destino);
    }
    
    @WebMethod(operationName = "convertirMasa")
    public double convertirMasa(
            @WebParam(name = "valor") double valor,
            @WebParam(name = "unidadOrigen") String origen,
            @WebParam(name = "unidadDestino") String destino) {
        return service.convertirMasa(valor, origen, destino);
    }
    
    @WebMethod(operationName = "convertirTemperatura")
    public double convertirTemperatura(
            @WebParam(name = "valor") double valor,
            @WebParam(name = "unidadOrigen") String origen,
            @WebParam(name = "unidadDestino") String destino) {
        return service.convertirTemperatura(valor, origen, destino);
    }
}
