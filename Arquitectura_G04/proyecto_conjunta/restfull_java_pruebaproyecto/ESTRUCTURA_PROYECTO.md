# Estructura Completa del Proyecto

```
restfull_java_pruebaproyecto/
│
├── README.md                              # Documentación completa del sistema
├── INICIO_RAPIDO.md                       # Guía rápida de inicio
├── .gitignore                             # Archivos a ignorar en Git
│
├── query1.sql                             # Script de creación de BDs
├── datos_prueba_banco.sql                 # Datos de prueba BanQuito
├── datos_prueba_comercializadora.sql      # Datos de prueba Comercializadora
│
├── desplegar.ps1                          # Script de compilación y despliegue
├── probar_api.ps1                         # Script de pruebas REST API
│
├── banquito-server/                       # ===== SERVIDOR BANCO BANQUITO =====
│   ├── pom.xml                            # Configuración Maven
│   │
│   └── src/
│       └── main/
│           ├── java/ec/edu/banquito/
│           │   │
│           │   ├── entity/                # === Entidades JPA ===
│           │   │   ├── Cliente.java
│           │   │   ├── Cuenta.java
│           │   │   ├── Movimiento.java
│           │   │   ├── Credito.java
│           │   │   └── CuotaCredito.java
│           │   │
│           │   ├── dto/                   # === Data Transfer Objects ===
│           │   │   ├── EvaluacionCreditoRequest.java
│           │   │   ├── EvaluacionCreditoResponse.java
│           │   │   └── CuotaCreditoDTO.java
│           │   │
│           │   ├── service/               # === Lógica de Negocio ===
│           │   │   └── CreditoService.java
│           │   │       - evaluarCredito()
│           │   │       - generarTablaAmortizacion()
│           │   │       - obtenerTablaAmortizacion()
│           │   │
│           │   └── rest/                  # === Endpoints REST ===
│           │       ├── RestApplication.java (@ApplicationPath("/api"))
│           │       ├── CreditoResource.java
│           │       │   - POST /credits/evaluate
│           │       │   - GET /credits/{id}/schedule
│           │       └── ClienteResource.java
│           │           - GET /clients/{cedula}
│           │
│           ├── resources/
│           │   └── META-INF/
│           │       └── persistence.xml    # Configuración JPA
│           │
│           └── webapp/
│               └── WEB-INF/
│                   ├── web.xml            # Configuración web
│                   ├── beans.xml          # Configuración CDI
│                   └── glassfish-resources.xml  # Pool JDBC
│
└── comercializadora-server/               # ===== SERVIDOR COMERCIALIZADORA =====
    ├── pom.xml                            # Configuración Maven
    │
    └── src/
        └── main/
            ├── java/ec/edu/comercializadora/
            │   │
            │   ├── entity/                # === Entidades JPA ===
            │   │   ├── ClienteComercializadora.java
            │   │   ├── Producto.java
            │   │   ├── Factura.java
            │   │   └── DetalleFactura.java
            │   │
            │   ├── dto/                   # === Data Transfer Objects ===
            │   │   ├── EvaluacionCreditoRequest.java
            │   │   ├── EvaluacionCreditoResponse.java
            │   │   ├── FacturaRequest.java
            │   │   └── DetalleFacturaRequest.java
            │   │
            │   ├── service/               # === Lógica de Negocio ===
            │   │   ├── FacturaService.java
            │   │   │   - crearFactura()
            │   │   │   - listarProductos()
            │   │   └── BancoRestClient.java
            │   │       - evaluarCredito() [Cliente REST]
            │   │
            │   └── rest/                  # === Endpoints REST ===
            │       ├── RestApplication.java (@ApplicationPath("/api"))
            │       ├── ProductoResource.java
            │       │   - GET /products
            │       └── FacturaResource.java
            │           - POST /invoices
            │           - GET /invoices/{id}
            │
            ├── resources/
            │   └── META-INF/
            │       └── persistence.xml    # Configuración JPA
            │
            └── webapp/
                └── WEB-INF/
                    ├── web.xml            # Configuración web
                    ├── beans.xml          # Configuración CDI
                    └── glassfish-resources.xml  # Pool JDBC
```

## Flujo de Datos

### Escenario 1: Pago en Efectivo (33% descuento)

```
Cliente → POST /api/invoices (formaPago=EFECTIVO)
         ↓
    Comercializadora Server
         ├─ Calcular subtotal
         ├─ Aplicar descuento 33%
         ├─ Guardar factura
         └─ Retornar factura
```

### Escenario 2: Pago a Crédito (integración con banco)

```
Cliente → POST /api/invoices (formaPago=CREDITO)
         ↓
    Comercializadora Server
         ├─ Calcular subtotal
         ├─ Llamar REST → BanQuito Server
         │                     ↓
         │              POST /api/credits/evaluate
         │                     ├─ Validar cliente
         │                     ├─ Validar cuenta
         │                     ├─ Calcular promedios
         │                     ├─ Calcular monto máximo
         │                     ├─ Crear crédito
         │                     ├─ Generar tabla amortización
         │                     └─ Retornar {aprobado, idCredito}
         │                     ↓
         ├─ Recibir respuesta ←┘
         ├─ Si aprobado: guardar factura con idCreditoBanco
         └─ Retornar factura
```

## Base de Datos

### BanQuitoDB
- CLIENTE (id_cliente, cedula, nombres, apellidos, fecha_nacimiento, estado_civil)
- CUENTA (id_cuenta, numero_cuenta, id_cliente, tipo_cuenta, saldo, fecha_apertura)
- MOVIMIENTO (id_movimiento, id_cuenta, fecha_movimiento, tipo_movimiento, monto)
- CREDITO (id_credito, id_cliente, id_cuenta, monto_credito, plazo_meses, tasa_anual, estado)
- CUOTA_CREDITO (id_cuota, id_credito, numero_cuota, valor_cuota, interes_pagado, capital_pagado, saldo_restante)

### ComercializadoraDB
- CLIENTE_COMERCIALIZADORA (id_cliente, cedula, nombres, apellidos, direccion, telefono, email)
- PRODUCTO (id_producto, codigo, nombre, descripcion, precio_venta)
- FACTURA (id_factura, numero_factura, id_cliente, fecha, forma_pago, subtotal, descuento, total, id_credito_banco)
- DETALLE_FACTURA (id_detalle, id_factura, id_producto, cantidad, precio_unitario, total_linea)

## Endpoints REST

### BanQuito Server (http://localhost:8080/banquito-server/api)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | /credits/evaluate | Evaluar y aprobar crédito |
| GET | /credits/{id}/schedule | Consultar tabla de amortización |
| GET | /clients/{cedula} | Consultar información de cliente |

### Comercializadora Server (http://localhost:8080/comercializadora-server/api)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | /products | Listar catálogo de productos |
| POST | /invoices | Crear factura (efectivo o crédito) |
| GET | /invoices/{id} | Consultar factura específica |

## Compilación y Despliegue

### Automático
```powershell
.\desplegar.ps1
```

### Manual
```powershell
# Compilar
cd banquito-server
mvn clean package

cd ..\comercializadora-server
mvn clean package

# Desplegar
C:\payara6\bin\asadmin deploy banquito-server\target\banquito-server.war
C:\payara6\bin\asadmin deploy comercializadora-server\target\comercializadora-server.war
```

## Tecnologías
- **Java 21 LTS**
- **Jakarta EE 10** (JAX-RS, JPA, CDI, EJB)
- **Payara 6**
- **MySQL 8**
- **Maven 3.8+**
