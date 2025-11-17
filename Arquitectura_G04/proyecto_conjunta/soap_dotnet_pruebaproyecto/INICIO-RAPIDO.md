# Guía Rápida de Inicio

## Pasos para iniciar el sistema:

### 1. Crear las bases de datos
Ejecuta el script SQL en SQL Server Management Studio o con sqlcmd:
```powershell
sqlcmd -S localhost -i "SQLQuery1.sql"
```

### 2. Restaurar paquetes NuGet
```powershell
.\restaurar-paquetes.ps1
```

### 3. (Opcional) Insertar datos de prueba
```powershell
.\insertar-datos-prueba.ps1
```

### 4. Iniciar los servidores
```powershell
.\iniciar-servidores.ps1
```

## URLs de Acceso:
- **Servicio SOAP del Banco:** http://localhost:5000/BancoService.asmx
- **API REST Comercializadora:** http://localhost:5001
- **Swagger UI:** http://localhost:5001/swagger

## Ejemplo de prueba con PowerShell:

### Crear factura con pago en efectivo (descuento 33%):
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

### Crear factura con pago a crédito:
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

### Listar productos:
```powershell
Invoke-RestMethod -Uri "http://localhost:5001/api/productos" -Method GET
```

Para más detalles, consulta el archivo README.md completo.
