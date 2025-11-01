/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.prueba;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 *
 * @author Rabedon1
 */
public class ConvensorUnidadesTest {
    
    private static final String BASE_URL = "http://localhost:8080/ConUni_Resfull_Java_G04/api/convensorservice";

    public static void main(String[] args) {
        System.out.println("=== PRUEBA DE SERVICIO REST ===");

        probarServicio("longitud?valor=5&origen=kilometros&destino=millas", "5 km → millas");
        probarServicio("longitud?valor=5&origen=millas&destino=metros", "5 millas → metros");
        probarServicio("masa?valor=1000&origen=gramos&destino=kilogramos", "1000 g → kg");
        probarServicio("temperatura?valor=0&origen=celsius&destino=fahrenheit", "0 °C → °F");
    }

    private static void probarServicio(String endpoint, String descripcion) {
        try {
            // Construir URL
            URL url = new URL(BASE_URL + "/" + endpoint);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");
            conn.setRequestProperty("Accept", "application/json");

            // Verificar respuesta HTTP
            if (conn.getResponseCode() != 200) {
                System.out.println(descripcion + " → ERROR (" + conn.getResponseCode() + ")");
                return;
            }

            // Leer respuesta del servidor
            BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            String output = br.readLine();

            // Mostrar resultado
            System.out.println(descripcion + " = " + output);

            conn.disconnect();

        } catch (Exception e) {
            System.out.println(descripcion + " → ERROR: " + e.getMessage());
        }
    }
}
