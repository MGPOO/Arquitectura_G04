<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Inicio de Sesión</title>
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background: #f3f7f7;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .login-box {
            background: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
            text-align: center;
      
            width: 350px;
        }
        
        
        input[type="text"], input[type="password"] {
            width: 90%;
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 8px;
            
        }
        button {
            background-color: #2ecc71;
            color: white;
            border: none;
            padding: 10px 20px;
            font-size: 16px;
            border-radius: 8px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
            margin-top: 10px;
        }
        button:hover {
            background-color: #134b80;
        }
        .error {
            color: red;
            margin-top: 10px;
        }
        img {
            width: 120px;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <div class="login-box">
        <img src="images/ldu_quito.png" alt="Logo">
        <h2>Iniciar Sesión</h2>
        <form action="LoginServlet" method="post">
            <input type="text" name="usuario" placeholder="Usuario" required><br>
            <input type="password" name="clave" placeholder="Contraseña" required><br>
            <button type="submit">Ingresar</button>
        </form>
        <% 
            String error = request.getParameter("error");
            if (error != null) {
        %>
            <p class="error">Usuario o contraseña incorrectos</p>
        <% } %>
    </div>
</body>
</html>
