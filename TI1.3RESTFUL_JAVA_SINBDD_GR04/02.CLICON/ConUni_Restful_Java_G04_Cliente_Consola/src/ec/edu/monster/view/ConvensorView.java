/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.view;

import ec.edu.monster.controller.ConvensorController;
import java.util.Scanner;

/**
 *
 * @author Rabedon1
 */
public class ConvensorView {
    
    public static void main(String[] args) {
        
         Scanner sc = new Scanner(System.in);
         ConvensorController controller = new ConvensorController();
    
        System.out.println("=========================================");
        System.out.println("     CLIENTE CONSOLA - CONVERSOR REST");
        System.out.println("=========================================");

     
        if (!controller.verificarConexion()) {
            System.out.println("No se puede continuar. Verifique que Payara esté en ejecución.");
            return;
        }

        boolean continuar = true;
        while (continuar) {
            System.out.println("\nSeleccione el tipo de conversión:");
            System.out.println("1. Longitud");
            System.out.println("2. Masa");
            System.out.println("3. Temperatura");
            System.out.println("0. Salir");
            System.out.print("→ Opción: ");
            int opcion = sc.nextInt();
            sc.nextLine(); // limpiar buffer

            switch (opcion) {
                case 1 -> manejarConversion("longitud", sc, controller);
                case 2 -> manejarConversion("masa", sc, controller);
                case 3 -> manejarConversion("temperatura", sc, controller);
                case 0 -> {
                    continuar = false;
                    System.out.println("👋 Programa finalizado. ¡Hasta luego!");
                }
                default -> System.out.println("❌ Opción no válida. Intente de nuevo.");
            }
        }
    }

    private static void manejarConversion(String tipo, Scanner sc, ConvensorController controller) {
        System.out.println("\n=== Conversión de " + tipo.toUpperCase() + " ===");

        switch (tipo) {
            case "longitud" -> {
                System.out.println("Unidades disponibles: metros, kilometros, millas");
            }
            case "masa" -> {
                System.out.println("Unidades disponibles: gramos, kilogramos, libras");
            }
            case "temperatura" -> {
                System.out.println("Unidades disponibles: celsius, fahrenheit, kelvin");
            }
        }

        System.out.print("Ingrese el valor a convertir: ");
        double valor = sc.nextDouble();
        sc.nextLine();

        System.out.print("Unidad origen: ");
        String origen = sc.nextLine();

        System.out.print("Unidad destino: ");
        String destino = sc.nextLine();

        try {
            double resultado = controller.convertir(tipo, valor, origen, destino);
            System.out.printf("✅ Resultado: %.4f %s = %.4f %s%n", valor, origen, resultado, destino);
        } catch (Exception e) {
            System.out.println("❌ Error al realizar la conversión: " + e.getMessage());
        }
    }
}
