# Sistema Banco BanQuito + Comercializadora de Electrodomésticos

Sistema completo con arquitectura .NET + SOAP que implementa un banco con módulo de crédito y una comercializadora de electrodomésticos.

## 📋 Requisitos Previos

- .NET 6.0 SDK o superior
- SQL Server (LocalDB o SQL Server completo)
- PowerShell (para ejecutar los comandos)

## 🗄️ Estructura del Proyecto

```
soap_dotnet_pruebaproyecto/
├── BancoSoapService/          # Servidor SOAP del Banco BanQuito
│   ├── Models/                # Modelos de datos (Cliente, Cuenta, Movimiento, Credito, CuotaCredito)
│   ├── Data/                  # DbContext de Entity Framework
│   ├── Services/              # Servicios SOAP (IBancoService, BancoService)
│   └── Services/Contracts/    # Contratos de datos SOAP
│
├── ComercializadoraAPI/       # API REST de la Comercializadora
│   ├── Models/                # Modelos de datos (Producto, Factura, DetalleFactura)
│   ├── Data/                  # DbContext de Entity Framework
│   ├── Controllers/           # Controladores REST (Productos, Facturas, Clientes)
│   ├── DTOs/                  # Data Transfer Objects
│   └── SoapClient/            # Cliente SOAP para comunicarse con el banco
│
└── SQLQuery1.sql              # Script de creación de bases de datos
```

## 🚀 Instalación y Configuración

### 1. Crear las Bases de Datos

Ejecuta el script SQL para crear las bases de datos `BanQuitoDB` y `ComercializadoraDB`:

```powershell
# Usando SQL Server Management Studio o ejecutar desde PowerShell:
sqlcmd -S localhost -i "SQLQuery1.sql"
```

O abre SQL Server Management Studio y ejecuta el contenido de `SQLQuery1.sql`.

### 2. Configurar Cadenas de Conexión

Ambos proyectos ya tienen configuradas las cadenas de conexión en sus archivos `appsettings.json`. Si tu servidor SQL Server usa una instancia diferente, actualiza las cadenas:

**BancoSoapService/appsettings.json:**
```json
"ConnectionStrings": {
  "BancoConnection": "Server=localhost;Database=BanQuitoDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**ComercializadoraAPI/appsettings.json:**
```json
"ConnectionStrings": {
  "ComercializadoraConnection": "Server=localhost;Database=ComercializadoraDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Restaurar Paquetes NuGet

```powershell
cd c:\src\proyecto_conjunta\soap_dotnet_pruebaproyecto

# Restaurar BancoSoapService
cd BancoSoapService
dotnet restore

# Restaurar ComercializadoraAPI
cd ..\ComercializadoraAPI
dotnet restore

cd ..
```

### 4. Aplicar Migraciones de Entity Framework (Opcional)

Si deseas usar migraciones en lugar del script SQL directo:

```powershell
# Para BancoSoapService
cd BancoSoapService
dotnet ef migrations add InitialCreate
dotnet ef database update

# Para ComercializadoraAPI
cd ..\ComercializadoraAPI
dotnet ef migrations add InitialCreate
dotnet ef database update

cd ..
```

## ▶️ Iniciar los Servidores

### Opción 1: Iniciar en Terminales Separadas

**Terminal 1 - Servidor del Banco (SOAP):**
```powershell
cd c:\src\proyecto_conjunta\soap_dotnet_pruebaproyecto\BancoSoapService
dotnet run
```

El servicio SOAP estará disponible en: `http://localhost:5000/BancoService.asmx`
Para ver el WSDL: `http://localhost:5000/BancoService.asmx?wsdl`

**Terminal 2 - API de la Comercializadora (REST):**
```powershell
cd c:\src\proyecto_conjunta\soap_dotnet_pruebaproyecto\ComercializadoraAPI
dotnet run
```

La API REST estará disponible en: `http://localhost:5001`
Swagger UI: `http://localhost:5001/swagger`

### Opción 2: Iniciar Ambos en Background

```powershell
cd c:\src\proyecto_conjunta\soap_dotnet_pruebaproyecto

# Iniciar Banco en background
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd BancoSoapService; dotnet run"

# Esperar 5 segundos
Start-Sleep -Seconds 5

# Iniciar Comercializadora en background
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd ComercializadoraAPI; dotnet run"
```

## 🔧 Configuración de Datos de Prueba

### Insertar datos de prueba en el Banco

```sql
USE BanQuitoDB;
GO

-- Insertar cliente
INSERT INTO CLIENTE (cedula, nombres, apellidos, fecha_nacimiento, estado_civil)
VALUES ('1234567890', 'Juan', 'Pérez', '1990-05-15', 'SOLTERO');

-- Insertar cuenta
DECLARE @IdCliente INT = (SELECT id_cliente FROM CLIENTE WHERE cedula = '1234567890');
INSERT INTO CUENTA (numero_cuenta, id_cliente, tipo_cuenta, saldo, fecha_apertura)
VALUES ('0001234567', @IdCliente, 'AHORROS', 5000.00, GETDATE());

-- Insertar movimientos (depósitos últimos 3 meses)
DECLARE @IdCuenta INT = (SELECT id_cuenta FROM CUENTA WHERE numero_cuenta = '0001234567');

INSERT INTO MOVIMIENTO (id_cuenta, fecha_movimiento, tipo_movimiento, monto, descripcion)
VALUES 
    (@IdCuenta, DATEADD(DAY, -80, GETDATE()), 'DEPOSITO', 1000.00, 'Depósito inicial'),
    (@IdCuenta, DATEADD(DAY, -75, GETDATE()), 'DEPOSITO', 1500.00, 'Depósito mensual'),
    (@IdCuenta, DATEADD(DAY, -50, GETDATE()), 'RETIRO', 500.00, 'Retiro'),
    (@IdCuenta, DATEADD(DAY, -45, GETDATE()), 'DEPOSITO', 2000.00, 'Depósito mensual'),
    (@IdCuenta, DATEADD(DAY, -20, GETDATE()), 'DEPOSITO', 1800.00, 'Depósito mensual'),
    (@IdCuenta, DATEADD(DAY, -15, GETDATE()), 'RETIRO', 300.00, 'Retiro'),
    (@IdCuenta, DATEADD(DAY, -5, GETDATE()), 'DEPOSITO', 2500.00, 'Depósito reciente');
```

### Insertar datos de prueba en la Comercializadora

```sql
USE ComercializadoraDB;
GO

-- Insertar cliente
INSERT INTO CLIENTE_COMERCIALIZADORA (cedula, nombres, apellidos, direccion, telefono, email)
VALUES ('1234567890', 'Juan', 'Pérez', 'Av. Principal 123', '0999999999', 'juan.perez@email.com');

-- Insertar productos
INSERT INTO PRODUCTO (codigo, nombre, descripcion, precio_venta)
VALUES 
    ('REF001', 'Refrigeradora LG 500L', 'Refrigeradora moderna con dispensador', 1200.00),
    ('LAV001', 'Lavadora Samsung 18Kg', 'Lavadora de carga frontal', 800.00),
    ('TV001', 'Smart TV 55 pulgadas', 'Smart TV 4K Ultra HD', 950.00),
    ('MIC001', 'Microondas Panasonic', 'Microondas 1.2 cu ft', 250.00);
```

## 📡 API Endpoints

### Servicio SOAP del Banco (Puerto 5000)

**Endpoint:** `http://localhost:5000/BancoService.asmx`

**Operaciones:**
- `EvaluateCredit` - Evaluar y aprobar crédito
- `GetAmortizationSchedule` - Obtener tabla de amortización
- `GetClientInfo` - Obtener información del cliente

### API REST de la Comercializadora (Puerto 5001)

**Base URL:** `http://localhost:5001/api`

**Endpoints de Clientes:**
- `GET /api/clientes` - Listar todos los clientes
- `GET /api/clientes/{cedula}` - Obtener cliente por cédula
- `POST /api/clientes` - Crear nuevo cliente

**Endpoints de Productos:**
- `GET /api/productos` - Listar todos los productos
- `GET /api/productos/{id}` - Obtener producto por ID
- `POST /api/productos` - Crear nuevo producto

**Endpoints de Facturas:**
- `GET /api/facturas` - Listar todas las facturas
- `GET /api/facturas/{id}` - Obtener factura por ID
- `POST /api/facturas` - Crear nueva factura (integración con SOAP)

## 🧪 Ejemplos de Uso

### 1. Crear Factura con Pago en EFECTIVO (Descuento 33%)

```powershell
$body = @{
    cedula = "1234567890"
    formaPago = "EFECTIVO"
    plazoMeses = 0
    detalles = @(
        @{
            idProducto = 1
            cantidad = 1
        }
    )
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/facturas" -Method POST -Body $body -ContentType "application/json"
```

**Respuesta esperada:**
```json
{
  "idFactura": 1,
  "numeroFactura": "FACT-20251116-1234",
  "fecha": "2025-11-16T10:30:00",
  "formaPago": "EFECTIVO",
  "subtotal": 1200.00,
  "descuento": 396.00,
  "total": 804.00,
  "idCreditoBanco": null,
  "mensaje": "Factura procesada con descuento del 33%",
  "aprobado": true
}
```

### 2. Crear Factura con Pago a CRÉDITO

```powershell
$body = @{
    cedula = "1234567890"
    formaPago = "CREDITO"
    plazoMeses = 12
    detalles = @(
        @{
            idProducto = 2
            cantidad = 1
        }
    )
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/facturas" -Method POST -Body $body -ContentType "application/json"
```

**Respuesta esperada (si es aprobado):**
```json
{
  "idFactura": 2,
  "numeroFactura": "FACT-20251116-5678",
  "fecha": "2025-11-16T10:35:00",
  "formaPago": "CREDITO",
  "subtotal": 800.00,
  "descuento": 0.00,
  "total": 800.00,
  "idCreditoBanco": 1,
  "mensaje": "Crédito aprobado. ID Crédito: 1",
  "aprobado": true
}
```

### 3. Consultar Tabla de Amortización

Puedes consultar la tabla de amortización directamente en la base de datos:

```sql
USE BanQuitoDB;
GO

SELECT 
    numero_cuota,
    fecha_vencimiento,
    valor_cuota,
    interes_pagado,
    capital_pagado,
    saldo_restante
FROM CUOTA_CREDITO
WHERE id_credito = 1
ORDER BY numero_cuota;
```

## 🔍 Validaciones del Sistema de Crédito

El sistema valida las siguientes condiciones para aprobar un crédito:

1. ✅ Cliente debe existir en el sistema
2. ✅ Cliente debe ser mayor de 21 años
3. ✅ Cliente NO debe estar casado
4. ✅ Cliente debe tener al menos una cuenta
5. ✅ Cliente NO debe tener créditos activos
6. ✅ Cliente debe tener depósitos en el último mes
7. ✅ Monto del electrodoméstico debe ser ≤ al monto máximo calculado
8. ✅ Plazo debe estar entre 1 y 36 meses

**Fórmula de Monto Máximo:**
```
MontoMáximo = (PromedioDepósitos3Meses - PromedioRetiros3Meses) × 10
```

## 🛑 Detener los Servidores

Para detener los servidores, presiona `Ctrl+C` en cada terminal donde se están ejecutando.

## 📝 Notas Adicionales

- **Tasa de Interés:** 16% anual (configurada por defecto)
- **Sistema de Amortización:** Cuota fija (Amortización Francesa)
- **Descuento Efectivo:** 33% sobre el subtotal
- **Puertos:**
  - Banco SOAP: 5000
  - Comercializadora API: 5001

## 🐛 Troubleshooting

**Error de conexión a la base de datos:**
- Verifica que SQL Server esté corriendo
- Confirma que las bases de datos existan
- Revisa las cadenas de conexión en `appsettings.json`

**Error "Port already in use":**
- Cambia los puertos en los archivos `Properties/launchSettings.json`

**Error al llamar al servicio SOAP:**
- Asegúrate de que el servidor del Banco esté corriendo primero
- Verifica la URL del servicio en `ComercializadoraAPI/appsettings.json`

## 📚 Tecnologías Utilizadas

- .NET 6.0
- ASP.NET Core Web API
- SoapCore (para servicios SOAP)
- Entity Framework Core 6.0
- SQL Server
- Swagger/OpenAPI
- System.ServiceModel (WCF Client)

---

**Desarrollado para el proyecto conjunto Banco BanQuito + Comercializadora**
