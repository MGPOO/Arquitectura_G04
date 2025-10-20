/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.servicios;

/**
 *
 * @author Rabedon1
 */
public class ConversorUnidadesService {
        // Longitud
    public double convertirLongitud(double valor, String origen, String destino) {
        double metros = 0;
        switch (origen.toLowerCase()) {
            case "metros": metros = valor; break;
            case "kilometros": metros = valor * 1000; break;
            case "millas": metros = valor * 1609.34; break;
            default: throw new IllegalArgumentException("Unidad origen no válida");
        }
        
        switch (destino.toLowerCase()) {
            case "metros": return metros;
            case "kilometros": return metros / 1000;
            case "millas": return metros / 1609.34;
            default: throw new IllegalArgumentException("Unidad destino no válida");
        }
        
    }
    
    // Masa
    public double convertirMasa(double valor, String origen, String destino) {
        double gramos = 0;
        switch (origen.toLowerCase()) {
            case "gramos": gramos = valor; break;
            case "kilogramos": gramos = valor * 1000; break;
            case "libras": gramos = valor * 453.592; break;
            default: throw new IllegalArgumentException("Unidad origen no válida");
        }

        switch (destino.toLowerCase()) {
            case "gramos": return gramos;
            case "kilogramos": return gramos / 1000;
            case "libras": return gramos / 453.592;
            default: throw new IllegalArgumentException("Unidad destino no válida");
        }
    }

    // Temperatura
    public double convertirTemperatura(double valor, String origen, String destino) {
        double celsius = 0;
        System.out.println(">>> Recibido: [" + origen + "] valor=" + valor);

        
        switch (origen.toLowerCase()) {
            case "celsius": celsius = valor; break;
            case "fahrenheit": celsius = (valor - 32) * 5 / 9; break;
            case "kelvin": celsius = valor - 273.15; break;
            default: throw new IllegalArgumentException("Unidad origen no válida");
        }

        switch (destino.toLowerCase()) {
            case "celsius": return celsius;
            case "fahrenheit": return (celsius * 9 / 5) + 32;
            case "kelvin": return celsius + 273.15;
            default: throw new IllegalArgumentException("Unidad destino no válida");
        }
    }
}
