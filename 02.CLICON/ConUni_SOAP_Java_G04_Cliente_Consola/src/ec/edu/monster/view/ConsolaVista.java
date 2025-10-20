/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.view;

import ec.edu.monster.controller.ConversorController;
import java.util.Scanner;

/**
 *
 * @author Rabedon1
 */
public class ConsolaVista {
    
    private final ConversorController controlador;
    private final Scanner sc;

    public ConsolaVista() {
        controlador = new ConversorController();
        sc = new Scanner(System.in);
    }

    public void iniciar() {
        int opcion = 0;

        do {
            System.out.println("\n=== MENÚ DE CONVERSIONES ===");
            System.out.println("1. Conversión de Longitud");
            System.out.println("2. Conversión de Masa");
            System.out.println("3. Conversión de Temperatura");
            System.out.println("4. Salir");
            System.out.print("Seleccione una opción: ");
            opcion = sc.nextInt();

            switch (opcion) {
                case 1 -> manejarConversion("longitud");
                case 2 -> manejarConversion("masa");
                case 3 -> manejarConversion("temperatura");
                case 4 -> System.out.println("Saliendo del programa...");
                default -> System.out.println("Opción no válida.");
            }

        } while (opcion != 4);
    }

    private void manejarConversion(String tipo) {
        System.out.println("\n--- Conversión de " + tipo.toUpperCase() + " ---");
        System.out.print("Ingrese valor: ");
        double valor = sc.nextDouble();

        System.out.print("Unidad origen: ");
        String origen = sc.next();

        System.out.print("Unidad destino: ");
        String destino = sc.next();

        try {
            double resultado = controlador.convertir(tipo, valor, origen, destino);
            System.out.println("Resultado: " + resultado + " " + destino);
        } catch (Exception e) {
            System.out.println("Error en la conversión: " + e.getMessage());
        }
    }

    public static void main(String[] args) {
        new ConsolaVista().iniciar();
    }
}
