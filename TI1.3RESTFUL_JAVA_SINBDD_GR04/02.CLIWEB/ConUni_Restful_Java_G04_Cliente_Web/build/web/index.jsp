<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%
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
            text-align: center;
            margin: 0;
        }
        h1 {
            background-color: #185a9d;
            color: white;
            padding: 20px;
        }
        h2 { color: #185a9d; }
        .container {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 40px;
            margin-top: 40px;
        }
        .card {
            background: white;
            padding: 25px;
            width: 300px;
            border-radius: 15px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
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
        }
        button:hover { background-color: #2bb673; }
        .result, .error {
            border-radius: 10px;
            padding: 10px;
            margin-top: 10px;
        }
        .result {
            background: #eaffea;
            border: 1px solid #43cea2;
            color: #185a9d;
        }
        .error {
            background: #ffecec;
            border: 1px solid #e74c3c;
            color: #c0392b;
        }
    </style>
</head>
<body>
    <h1>Conversor de Unidades (Servicio REST)</h1>

    <div class="container">
        <!-- CONVERSIÓN DE LONGITUD -->
        <div class="card">
            <h2>Longitud</h2>
            <form action="ConversorRestController" method="post">
                <input type="hidden" name="tipo" value="longitud">
                <input type="number" name="valor" step="0.01" placeholder="Valor" required>
                <label>De:</label>
                <select name="unidadOrigen">
                    <option value="metros">Metros</option>
                    <option value="kilometros">Kilómetros</option>
                    <option value="millas">Millas</option>
                </select>
                <label>A:</label>
                <select name="unidadDestino">
                    <option value="metros">Metros</option>
                    <option value="kilometros">Kilómetros</option>
                    <option value="millas">Millas</option>
                </select>
                <button type="submit">Convertir</button>
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
                <input type="number" name="valor" step="0.01" placeholder="Valor" required>
                <label>De:</label>
                <select name="unidadOrigen">
                    <option value="gramos">Gramos</option>
                    <option value="kilogramos">Kilogramos</option>
                    <option value="libras">Libras</option>
                </select>
                <label>A:</label>
                <select name="unidadDestino">
                    <option value="gramos">Gramos</option>
                    <option value="kilogramos">Kilogramos</option>
                    <option value="libras">Libras</option>
                </select>
                <button type="submit">Convertir</button>
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
                <input type="number" name="valor" step="0.01" placeholder="Valor" required>
                <label>De:</label>
                <select name="unidadOrigen">
                    <option value="celsius">Celsius</option>
                    <option value="fahrenheit">Fahrenheit</option>
                    <option value="kelvin">Kelvin</option>
                </select>
                <label>A:</label>
                <select name="unidadDestino">
                    <option value="celsius">Celsius</option>
                    <option value="fahrenheit">Fahrenheit</option>
                    <option value="kelvin">Kelvin</option>
                </select>
                <button type="submit">Convertir</button>
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
        </div>
    </div>
</body>
</html>
