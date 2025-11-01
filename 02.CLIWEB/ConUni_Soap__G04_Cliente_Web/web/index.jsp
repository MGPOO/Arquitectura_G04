<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%
    // Simula autenticación si aún no tienes login
    String usuario = "usuarioDemo";
    session.setAttribute("usuario", usuario);

    String tipo = (String) request.getAttribute("tipo");
    Double valor = (Double) request.getAttribute("valor");
    String origen = (String) request.getAttribute("origen");
    String destino = (String) request.getAttribute("destino");
    Double resultado = (Double) request.getAttribute("resultado");
    String error = (String) request.getAttribute("error");
%>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Conversor Universal (REST)</title>
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background: #f3f7f7;
            margin: 0;
            padding: 0;
            text-align: center;
        }
        h1 {
            background-color: #185a9d;
            color: white;
            padding: 20px;
            margin: 0;
        }
        h2 {
            color: #185a9d;
        }
        .container {
            display: flex;
            justify-content: center;
            align-items: flex-start;
            gap: 40px;
            margin-top: 50px;
            flex-wrap: wrap;
        }
        .card {
            background: white;
            width: 300px;
            padding: 25px;
            border-radius: 15px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
            text-align: center;
        }
        select, input {
            width: 90%;
            padding: 8px;
            margin: 8px 0;
            border: 1px solid #ccc;
            border-radius: 8px;
        }
        button {
            background-color: #e63946;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 8px;
            cursor: pointer;
            font-weight: bold;
            margin-top: 10px;
        }
        button:hover {
            background-color: #2bb673;
        }
        .logout {
            position: absolute;
            top: 20px;
            right: 30px;
            color: white;
            text-decoration: none;
            font-weight: bold;
            background-color: #d9534f;
            padding: 8px 15px;
            border-radius: 6px;
        }
        .logout:hover {
            background-color: #b52b27;
        }
        .result {
            background: #eaffea;
            border: 1px solid #43cea2;
            border-radius: 10px;
            padding: 10px;
            margin-top: 10px;
            color: #185a9d;
            font-weight: bold;
        }
        .error {
            background: #ffecec;
            border: 1px solid #e74c3c;
            color: #c0392b;
            border-radius: 10px;
            padding: 10px;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <h1>Conversor de Unidades (Servicio REST)</h1>
    <a href="login.jsp" class="logout">Cerrar sesión</a>

    <div class="container">

        <!-- CONVERSIÓN DE LONGITUD -->
        <div class="card">
            <h2>Longitud</h2>
            <form action="ConversorRestController" method="post">
                <input type="hidden" name="tipo" value="longitud">
                <input type="number" name="valor" step="0.01" placeholder="Ingrese valor" required><br>

                <label>De:</label>
                <select name="unidadOrigen">
                    <option value="metros">Metros</option>
                    <option value="kilometros">Kilómetros</option>
                    <option value="millas">Millas</option>
                </select><br>

                <label>A:</label>
                <select name="unidadDestino">
                    <option value="metros">Metros</option>
                    <option value="kilometros">Kilómetros</option>
                    <option value="millas">Millas</option>
                </select><br>

                <button type="submit">Calcular</button>
            </form>

            <% if ("longitud".equals(tipo)) { %>
                <div class="<%= error == null ? "result" : "error" %>">
                    <% if (error == null) { %>
                        <%= valor %> <%= origen %> = <%= resultado %> <%= destino %>
                    <% } else { %>
                        <%= error %>
                    <% } %>
                </div>
            <% } %>
        </div>

        <!-- CONVERSIÓN DE MASA -->
        <div class="card">
            <h2>Masa</h2>
            <form action="ConversorRestController" method="post">
                <input type="hidden" name="tipo" value="masa">
                <input type="number" name="valor" step="0.01" placeholder="Ingrese valor" required><br>

                <label>De:</label>
                <select name="unidadOrigen">
                    <option value="gramos">Gramos</option>
                    <option value="kilogramos">Kilogramos</option>
                    <option value="libras">Libras</option>
                </select><br>

                <label>A:</label>
                <select name="unidadDestino">
                    <option value="gramos">Gramos</option>
                    <option value="kilogramos">Kilogramos</option>
                    <option value="libras">Libras</option>
                </select><br>

                <button type="submit">Calcular</button>
            </form>

            <% if ("masa".equals(tipo)) { %>
                <div class="<%= error == null ? "result" : "error" %>">
                    <% if (error == null) { %>
                        <%= valor %> <%= origen %> = <%= resultado %> <%= destino %>
                    <% } else { %>
                        <%= error %>
                    <% } %>
                </div>
            <% } %>
        </div>

        <!-- CONVERSIÓN DE TEMPERATURA -->
        <div class="card">
            <h2>Temperatura</h2>
            <form action="ConversorRestController" method="post">
                <input type="hidden" name="tipo" value="temperatura">
                <input type="number" name="valor" step="0.01" placeholder="Ingrese valor" required><br>

                <label>De:</label>
                <select name="unidadOrigen">
                    <option value="celsius">Celsius</option>
                    <option value="fahrenheit">Fahrenheit</option>
                    <option value="kelvin">Kelvin</option>
                </select><br>

                <label>A:</label>
                <select name="unidadDestino">
                    <option value="celsius">Celsius</option>
                    <option value="fahrenheit">Fahrenheit</option>
                    <option value="kelvin">Kelvin</option>
                </select><br>

                <button type="submit">Calcular</button>
            </form>

            <% if ("temperatura".equals(tipo)) { %>
                <div class="<%= error == null ? "result" : "error" %>">
                    <% if (error == null) { %>
                        <%= valor %> <%= origen %> = <%= resultado %> <%= destino %>
                          <% } else { %>
                        <%= error %>
                    <% } %>
                </div>
            <% } %>
