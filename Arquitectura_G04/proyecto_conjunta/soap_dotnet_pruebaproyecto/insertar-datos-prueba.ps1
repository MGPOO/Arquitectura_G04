# Script para insertar datos de prueba en las bases de datos

Write-Host "=== Insertando Datos de Prueba ===" -ForegroundColor Green
Write-Host ""

# SQL para datos de prueba del Banco
$sqlBanco = @"
USE BanQuitoDB;
GO

-- Verificar si ya existe el cliente
IF NOT EXISTS (SELECT 1 FROM CLIENTE WHERE cedula = '1234567890')
BEGIN
    -- Insertar cliente
    INSERT INTO CLIENTE (cedula, nombres, apellidos, fecha_nacimiento, estado_civil)
    VALUES ('1234567890', 'Juan', 'Pérez', '1990-05-15', 'SOLTERO');
    
    PRINT 'Cliente insertado';
END
ELSE
BEGIN
    PRINT 'Cliente ya existe';
END

-- Insertar cuenta si no existe
DECLARE @IdCliente INT = (SELECT id_cliente FROM CLIENTE WHERE cedula = '1234567890');

IF NOT EXISTS (SELECT 1 FROM CUENTA WHERE numero_cuenta = '0001234567')
BEGIN
    INSERT INTO CUENTA (numero_cuenta, id_cliente, tipo_cuenta, saldo, fecha_apertura)
    VALUES ('0001234567', @IdCliente, 'AHORROS', 5000.00, GETDATE());
    
    PRINT 'Cuenta insertada';
END
ELSE
BEGIN
    PRINT 'Cuenta ya existe';
END

-- Insertar movimientos (depósitos últimos 3 meses)
DECLARE @IdCuenta INT = (SELECT id_cuenta FROM CUENTA WHERE numero_cuenta = '0001234567');

IF NOT EXISTS (SELECT 1 FROM MOVIMIENTO WHERE id_cuenta = @IdCuenta)
BEGIN
    INSERT INTO MOVIMIENTO (id_cuenta, fecha_movimiento, tipo_movimiento, monto, descripcion)
    VALUES 
        (@IdCuenta, DATEADD(DAY, -80, GETDATE()), 'DEPOSITO', 1000.00, 'Depósito inicial'),
        (@IdCuenta, DATEADD(DAY, -75, GETDATE()), 'DEPOSITO', 1500.00, 'Depósito mensual'),
        (@IdCuenta, DATEADD(DAY, -50, GETDATE()), 'RETIRO', 500.00, 'Retiro'),
        (@IdCuenta, DATEADD(DAY, -45, GETDATE()), 'DEPOSITO', 2000.00, 'Depósito mensual'),
        (@IdCuenta, DATEADD(DAY, -20, GETDATE()), 'DEPOSITO', 1800.00, 'Depósito mensual'),
        (@IdCuenta, DATEADD(DAY, -15, GETDATE()), 'RETIRO', 300.00, 'Retiro'),
        (@IdCuenta, DATEADD(DAY, -5, GETDATE()), 'DEPOSITO', 2500.00, 'Depósito reciente');
    
    PRINT 'Movimientos insertados';
END
ELSE
BEGIN
    PRINT 'Movimientos ya existen';
END

-- Mostrar resumen
SELECT 'Cliente: ' + nombres + ' ' + apellidos + ' (Cédula: ' + cedula + ')' AS Info
FROM CLIENTE WHERE cedula = '1234567890';

SELECT 'Cuenta: ' + numero_cuenta + ' | Saldo: ' + CAST(saldo AS VARCHAR) AS Info
FROM CUENTA WHERE numero_cuenta = '0001234567';

SELECT 'Total Movimientos: ' + CAST(COUNT(*) AS VARCHAR) AS Info
FROM MOVIMIENTO WHERE id_cuenta = @IdCuenta;
"@

# SQL para datos de prueba de Comercializadora
$sqlComercializadora = @"
USE ComercializadoraDB;
GO

-- Insertar cliente si no existe
IF NOT EXISTS (SELECT 1 FROM CLIENTE_COMERCIALIZADORA WHERE cedula = '1234567890')
BEGIN
    INSERT INTO CLIENTE_COMERCIALIZADORA (cedula, nombres, apellidos, direccion, telefono, email)
    VALUES ('1234567890', 'Juan', 'Pérez', 'Av. Principal 123', '0999999999', 'juan.perez@email.com');
    
    PRINT 'Cliente comercializadora insertado';
END
ELSE
BEGIN
    PRINT 'Cliente comercializadora ya existe';
END

-- Insertar productos si no existen
IF NOT EXISTS (SELECT 1 FROM PRODUCTO WHERE codigo = 'REF001')
BEGIN
    INSERT INTO PRODUCTO (codigo, nombre, descripcion, precio_venta)
    VALUES 
        ('REF001', 'Refrigeradora LG 500L', 'Refrigeradora moderna con dispensador', 1200.00),
        ('LAV001', 'Lavadora Samsung 18Kg', 'Lavadora de carga frontal', 800.00),
        ('TV001', 'Smart TV 55 pulgadas', 'Smart TV 4K Ultra HD', 950.00),
        ('MIC001', 'Microondas Panasonic', 'Microondas 1.2 cu ft', 250.00),
        ('COC001', 'Cocina Indurama 6 hornillas', 'Cocina a gas con horno', 450.00);
    
    PRINT 'Productos insertados';
END
ELSE
BEGIN
    PRINT 'Productos ya existen';
END

-- Mostrar resumen
SELECT 'Cliente: ' + nombres + ' ' + apellidos + ' (Cédula: ' + cedula + ')' AS Info
FROM CLIENTE_COMERCIALIZADORA WHERE cedula = '1234567890';

SELECT 'Total Productos: ' + CAST(COUNT(*) AS VARCHAR) AS Info
FROM PRODUCTO;

SELECT 'Productos disponibles:' AS Info;
SELECT codigo, nombre, precio_venta FROM PRODUCTO;
"@

# Guardar scripts en archivos temporales
$sqlBanco | Out-File -FilePath "temp_datos_banco.sql" -Encoding UTF8
$sqlComercializadora | Out-File -FilePath "temp_datos_comercializadora.sql" -Encoding UTF8

Write-Host "1. Insertando datos en BanQuitoDB..." -ForegroundColor Cyan
sqlcmd -S localhost -i "temp_datos_banco.sql"

if ($LASTEXITCODE -ne 0) {
    Write-Host "   ⚠ Advertencia: Posible error al insertar datos en BanQuitoDB" -ForegroundColor Yellow
} else {
    Write-Host "   ✓ Datos insertados en BanQuitoDB" -ForegroundColor Green
}

Write-Host ""
Write-Host "2. Insertando datos en ComercializadoraDB..." -ForegroundColor Cyan
sqlcmd -S localhost -i "temp_datos_comercializadora.sql"

if ($LASTEXITCODE -ne 0) {
    Write-Host "   ⚠ Advertencia: Posible error al insertar datos en ComercializadoraDB" -ForegroundColor Yellow
} else {
    Write-Host "   ✓ Datos insertados en ComercializadoraDB" -ForegroundColor Green
}

# Limpiar archivos temporales
Remove-Item "temp_datos_banco.sql" -ErrorAction SilentlyContinue
Remove-Item "temp_datos_comercializadora.sql" -ErrorAction SilentlyContinue

Write-Host ""
Write-Host "=== Datos de Prueba Insertados ===" -ForegroundColor Green
Write-Host ""
Write-Host "Cliente de prueba:" -ForegroundColor White
Write-Host "  Cédula: 1234567890" -ForegroundColor Gray
Write-Host "  Nombre: Juan Pérez" -ForegroundColor Gray
Write-Host "  Cuenta: 0001234567" -ForegroundColor Gray
Write-Host ""
Write-Host "Productos disponibles: 5 electrodomésticos" -ForegroundColor White
Write-Host ""
