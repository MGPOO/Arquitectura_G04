/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/JSP_Servlet/Servlet.java to edit this template
 */
package ec.edu.monster.controller;

import java.io.IOException;
import java.io.PrintWriter;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.servlet.http.HttpSession;

/**
 *
 * @author Rabedon1
 */
@WebServlet(name = "Login", urlPatterns = {"/Login"})
public class Login extends HttpServlet {

    private static final String USUARIO = "admin";
    private static final String CLAVE = "1234";

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        
        String user = req.getParameter("usuario");
        String pass = req.getParameter("clave");
        
        if(USUARIO.equals(user) && CLAVE.equals(pass)) {
            HttpSession session = req.getSession();
            session.setAttribute("usuario", user);
            resp.sendRedirect("index.jsp");
        }else{
            resp.sendRedirect("login.jsp?error=true");
        }
        
    }

    
}
