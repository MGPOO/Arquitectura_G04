/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.controller;

import ec.edu.monster.service.ConversorRestService;

/**
 *
 * @author Rabedon1
 */
public class ConvensorController {
    
    private final ConversorRestService restService;
    public ConvensorController() {
        this.restService = new ConversorRestService();
    }
    
    public double convertir(String tipo, double valor, String origen, String destino) throws Exception {
        switch (tipo.toLowerCase()) {
            case "longitud":
                return restService.convertirLongitud(valor, origen, destino);
            case "masa":
                return restService.convertirMasa(valor, origen, destino);
            case "temperatura":
                return restService.convertirTemperatura(valor, origen, destino);
            default:
                throw new IllegalArgumentException("Tipo de conversión no válido");
        }
    }
    
    public boolean verificarConexion() {
        try {
            String resp = restService.ping();
            System.out.println(resp);
            return true;
        } catch (Exception e) {
            System.out.println("❌ No se pudo conectar con el servicio REST.");
            return false;
        }
    }
}
