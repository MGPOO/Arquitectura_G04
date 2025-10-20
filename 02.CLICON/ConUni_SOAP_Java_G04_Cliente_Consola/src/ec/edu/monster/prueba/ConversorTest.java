/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.prueba;

import ec.edu.monster.servicios.ConversorService;

/**
 *
 * @author Rabedon1
 */
public class ConversorTest {
    
       public static void main(String[] args) {
        System.out.println("=== PRUEBAS DE CONVERSOR SOAP ===");

        // 🔹 Pruebas de Longitud
        System.out.println("\n--- LONGITUD ---");
        System.out.println("5 kilómetros → millas = " +
                ConversorService.convertirLongitud(5, "kilometros", "millas"));
        System.out.println("10 millas → kilómetros = " +
                ConversorService.convertirLongitud(10, "millas", "kilometros"));
        System.out.println("200 metros → pies = " +
                ConversorService.convertirLongitud(200, "metros", "kilometros"));

        // 🔹 Pruebas de Masa
        System.out.println("\n--- MASA ---");
        System.out.println("1000 gramos → kilogramos = " +
                ConversorService.convertirMasa(1000, "gramos", "kilogramos"));
        System.out.println("2 kilogramos → libras = " +
                ConversorService.convertirMasa(2, "kilogramos", "libras"));
        System.out.println("10 libras → kilogramos = " +
                ConversorService.convertirMasa(10, "libras", "kilogramos"));

System.out.println("\n--- TEMPERATURA (Pruebas completas) ---");

try {
    System.out.println("1️⃣ 0 °C → °F = " +
        ConversorService.convertirTemperatura(0, "celsius", "fahrenheit"));
} catch (Exception e) {
    System.out.println("Error 1️⃣: " + e.getMessage());
}

try {
    System.out.println("2️⃣ 0 °C → K = " +
        ConversorService.convertirTemperatura(0, "celsius", "kelvin"));
} catch (Exception e) {
    System.out.println("Error 2️⃣: " + e.getMessage());
}

try {
    System.out.println("3️⃣ 100 °C → °F = " +
        ConversorService.convertirTemperatura(100, "celsius", "fahrenheit"));
} catch (Exception e) {
    System.out.println("Error 3️⃣: " + e.getMessage());
}

try {
    System.out.println("4️⃣ 32 °F → °C = " +
        ConversorService.convertirTemperatura(32, "fahrenheit", "celsius"));
} catch (Exception e) {
    System.out.println("Error 4️⃣: " + e.getMessage());
}

try {
    System.out.println("5️⃣ 32 °F → K = " +
        ConversorService.convertirTemperatura(32, "fahrenheit", "kelvin"));
} catch (Exception e) {
    System.out.println("Error 5️⃣: " + e.getMessage());
}

try {
    System.out.println("6️⃣ 273.15 K → °C = " +
        ConversorService.convertirTemperatura(273.15, "kelvin", "celsius"));
} catch (Exception e) {
    System.out.println("Error 6️⃣: " + e.getMessage());
}

try {
    System.out.println("7️⃣ 373.15 K → °F = " +
        ConversorService.convertirTemperatura(373.15, "kelvin", "fahrenheit"));
} catch (Exception e) {
    System.out.println("Error 7️⃣: " + e.getMessage());
}

try {
    System.out.println("8️⃣ 100 °C → °C = " +
        ConversorService.convertirTemperatura(100, "celsius", "celsius"));
} catch (Exception e) {
    System.out.println("Error 8️⃣: " + e.getMessage());
}

System.out.println("\n=== FIN DE LAS PRUEBAS ===");


    }
}
