# Instrucciones Rápidas de Inicio

## 1. Crear las bases de datos

```powershell
mysql -u root -pcomand -e "source query1.sql"
```

## 2. Insertar datos de prueba

```powershell
mysql -u root -pcomand -e "source datos_prueba_banco.sql"
mysql -u root -pcomand -e "source datos_prueba_comercializadora.sql"
```

## 3. Compilar y desplegar

```powershell
.\desplegar.ps1
```

O si Payara está en otra ubicación:

```powershell
.\desplegar.ps1 -PayaraPath "D:\mi-payara"
```

## 4. Probar las APIs

```powershell
.\probar_api.ps1
```

## Compilación Manual

### BanQuito Server

```powershell
cd banquito-server
mvn clean package
```

### Comercializadora Server

```powershell
cd comercializadora-server
mvn clean package
```

## Despliegue Manual

```powershell
# Iniciar Payara
C:\payara6\bin\asadmin start-domain

# Desplegar aplicaciones
C:\payara6\bin\asadmin deploy banquito-server\target\banquito-server.war
C:\payara6\bin\asadmin deploy comercializadora-server\target\comercializadora-server.war
```

## URLs

- **BanQuito**: http://localhost:8080/banquito-server/api
- **Comercializadora**: http://localhost:8080/comercializadora-server/api

## Ejemplo de prueba manual con PowerShell

```powershell
# Evaluar crédito
$body = @{
    cedula = "1234567890"
    precioElectrodomestico = 5000.00
    plazoMeses = 12
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:8080/banquito-server/api/credits/evaluate" -Method POST -ContentType "application/json" -Body $body
```

Para más detalles, consulta el archivo **README.md**
