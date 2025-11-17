# GUIA PASO A PASO - Sistema BanQuito + Comercializadora

## RESUMEN DE ARCHIVOS CREADOS

Su proyecto tiene la siguiente estructura completa:

```
restfull_java_pruebaproyecto/
├── banquito-server/              (Servidor del Banco)
├── comercializadora-server/      (Servidor Comercializadora)
├── query1.sql                    (Script creacion BDs)
├── datos_prueba_*.sql            (Datos de prueba)
├── cargar_datos.bat              (Script carga de datos)
├── compilar.bat                  (Script compilacion)
├── README.md                     (Documentacion completa)
└── GUIA_INSTALACION.md           (Esta guia)
```

---

## PASO 1: VERIFICAR PREREQUISITOS

### A) Verificar Java 21

```powershell
java -version
```

**Debe mostrar:** `java version "21.0.7"`

Si no esta instalado, descargue desde: https://www.oracle.com/java/technologies/downloads/#java21

### B) Verificar MySQL

Ejecute el script batch:

```powershell
.\cargar_datos.bat
```

Si encuentra MySQL, saltará al PASO 2. Si no:

**Opciones:**
1. Instalar MySQL: https://dev.mysql.com/downloads/mysql/
2. Usar XAMPP (incluye MySQL): https://www.apachefriends.org/
3. Usar MySQL Workbench para cargar scripts manualmente

### C) Instalar Maven

Descargue desde: https://maven.apache.org/download.cgi

1. Descomprima en `C:\apache-maven`
2. Agregue al PATH:
   - Variables de entorno → Path → Nuevo
   - Agregue: `C:\apache-maven\bin`
3. Verifique:
   ```powershell
   mvn -version
   ```

### D) Instalar Payara 6

Descargue desde: https://www.payara.fish/downloads/payara-platform-community-edition/

1. Descomprima en `C:\payara6`
2. Verifique:
   ```powershell
   C:\payara6\bin\asadmin version
   ```

---

## PASO 2: CREAR Y CARGAR BASES DE DATOS

### Opcion A: Script Automatico (Recomendado)

```powershell
.\cargar_datos.bat
```

### Opcion B: MySQL Workbench

1. Abra MySQL Workbench
2. Conecte a localhost:3306 (usuario: root, password: comand)
3. Abra y ejecute en orden:
   - `query1.sql`
   - `datos_prueba_banco.sql`
   - `datos_prueba_comercializadora.sql`

### Opcion C: Linea de Comandos MySQL

Reemplace `RUTA_MYSQL` con su ruta real:

```powershell
# Ejemplo con XAMPP
& "C:\xampp\mysql\bin\mysql.exe" -u root -pcomand < query1.sql
& "C:\xampp\mysql\bin\mysql.exe" -u root -pcomand < datos_prueba_banco.sql
& "C:\xampp\mysql\bin\mysql.exe" -u root -pcomand < datos_prueba_comercializadora.sql

# Ejemplo con MySQL Server 8.0
& "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" -u root -pcomand < query1.sql
& "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" -u root -pcomand < datos_prueba_banco.sql
& "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" -u root -pcomand < datos_prueba_comercializadora.sql
```

---

## PASO 3: COMPILAR LOS PROYECTOS

### Opcion A: Script Automatico

```powershell
.\compilar.bat
```

### Opcion B: Maven Manual

```powershell
# Compilar BanQuito Server
cd banquito-server
mvn clean package
cd ..

# Compilar Comercializadora Server
cd comercializadora-server
mvn clean package
cd ..
```

**Resultado esperado:**
- `banquito-server\target\banquito-server.war`
- `comercializadora-server\target\comercializadora-server.war`

---

## PASO 4: DESPLEGAR EN PAYARA

### A) Iniciar Payara

```powershell
C:\payara6\bin\asadmin start-domain
```

Espere 10-15 segundos para que inicie completamente.

### B) Desplegar Aplicaciones

```powershell
# Desplegar BanQuito
C:\payara6\bin\asadmin deploy --force=true banquito-server\target\banquito-server.war

# Desplegar Comercializadora
C:\payara6\bin\asadmin deploy --force=true comercializadora-server\target\comercializadora-server.war
```

### C) Verificar Despliegue

```powershell
C:\payara6\bin\asadmin list-applications
```

**Debe mostrar:**
```
banquito-server        <web>
comercializadora-server <web>
```

---

## PASO 5: PROBAR LAS APIs

### Prueba 1: Listar Productos

```powershell
Invoke-RestMethod -Uri "http://localhost:8080/comercializadora-server/api/products" -Method GET
```

### Prueba 2: Evaluar Credito

```powershell
$body = @{
    cedula = "1234567890"
    precioElectrodomestico = 5000.00
    plazoMeses = 12
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:8080/banquito-server/api/credits/evaluate" -Method POST -ContentType "application/json" -Body $body
```

### Prueba 3: Consultar Tabla de Amortizacion

```powershell
# Reemplace {idCredito} con el ID obtenido en la prueba anterior
Invoke-RestMethod -Uri "http://localhost:8080/banquito-server/api/credits/1/schedule" -Method GET
```

### Prueba 4: Crear Factura en Efectivo (33% descuento)

```powershell
$factura = @{
    cedula = "1234567890"
    formaPago = "EFECTIVO"
    detalles = @(
        @{ idProducto = 1; cantidad = 1 }
    )
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:8080/comercializadora-server/api/invoices" -Method POST -ContentType "application/json" -Body $factura
```

### Prueba 5: Crear Factura a Credito (integra con banco)

```powershell
$factura = @{
    cedula = "1234567890"
    formaPago = "CREDITO"
    plazoMeses = 12
    detalles = @(
        @{ idProducto = 3; cantidad = 1 }
    )
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:8080/comercializadora-server/api/invoices" -Method POST -ContentType "application/json" -Body $factura
```

---

## URLS DE LOS SERVICIOS

### BanQuito Server
- **Base:** http://localhost:8080/banquito-server/api
- **Evaluar credito:** `POST /credits/evaluate`
- **Tabla amortizacion:** `GET /credits/{id}/schedule`
- **Consultar cliente:** `GET /clients/{cedula}`

### Comercializadora Server
- **Base:** http://localhost:8080/comercializadora-server/api
- **Listar productos:** `GET /products`
- **Crear factura:** `POST /invoices`
- **Consultar factura:** `GET /invoices/{id}`

---

## COMANDOS UTILES

### Ver logs de Payara

```powershell
Get-Content C:\payara6\glassfish\domains\domain1\logs\server.log -Tail 50 -Wait
```

### Detener Payara

```powershell
C:\payara6\bin\asadmin stop-domain
```

### Redesplegar una aplicacion

```powershell
# Recompilar
cd banquito-server
mvn clean package
cd ..

# Redesplegar
C:\payara6\bin\asadmin undeploy banquito-server
C:\payara6\bin\asadmin deploy --force=true banquito-server\target\banquito-server.war
```

### Ver aplicaciones desplegadas

```powershell
C:\payara6\bin\asadmin list-applications
```

---

## SOLUCION DE PROBLEMAS

### "Cannot connect to database"

1. Verifique que MySQL este corriendo
2. Verifique credenciales en:
   - `banquito-server\src\main\webapp\WEB-INF\web.xml`
   - `comercializadora-server\src\main\webapp\WEB-INF\web.xml`

### "Port 8080 already in use"

Otro proceso usa el puerto 8080. Opciones:

1. Detener el proceso que usa 8080
2. Cambiar puerto de Payara:
   ```powershell
   C:\payara6\bin\asadmin set server.network-config.network-listeners.network-listener.http-listener-1.port=8081
   ```

### "Maven command not found"

Maven no esta en el PATH. Opciones:

1. Agregar al PATH (ver PASO 1C)
2. Usar ruta completa: `C:\apache-maven\bin\mvn`

### Error de compilacion "package does not exist"

1. Limpie cache de Maven:
   ```powershell
   mvn clean
   ```
2. Verifique version de Java:
   ```powershell
   java -version
   mvn -version
   ```

### Las APIs no responden (404)

1. Verifique que las aplicaciones esten desplegadas:
   ```powershell
   C:\payara6\bin\asadmin list-applications
   ```
2. Revise los logs de Payara
3. Verifique las URLs (incluyen `/api`)

---

## DATOS DE PRUEBA

### Clientes con movimientos (pueden solicitar credito):

- **1234567890** - Juan Carlos Perez (CASADO, 40 años)
- **0987654321** - Maria Fernanda Lopez (SOLTERA, 33 años)
- **1122334455** - Pedro Antonio Ramirez (CASADO, 47 años)

### Cliente sin movimientos suficientes:

- **5544332211** - Ana Sofia Torres

### Productos disponibles (algunos ejemplos):

- ID 1: Televisor Samsung 55" - $1200.00
- ID 3: Televisor Sony 50" - $850.00
- ID 7: Lavadora Whirlpool - $950.00

---

## ARQUITECTURA

```
Cliente HTTP
    |
    ├─→ Comercializadora Server (puerto 8080)
    │       ├─ GET /api/products
    │       └─ POST /api/invoices
    │           └─→ BanQuito Server (puerto 8080)
    │                   ├─ POST /api/credits/evaluate
    │                   └─ GET /api/credits/{id}/schedule
    │
    └─→ BanQuito Server (puerto 8080)
            ├─ Evalua reglas de credito
            ├─ Genera tabla de amortizacion
            └─ Retorna aprobacion/rechazo
```

---

Para mas detalles tecnicos, consulte el **README.md** completo.
