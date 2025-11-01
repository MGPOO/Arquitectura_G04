package ec.edu.monster.controller;

import ec.edu.monster.service.ConversorRestService;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet(name = "ConversorRestController", urlPatterns = {"/ConversorRestController"})
public class ConversorController extends HttpServlet {

    private final ConversorRestService restService = new ConversorRestService();

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        try {
            String tipo = request.getParameter("tipo");
            double valor = Double.parseDouble(request.getParameter("valor"));
            String origen = request.getParameter("unidadOrigen");
            String destino = request.getParameter("unidadDestino");

            double resultado = 0.0;

            switch (tipo) {
                case "longitud":
                    resultado = restService.convertirLongitud(valor, origen, destino);
                    break;
                case "masa":
                    resultado = restService.convertirMasa(valor, origen, destino);
                    break;
                case "temperatura":
                    resultado = restService.convertirTemperatura(valor, origen, destino);
                    break;
                default:
                    throw new IllegalArgumentException("Tipo de conversión no válido");
            }

            // ✅ Enviar datos al JSP
            request.setAttribute("tipo", tipo);
            request.setAttribute("valor", valor);
            request.setAttribute("origen", origen);
            request.setAttribute("destino", destino);
            request.setAttribute("resultado", resultado);

        } catch (Exception e) {
            request.setAttribute("error", "❌ Error al consumir el servicio REST: " + e.getMessage());
        }

        // ✅ Redirigir de nuevo al mismo JSP
        request.getRequestDispatcher("index.jsp").forward(request, response);
    }
}
