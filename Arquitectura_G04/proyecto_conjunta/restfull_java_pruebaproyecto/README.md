# Sistema de Crédito BanQuito + Comercializadora de Electrodomésticos

Sistema completo con dos servidores RESTful en Java usando Jakarta EE 10 y Payara 6.

## Estructura del Proyecto

```
restfull_java_pruebaproyecto/
├── query1.sql                          # Script de creación de bases de datos
├── banquito-server/                    # Servidor del Banco BanQuito
│   ├── pom.xml
│   ├── src/
│   │   ├── main/
│   │   │   ├── java/
│   │   │   │   └── ec/edu/banquito/
│   │   │   │       ├── entity/         # Entidades JPA
│   │   │   │       ├── dto/            # Data Transfer Objects
│   │   │   │       ├── service/        # Lógica de negocio
│   │   │   │       └── rest/           # Endpoints REST
│   │   │   ├── resources/
│   │   │   │   └── META-INF/
│   │   │   │       └── persistence.xml
│   │   │   └── webapp/
│   │   │       └── WEB-INF/
│   │   │           ├── web.xml
│   │   │           └── glassfish-resources.xml
│   └── target/                         # WAR compilado
└── comercializadora-server/            # Servidor Comercializadora
    ├── pom.xml
    ├── src/
    │   ├── main/
    │   │   ├── java/
    │   │   │   └── ec/edu/comercializadora/
    │   │   │       ├── entity/         # Entidades JPA
    │   │   │       ├── dto/            # Data Transfer Objects
    │   │   │       ├── service/        # Lógica de negocio + Cliente REST
    │   │   │       └── rest/           # Endpoints REST
    │   │   ├── resources/
    │   │   │   └── META-INF/
    │   │   │       └── persistence.xml
    │   │   └── webapp/
    │   │       └── WEB-INF/
    │   │           ├── web.xml
    │   │           └── glassfish-resources.xml
    └── target/                         # WAR compilado
```

## Requisitos Previos

- **Java**: JDK 21.0.7 LTS
- **MySQL**: 8.0 o superior
- **Payara Server**: 6.x (Jakarta EE 10)
- **Maven**: 3.8 o superior

## Configuración de Base de Datos

### 1. Crear las bases de datos

Ejecutar el script `query1.sql` en MySQL:

```powershell
mysql -u root -pcomand < query1.sql
```

O desde el cliente MySQL:

```sql
source c:/src/proyecto_conjunta/restfull_java_pruebaproyecto/query1.sql
```

Esto creará:
- Base de datos `BanQuitoDB` con tablas: CLIENTE, CUENTA, MOVIMIENTO, CREDITO, CUOTA_CREDITO
- Base de datos `ComercializadoraDB` con tablas: CLIENTE_COMERCIALIZADORA, PRODUCTO, FACTURA, DETALLE_FACTURA

### 2. Verificar configuración MySQL

Las credenciales por defecto son:
- **Servidor**: localhost
- **Puerto**: 3306
- **Usuario**: root
- **Contraseña**: comand

Si necesitas cambiar las credenciales, editar:
- `banquito-server/src/main/webapp/WEB-INF/web.xml`
- `banquito-server/src/main/webapp/WEB-INF/glassfish-resources.xml`
- `comercializadora-server/src/main/webapp/WEB-INF/web.xml`
- `comercializadora-server/src/main/webapp/WEB-INF/glassfish-resources.xml`

## Compilación de los Proyectos

### Servidor BanQuito

```powershell
cd c:\src\proyecto_conjunta\restfull_java_pruebaproyecto\banquito-server
mvn clean package
```

Esto generará: `target/banquito-server.war`

### Servidor Comercializadora

```powershell
cd c:\src\proyecto_conjunta\restfull_java_pruebaproyecto\comercializadora-server
mvn clean package
```

Esto generará: `target/comercializadora-server.war`

## Despliegue en Payara 6

### Opción 1: Despliegue con asadmin (Línea de comandos)

Asumiendo que Payara está instalado en `C:\payara6`:

```powershell
# Iniciar Payara
C:\payara6\bin\asadmin start-domain

# Desplegar servidor del banco
C:\payara6\bin\asadmin deploy --force=true c:\src\proyecto_conjunta\restfull_java_pruebaproyecto\banquito-server\target\banquito-server.war

# Desplegar servidor de comercializadora
C:\payara6\bin\asadmin deploy --force=true c:\src\proyecto_conjunta\restfull_java_pruebaproyecto\comercializadora-server\target\comercializadora-server.war
```

### Opción 2: Despliegue copiando WAR a autodeploy

```powershell
# Copiar archivos WAR a la carpeta autodeploy
copy c:\src\proyecto_conjunta\restfull_java_pruebaproyecto\banquito-server\target\banquito-server.war C:\payara6\glassfish\domains\domain1\autodeploy\

copy c:\src\proyecto_conjunta\restfull_java_pruebaproyecto\comercializadora-server\target\comercializadora-server.war C:\payara6\glassfish\domains\domain1\autodeploy\
```

### Verificar despliegue

```powershell
C:\payara6\bin\asadmin list-applications
```

Deberías ver:
```
banquito-server        <web>
comercializadora-server <web>
```

## URLs de los Servicios

### Servidor BanQuito
- **Base URL**: http://localhost:8080/banquito-server/api
- **Evaluación de crédito**: `POST /api/credits/evaluate`
- **Tabla de amortización**: `GET /api/credits/{idCredito}/schedule`
- **Consulta cliente**: `GET /api/clients/{cedula}`

### Servidor Comercializadora
- **Base URL**: http://localhost:8080/comercializadora-server/api
- **Listar productos**: `GET /api/products`
- **Crear factura**: `POST /api/invoices`
- **Consultar factura**: `GET /api/invoices/{idFactura}`

## Ejemplos de Uso

### 1. Evaluación de Crédito (BanQuito)

```powershell
# Usar curl o Invoke-RestMethod
$body = @{
    cedula = "1234567890"
    precioElectrodomestico = 5000.00
    plazoMeses = 12
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:8080/banquito-server/api/credits/evaluate" `
    -Method POST `
    -ContentType "application/json" `
    -Body $body
```

**Respuesta exitosa:**
```json
{
    "sujetoCredito": true,
    "montoMaximo": 6000.00,
    "aprobado": true,
    "idCredito": 1,
    "motivo": "Crédito aprobado exitosamente"
}
```

**Respuesta rechazada:**
```json
{
    "sujetoCredito": false,
    "montoMaximo": 0.00,
    "aprobado": false,
    "idCredito": null,
    "motivo": "Cliente no registrado en el banco"
}
```

### 2. Consultar Tabla de Amortización

```powershell
Invoke-RestMethod -Uri "http://localhost:8080/banquito-server/api/credits/1/schedule" `
    -Method GET
```

**Respuesta:**
```json
[
    {
        "numeroCuota": 1,
        "fechaVencimiento": "2025-12-16",
        "valorCuota": 425.00,
        "interesPagado": 66.67,
        "capitalPagado": 358.33,
        "saldoRestante": 4641.67
    },
    ...
]
```

### 3. Listar Productos (Comercializadora)

```powershell
Invoke-RestMethod -Uri "http://localhost:8080/comercializadora-server/api/products" `
    -Method GET
```

### 4. Crear Factura con Efectivo (33% descuento)

```powershell
$factura = @{
    cedula = "1234567890"
    formaPago = "EFECTIVO"
    detalles = @(
        @{
            idProducto = 1
            cantidad = 2
        }
    )
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:8080/comercializadora-server/api/invoices" `
    -Method POST `
    -ContentType "application/json" `
    -Body $factura
```

**Respuesta:**
```json
{
    "idFactura": 1,
    "numeroFactura": "FACT-20251116143055",
    "fecha": "2025-11-16T14:30:55",
    "formaPago": "EFECTIVO",
    "subtotal": 2000.00,
    "descuento": 660.00,
    "total": 1340.00,
    "idCreditoBanco": null
}
```

### 5. Crear Factura a Crédito (con integración al banco)

```powershell
$factura = @{
    cedula = "1234567890"
    formaPago = "CREDITO"
    plazoMeses = 12
    detalles = @(
        @{
            idProducto = 1
            cantidad = 1
        }
    )
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:8080/comercializadora-server/api/invoices" `
    -Method POST `
    -ContentType "application/json" `
    -Body $factura
```

**Respuesta exitosa:**
```json
{
    "idFactura": 2,
    "numeroFactura": "FACT-20251116143500",
    "fecha": "2025-11-16T14:35:00",
    "formaPago": "CREDITO",
    "subtotal": 5000.00,
    "descuento": 0.00,
    "total": 5000.00,
    "idCreditoBanco": 1
}
```

## Datos de Prueba

### Insertar cliente en BanQuito

```sql
USE BanQuitoDB;

INSERT INTO CLIENTE (cedula, nombres, apellidos, fecha_nacimiento, estado_civil) 
VALUES ('1234567890', 'Juan', 'Pérez', '1990-05-15', 'CASADO');

INSERT INTO CUENTA (numero_cuenta, id_cliente, tipo_cuenta, saldo, fecha_apertura) 
VALUES ('0001234567', 1, 'AHORROS', 5000.00, '2023-01-10');

INSERT INTO MOVIMIENTO (id_cuenta, fecha_movimiento, tipo_movimiento, monto, descripcion)
VALUES 
(1, NOW() - INTERVAL 60 DAY, 'DEPOSITO', 1500.00, 'Depósito inicial'),
(1, NOW() - INTERVAL 50 DAY, 'DEPOSITO', 2000.00, 'Salario'),
(1, NOW() - INTERVAL 40 DAY, 'RETIRO', 500.00, 'Compra'),
(1, NOW() - INTERVAL 30 DAY, 'DEPOSITO', 1800.00, 'Salario'),
(1, NOW() - INTERVAL 20 DAY, 'RETIRO', 300.00, 'Compra'),
(1, NOW() - INTERVAL 10 DAY, 'DEPOSITO', 2200.00, 'Salario');
```

### Insertar productos en Comercializadora

```sql
USE ComercializadoraDB;

INSERT INTO PRODUCTO (codigo, nombre, descripcion, precio_venta) 
VALUES 
('TV001', 'Televisor Samsung 55"', 'Smart TV 4K UHD', 1200.00),
('REF001', 'Refrigeradora LG', 'Refrigeradora No Frost 500L', 1800.00),
('LAV001', 'Lavadora Whirlpool', 'Lavadora Automática 20kg', 950.00),
('MIC001', 'Microondas Samsung', 'Microondas 1.5 pies', 280.00);
```

## Lógica de Negocio Implementada

### Evaluación de Crédito (BanQuito)

El sistema evalúa los siguientes criterios:

1. **Cliente registrado**: Debe existir en la BD del banco
2. **Cuenta activa**: El cliente debe tener al menos una cuenta
3. **Edad mínima**: Mayor de 18 años
4. **Estado civil/edad**: Si es soltero, no mayor de 55 años
5. **Créditos activos**: No debe tener créditos activos previos
6. **Movimientos**: Debe tener depósitos en los últimos 3 meses

**Fórmula de monto máximo:**
```
MontoMáximo = (PromedioDepósitos - PromedioRetiros) × 0.50 × PlazoMeses
```

**Tabla de Amortización:**
- Sistema francés (cuota fija)
- Tasa de interés: 16% anual (1.33% mensual)
- Fórmula: `Cuota = M × [i(1+i)^n] / [(1+i)^n - 1]`

### Facturación (Comercializadora)

**Pago en Efectivo:**
- Aplica descuento automático del 33% sobre el subtotal
- Guarda factura directamente

**Pago a Crédito:**
1. Llama al endpoint `/api/credits/evaluate` del banco
2. Si el crédito es aprobado, guarda factura con `idCreditoBanco`
3. Si es rechazado, devuelve error con motivo

## Comandos de Gestión

### Detener Payara

```powershell
C:\payara6\bin\asadmin stop-domain
```

### Ver logs

```powershell
Get-Content C:\payara6\glassfish\domains\domain1\logs\server.log -Tail 50 -Wait
```

### Redesplegar aplicación

```powershell
# Recompilar
cd c:\src\proyecto_conjunta\restfull_java_pruebaproyecto\banquito-server
mvn clean package

# Redesplegar
C:\payara6\bin\asadmin undeploy banquito-server
C:\payara6\bin\asadmin deploy target\banquito-server.war
```

### Ver estado del servidor

```powershell
C:\payara6\bin\asadmin list-domains
```

## Solución de Problemas

### Error de conexión a MySQL

Verificar que MySQL está corriendo:
```powershell
Get-Service | Where-Object {$_.Name -like "*mysql*"}
```

Si no está corriendo:
```powershell
Start-Service MySQL80
```

### Puerto 8080 ocupado

Cambiar puerto de Payara:
```powershell
C:\payara6\bin\asadmin set server.network-config.network-listeners.network-listener.http-listener-1.port=8081
```

### Error de pool de conexiones

Recrear el pool JDBC:
```powershell
C:\payara6\bin\asadmin delete-jdbc-resource jdbc/BanQuitoDB
C:\payara6\bin\asadmin delete-jdbc-connection-pool BanQuitoPool
C:\payara6\bin\asadmin add-resources c:\src\proyecto_conjunta\restfull_java_pruebaproyecto\banquito-server\src\main\webapp\WEB-INF\glassfish-resources.xml
```

## Arquitectura del Sistema

```
┌─────────────────────┐
│  Cliente HTTP/REST  │
└──────────┬──────────┘
           │
           ├─────────────────────┐
           │                     │
           ▼                     ▼
┌──────────────────┐  ┌─────────────────────┐
│ Comercializadora │  │   BanQuito Server   │
│     Server       │  │                     │
│  (Port 8080)     │  │   (Port 8080)       │
├──────────────────┤  ├─────────────────────┤
│ REST Resources   │  │  REST Resources     │
│ - /api/products  │  │  - /api/credits/    │
│ - /api/invoices  │  │      evaluate       │
├──────────────────┤  │  - /api/credits/    │
│ Services         │──┤      {id}/schedule  │
│ - FacturaService │  │  - /api/clients/    │
│ - BancoClient ◄──┼──┤      {cedula}       │
├──────────────────┤  ├─────────────────────┤
│ JPA Entities     │  │  JPA Entities       │
│ - Factura        │  │  - Cliente          │
│ - Producto       │  │  - Cuenta           │
│ - Detalle        │  │  - Movimiento       │
│                  │  │  - Credito          │
└────────┬─────────┘  │  - CuotaCredito     │
         │            └──────────┬──────────┘
         │                       │
         ▼                       ▼
┌──────────────────┐  ┌─────────────────────┐
│ComercializadoraDB│  │    BanQuitoDB       │
│   (MySQL)        │  │     (MySQL)         │
└──────────────────┘  └─────────────────────┘
```

## Tecnologías Utilizadas

- **Java 21 LTS**: Lenguaje de programación
- **Jakarta EE 10**: Framework empresarial
  - JAX-RS (REST API)
  - JPA (Persistencia)
  - CDI (Inyección de dependencias)
  - EJB (Enterprise JavaBeans)
- **Payara 6**: Servidor de aplicaciones
- **MySQL 8**: Base de datos
- **Maven**: Gestor de dependencias y construcción
- **JSON-B**: Serialización JSON

## Autor

Proyecto de examen práctico conjunto - Sistema de Crédito y Comercialización

## Licencia

Uso académico
