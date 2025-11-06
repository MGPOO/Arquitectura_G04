package ec.edu.monster.view;

import ec.edu.monster.controller.ConversorController;
import java.util.InputMismatchException;
import java.util.Scanner;

public class ConsolaVista {

    private final ConversorController controlador;
    private final Scanner sc;

    public ConsolaVista() {
        controlador = new ConversorController();
        sc = new Scanner(System.in);
    }

    public void iniciar() {
        if (!iniciarSesion()) {
            System.out.println("❌ Demasiados intentos fallidos. Cerrando programa...");
            return;
        }

        int opcion;

        do {
            System.out.println("\n=== MENÚ DE CONVERSIONES ===");
            System.out.println("1. Conversión de Longitud");
            System.out.println("2. Conversión de Masa");
            System.out.println("3. Conversión de Temperatura");
            System.out.println("4. Salir");
            System.out.print("Seleccione una opción: ");
            while (!sc.hasNextInt()) {
                System.out.println("⚠️ Debe ingresar un número (1-4). Intente de nuevo.");
                sc.next();
            }
            opcion = sc.nextInt();

            switch (opcion) {
                case 1 -> manejarConversion("longitud");
                case 2 -> manejarConversion("masa");
                case 3 -> manejarConversion("temperatura");
                case 4 -> System.out.println("👋 Saliendo del programa...");
                default -> System.out.println("❌ Opción no válida, intente de nuevo.");
            }

        } while (opcion != 4);
    }

    private boolean iniciarSesion() {
        String usuarioValido = "MONSTER";
        String contrasenaValida = "monster9";

        System.out.println("===================================");
        System.out.println("     SISTEMA DE CONVERSIONES");
        System.out.println("===================================");

        int intentos = 0;

        while (intentos < 3) {
            System.out.print("Usuario: ");
            String usuario = sc.next();

            System.out.print("Contraseña: ");
            String contrasena = sc.next();

            if (usuario.equals(usuarioValido) && contrasena.equals(contrasenaValida)) {
                System.out.println("\n✅ Inicio de sesión exitoso. ¡Bienvenido, " + usuario + "!");
                return true;
            } else {
                intentos++;
                System.out.println("❌ Credenciales incorrectas. Intento " + intentos + " de 3.");
            }
        }

        return false;
    }

    private void manejarConversion(String tipo) {
        System.out.println("\n--- Conversión de " + tipo.toUpperCase() + " ---");

        double valor = leerValor(tipo);

        System.out.print("Unidad origen: ");
        String origen = sc.next();

        System.out.print("Unidad destino: ");
        String destino = sc.next();

        try {
            double resultado = controlador.convertir(tipo, valor, origen, destino);
            System.out.println("✅ Resultado: " + valor + " " + origen + " = " + resultado + " " + destino);
        } catch (Exception e) {
            System.out.println("⚠️ Error en la conversión: " + e.getMessage());
        }
    }

    /**
     * Valida el valor numérico según el tipo de conversión
     */
    private double leerValor(String tipo) {
        double valor = -1;
        boolean valido = false;

        while (!valido) {
            System.out.print("Ingrese valor para " + tipo + ": ");
            try {
                valor = sc.nextDouble();

                switch (tipo) {
                    case "longitud":
                    case "masa":
                        if (valor <= 0) {
                            System.out.println("⚠️ El valor debe ser mayor que 0.");
                        } else if (valor > 1000) {
                            System.out.println("⚠️ El valor máximo permitido es 1000.");
                        } else {
                            valido = true;
                        }
                        break;

                    case "temperatura":
                        if (valor < -273.15) {
                            System.out.println("⚠️ No puede ser menor que el cero absoluto (-273.15 °C).");
                        } else if (valor > 1000) {
                            System.out.println("⚠️ El valor máximo permitido es 1000 °C.");
                        } else {
                            valido = true;
                        }
                        break;

                    default:
                        valido = true;
                        break;
                }

            } catch (InputMismatchException e) {
                System.out.println("❌ Entrada no válida. Debe ingresar un número.");
                sc.next(); // limpiar buffer
            }
        }

        return valor;
    }

    public static void main(String[] args) {
        new ConsolaVista().iniciar();
    }
}
