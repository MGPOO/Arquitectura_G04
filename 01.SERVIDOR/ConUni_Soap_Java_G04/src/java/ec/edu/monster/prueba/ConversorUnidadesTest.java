/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.prueba;

import ec.edu.monster.servicios.ConversorUnidadesService;

/**
 *
 * @author Rabedon1
 */
public class ConversorUnidadesTest {
    
     public static void main(String[] args) {
        ConversorUnidadesService service = new ConversorUnidadesService ();

        System.out.println("=== Prueba de Conversor ===");
        System.out.println("5 km → millas = " + service.convertirLongitud(5, "kilometros", "millas"));
        System.out.println("5 millas → metros = " + service.convertirLongitud(5, "millas", "metros"));
        System.out.println("1000 g → kg = " + service.convertirMasa(1000, "gramos", "kilogramos"));
        System.out.println("0 °C → °F = " + service.convertirTemperatura(0, "celsius", "fahrenheit"));
    }
}
