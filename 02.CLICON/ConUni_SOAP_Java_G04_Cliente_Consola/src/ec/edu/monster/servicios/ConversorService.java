
package ec.edu.monster.servicios;



/**
 *
 * @author Rabedon1
 */
public class ConversorService {

    public static double convertirLongitud(double valor, java.lang.String unidadOrigen, java.lang.String unidadDestino) {
        ec.edu.monster.ws.ConversorService_Service service = new ec.edu.monster.ws.ConversorService_Service();
        ec.edu.monster.ws.ConversorService port = service.getConversorServicePort();
        return port.convertirLongitud(valor, unidadOrigen, unidadDestino);
    }

    public static double convertirMasa(double valor, java.lang.String unidadOrigen, java.lang.String unidadDestino) {
        ec.edu.monster.ws.ConversorService_Service service = new ec.edu.monster.ws.ConversorService_Service();
        ec.edu.monster.ws.ConversorService port = service.getConversorServicePort();
        return port.convertirMasa(valor, unidadOrigen, unidadDestino);
    }

    public static double convertirTemperatura(double valor, java.lang.String unidadOrigen, java.lang.String unidadDestino) {
        ec.edu.monster.ws.ConversorService_Service service = new ec.edu.monster.ws.ConversorService_Service();
        ec.edu.monster.ws.ConversorService port = service.getConversorServicePort();
        return port.convertirTemperatura(valor, unidadOrigen, unidadDestino);
    }

   
      
}
