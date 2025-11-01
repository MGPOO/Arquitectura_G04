package ec.edu.monster.controller;

import ec.edu.monster.services.ConversorService;
import java.io.IOException;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

@WebServlet(name = "ConversorServletController", urlPatterns = {"/ConversorServletController"})
public class ConversorServletController extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        try {
            String tipo = request.getParameter("tipo"); // longitud, masa o temperatura
            double valor = Double.parseDouble(request.getParameter("valor"));
            String origen = request.getParameter("unidadOrigen");
            String destino = request.getParameter("unidadDestino");

            double resultado = 0.0;

            switch (tipo) {
                case "longitud":
                    resultado = ConversorService.convertirLongitud(valor, origen, destino);
                    break;
                case "masa":
                    resultado = ConversorService.convertirMasa(valor, origen, destino);
                    break;
                case "temperatura":
                    resultado = ConversorService.convertirTemperatura(valor, origen, destino);
                    break;
                default:
                    throw new ServletException("Tipo de conversión no válido: " + tipo);
            }

            // ✅ Guardamos los resultados en atributos de request
            request.setAttribute("tipo", tipo);
            request.setAttribute("valor", valor);
            request.setAttribute("origen", origen);
            request.setAttribute("destino", destino);
            request.setAttribute("resultado", resultado);

            // ✅ Reenviamos al mismo JSP
            request.getRequestDispatcher("index.jsp").forward(request, response);

        } catch (NumberFormatException e) {
            throw new ServletException("El valor ingresado no es un número válido.", e);
        } catch (Exception e) {
            throw new ServletException("Error al consumir el servicio web: " + e.getMessage(), e);
        }
    }
}
