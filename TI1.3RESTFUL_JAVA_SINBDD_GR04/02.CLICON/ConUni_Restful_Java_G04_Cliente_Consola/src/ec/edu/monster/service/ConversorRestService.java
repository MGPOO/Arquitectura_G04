package ec.edu.monster.service;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class ConversorRestService {

    private static final String BASE_URL = "http://localhost:8080/ConUni_Resfull_Java_G04/api/convensorservice";

    // Método genérico para realizar peticiones GET
    private String enviarGet(String endpoint) throws Exception {
        URL url = new URL(BASE_URL + endpoint);
        HttpURLConnection con = (HttpURLConnection) url.openConnection();
        con.setRequestMethod("GET");

        int responseCode = con.getResponseCode();
        if (responseCode == HttpURLConnection.HTTP_OK) {
            try (BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()))) {
                String inputLine;
                StringBuilder response = new StringBuilder();
                while ((inputLine = in.readLine()) != null) {
                    response.append(inputLine);
                }
                return response.toString();
            }
        } else {
            throw new RuntimeException("Error: " + responseCode);
        }
    }

    // Llamadas específicas al servicio REST
    public double convertirLongitud(double valor, String origen, String destino) throws Exception {
        String endpoint = String.format("/longitud?valor=%s&origen=%s&destino=%s",
                valor, origen, destino);
        return Double.parseDouble(enviarGet(endpoint));
    }

    public double convertirMasa(double valor, String origen, String destino) throws Exception {
        String endpoint = String.format("/masa?valor=%s&origen=%s&destino=%s",
                valor, origen, destino);
        return Double.parseDouble(enviarGet(endpoint));
    }

    public double convertirTemperatura(double valor, String origen, String destino) throws Exception {
        String endpoint = String.format("/temperatura?valor=%s&origen=%s&destino=%s",
                valor, origen, destino);
        return Double.parseDouble(enviarGet(endpoint));
    }

    public String ping() throws Exception {
        return enviarGet("/ping");
    }
}
