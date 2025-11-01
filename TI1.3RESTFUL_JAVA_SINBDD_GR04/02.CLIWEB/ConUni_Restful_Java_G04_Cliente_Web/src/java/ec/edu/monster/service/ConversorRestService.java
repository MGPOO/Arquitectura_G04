/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.service;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 *
 * @author Rabedon1
 */
public class ConversorRestService {
    
    private static final String BASE_URL = "http://localhost:8080/ConUni_Resfull_Java_G04/api/convensorservice";

    private String enviarGet(String endpoint) throws Exception {
        URL url = new URL(BASE_URL + endpoint);
        HttpURLConnection con = (HttpURLConnection) url.openConnection();
        con.setRequestMethod("GET");

        int responseCode = con.getResponseCode();
        if (responseCode == HttpURLConnection.HTTP_OK) {
            try (BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()))) {
                StringBuilder response = new StringBuilder();
                String line;
                while ((line = in.readLine()) != null) {
                    response.append(line);
                }
                return response.toString();
            }
        } else {
            throw new RuntimeException("Error HTTP: " + responseCode);
        }
    }

    public double convertirLongitud(double valor, String origen, String destino) throws Exception {
        String endpoint = String.format("/longitud?valor=%s&origen=%s&destino=%s", valor, origen, destino);
        return Double.parseDouble(enviarGet(endpoint));
    }

    public double convertirMasa(double valor, String origen, String destino) throws Exception {
        String endpoint = String.format("/masa?valor=%s&origen=%s&destino=%s", valor, origen, destino);
        return Double.parseDouble(enviarGet(endpoint));
    }

    public double convertirTemperatura(double valor, String origen, String destino) throws Exception {
        String endpoint = String.format("/temperatura?valor=%s&origen=%s&destino=%s", valor, origen, destino);
        return Double.parseDouble(enviarGet(endpoint));
    }
}
