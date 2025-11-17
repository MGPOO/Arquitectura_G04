# Guia Rapida de Instalacion y Despliegue

## Paso 1: Verificar MySQL

Primero, necesitas encontrar donde esta instalado MySQL. Ejecuta este comando:

```powershell
Get-ChildItem "C:\Program Files" -Recurse -Filter "mysql.exe" -ErrorAction SilentlyContinue | Select-Object FullName
```

O busca manualmente en estas ubicaciones comunes:
- `C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe`
- `C:\Program Files\MySQL\MySQL Server 8.4\bin\mysql.exe`
- `C:\xampp\mysql\bin\mysql.exe`

## Paso 2: Cargar Datos en MySQL

### Opcion A: Con el script automatico

```powershell
.\cargar_datos.ps1
```

Si MySQL esta en otra ubicacion:

```powershell
.\cargar_datos.ps1 -MySqlPath "C:\ruta\completa\a\mysql.exe"
```

### Opcion B: Manualmente desde MySQL Workbench

1. Abre MySQL Workbench
2. Conectate a tu servidor (localhost:3306, usuario: root, password: comand)
3. Abre y ejecuta cada archivo en orden:
   - `query1.sql` (crea las bases de datos)
   - `datos_prueba_banco.sql`
   - `datos_prueba_comercializadora.sql`

### Opcion C: Manualmente desde linea de comandos

Reemplaza `RUTA_A_MYSQL` con la ruta real de tu instalacion:

```powershell
& "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" -u root -pcomand -e "source C:\src\proyecto_conjunta\restfull_java_pruebaproyecto\query1.sql"

& "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" -u root -pcomand -e "source C:\src\proyecto_conjunta\restfull_java_pruebaproyecto\datos_prueba_banco.sql"

& "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" -u root -pcomand -e "source C:\src\proyecto_conjunta\restfull_java_pruebaproyecto\datos_prueba_comercializadora.sql"
```

## Paso 3: Compilar y Desplegar en Payara

### Opcion A: Con el script automatico

```powershell
.\desplegar.ps1
```

Si Payara esta en otra ubicacion:

```powershell
.\desplegar.ps1 -PayaraPath "D:\mi-payara"
```

### Opcion B: Manualmente

```powershell
# 1. Compilar BanQuito
cd banquito-server
mvn clean package
cd ..

# 2. Compilar Comercializadora
cd comercializadora-server
mvn clean package
cd ..

# 3. Iniciar Payara (ajusta la ruta)
& "C:\payara6\bin\asadmin.bat" start-domain

# 4. Desplegar aplicaciones
& "C:\payara6\bin\asadmin.bat" deploy --force=true "banquito-server\target\banquito-server.war"
& "C:\payara6\bin\asadmin.bat" deploy --force=true "comercializadora-server\target\comercializadora-server.war"

# 5. Verificar despliegue
& "C:\payara6\bin\asadmin.bat" list-applications
```

## Paso 4: Probar las APIs

```powershell
.\probar_api.ps1
```

O prueba manualmente:

```powershell
# Listar productos
Invoke-RestMethod -Uri "http://localhost:8080/comercializadora-server/api/products" -Method GET

# Evaluar credito
$body = @{
    cedula = "1234567890"
    precioElectrodomestico = 5000.00
    plazoMeses = 12
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:8080/banquito-server/api/credits/evaluate" -Method POST -ContentType "application/json" -Body $body
```

## Solucionar Problemas Comunes

### Problema: "mysql no se reconoce"

**Solucion:** MySQL no esta en el PATH. Usa la ruta completa o el script `cargar_datos.ps1`.

### Problema: "Access denied for user 'root'@'localhost'"

**Solucion:** La contraseña no es "comand". Edita los archivos:
- `banquito-server\src\main\webapp\WEB-INF\web.xml`
- `comercializadora-server\src\main\webapp\WEB-INF\web.xml`

Y cambia `<password>comand</password>` por tu contraseña real.

### Problema: "Puerto 8080 ya esta en uso"

**Solucion:** Otro proceso esta usando el puerto. Detener el proceso o cambiar el puerto de Payara:

```powershell
& "C:\payara6\bin\asadmin.bat" set server.network-config.network-listeners.network-listener.http-listener-1.port=8081
```

### Problema: Errores de compilacion con Maven

**Solucion:** Verificar version de Java:

```powershell
java -version
```

Debe ser Java 21. Si no, instala Java 21 JDK y configura JAVA_HOME.

### Problema: "Cannot find module 'mysql-connector-j'"

**Solucion:** Copiar el driver MySQL a Payara:

1. Descarga mysql-connector-j-8.3.0.jar
2. Copialo a `C:\payara6\glassfish\domains\domain1\lib\`
3. Reinicia Payara

## URLs de los Servicios

Una vez desplegado, los servicios estaran disponibles en:

- **BanQuito:** http://localhost:8080/banquito-server/api
- **Comercializadora:** http://localhost:8080/comercializadora-server/api

## Ver Logs de Payara

```powershell
Get-Content "C:\payara6\glassfish\domains\domain1\logs\server.log" -Tail 50 -Wait
```

## Detener Payara

```powershell
& "C:\payara6\bin\asadmin.bat" stop-domain
```
