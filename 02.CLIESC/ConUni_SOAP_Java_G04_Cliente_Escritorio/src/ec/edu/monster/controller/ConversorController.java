package ec.edu.monster.controller;

import ec.edu.monster.servicios.ConversorService;

/**
 *
 * @author Rabedon1
 */
public class ConversorController {
    
      private final ConversorService servicio;

    public ConversorController() {
        // Creamos la instancia del servicio
        servicio = new ConversorService();
    }

    public double convertir(String tipo, double valor, String origen, String destino) {
        
        switch (tipo.toLowerCase()) {
            case "longitud":
                if(valor >= 0 && valor < 10000){
                return servicio.convertirLongitud(valor, origen, destino);
            }else {
                    throw new IllegalArgumentException(
                            "El valor para longitud dese estar entre 0 y 10000"
                    );
                }
            case "masa":
                if(valor >= 0 && valor < 10000){
                return servicio.convertirMasa(valor, origen, destino);
            }else {
                    throw new IllegalArgumentException(
                            "El valor para la masa debe estar entre 0 y 10000"
                    );
                }
            case "temperatura":
                return servicio.convertirTemperatura(valor, origen, destino);
            default:
                throw new IllegalArgumentException("Tipo de conversión no válido: " + tipo);
        }
    }
}

