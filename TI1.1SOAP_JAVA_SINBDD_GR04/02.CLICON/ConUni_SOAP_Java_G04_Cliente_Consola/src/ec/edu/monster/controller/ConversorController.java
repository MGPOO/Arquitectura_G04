/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
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
                return servicio.convertirLongitud(valor, origen, destino);
            case "masa":
                return servicio.convertirMasa(valor, origen, destino);
            case "temperatura":
                return servicio.convertirTemperatura(valor, origen, destino);
            default:
                throw new IllegalArgumentException("Tipo de conversión no válido: " + tipo);
        }
    }
}
