-- ============================================
-- DATOS DE PRUEBA PARA BANQUITODB
-- ============================================

USE BanQuitoDB;

-- Insertar clientes de prueba
INSERT INTO CLIENTE (cedula, nombres, apellidos, fecha_nacimiento, estado_civil) VALUES
('1234567890', 'Juan Carlos', 'Pérez Gómez', '1985-03-15', 'CASADO'),
('0987654321', 'María Fernanda', 'López Martínez', '1992-07-22', 'SOLTERA'),
('1122334455', 'Pedro Antonio', 'Ramírez Silva', '1978-11-30', 'CASADO'),
('5544332211', 'Ana Sofía', 'Torres Vega', '1995-05-18', 'SOLTERA');

-- Insertar cuentas
INSERT INTO CUENTA (numero_cuenta, id_cliente, tipo_cuenta, saldo, fecha_apertura) VALUES
('0001234567', 1, 'AHORROS', 8500.00, '2020-01-15'),
('0009876543', 2, 'CORRIENTE', 3200.00, '2021-03-20'),
('0001122334', 3, 'AHORROS', 12000.00, '2019-06-10'),
('0005544332', 4, 'AHORROS', 2800.00, '2022-08-05');

-- Insertar movimientos de los últimos 3 meses para Cliente 1
INSERT INTO MOVIMIENTO (id_cuenta, fecha_movimiento, tipo_movimiento, monto, descripcion) VALUES
-- Cliente 1 - Mes 1 (hace 3 meses)
(1, DATE_SUB(NOW(), INTERVAL 90 DAY), 'DEPOSITO', 1500.00, 'Depósito salario mes 1'),
(1, DATE_SUB(NOW(), INTERVAL 88 DAY), 'RETIRO', 300.00, 'Retiro supermercado'),
(1, DATE_SUB(NOW(), INTERVAL 85 DAY), 'DEPOSITO', 200.00, 'Depósito transferencia'),
(1, DATE_SUB(NOW(), INTERVAL 82 DAY), 'RETIRO', 150.00, 'Retiro farmacia'),
-- Cliente 1 - Mes 2 (hace 2 meses)
(1, DATE_SUB(NOW(), INTERVAL 60 DAY), 'DEPOSITO', 1600.00, 'Depósito salario mes 2'),
(1, DATE_SUB(NOW(), INTERVAL 58 DAY), 'RETIRO', 250.00, 'Retiro servicios'),
(1, DATE_SUB(NOW(), INTERVAL 55 DAY), 'DEPOSITO', 300.00, 'Depósito bono'),
(1, DATE_SUB(NOW(), INTERVAL 52 DAY), 'RETIRO', 180.00, 'Retiro gasolina'),
-- Cliente 1 - Mes 3 (hace 1 mes)
(1, DATE_SUB(NOW(), INTERVAL 30 DAY), 'DEPOSITO', 1700.00, 'Depósito salario mes 3'),
(1, DATE_SUB(NOW(), INTERVAL 28 DAY), 'RETIRO', 280.00, 'Retiro compras'),
(1, DATE_SUB(NOW(), INTERVAL 25 DAY), 'DEPOSITO', 150.00, 'Depósito extra'),
(1, DATE_SUB(NOW(), INTERVAL 22 DAY), 'RETIRO', 200.00, 'Retiro restaurante');

-- Insertar movimientos para Cliente 2
INSERT INTO MOVIMIENTO (id_cuenta, fecha_movimiento, tipo_movimiento, monto, descripcion) VALUES
-- Cliente 2 - Últimos 3 meses
(2, DATE_SUB(NOW(), INTERVAL 85 DAY), 'DEPOSITO', 1200.00, 'Depósito salario'),
(2, DATE_SUB(NOW(), INTERVAL 80 DAY), 'RETIRO', 400.00, 'Retiro varios'),
(2, DATE_SUB(NOW(), INTERVAL 55 DAY), 'DEPOSITO', 1300.00, 'Depósito salario'),
(2, DATE_SUB(NOW(), INTERVAL 50 DAY), 'RETIRO', 350.00, 'Retiro compras'),
(2, DATE_SUB(NOW(), INTERVAL 25 DAY), 'DEPOSITO', 1250.00, 'Depósito salario'),
(2, DATE_SUB(NOW(), INTERVAL 20 DAY), 'RETIRO', 300.00, 'Retiro servicios');

-- Insertar movimientos para Cliente 3
INSERT INTO MOVIMIENTO (id_cuenta, fecha_movimiento, tipo_movimiento, monto, descripcion) VALUES
-- Cliente 3 - Últimos 3 meses
(3, DATE_SUB(NOW(), INTERVAL 90 DAY), 'DEPOSITO', 2500.00, 'Depósito salario'),
(3, DATE_SUB(NOW(), INTERVAL 85 DAY), 'RETIRO', 500.00, 'Retiro varios'),
(3, DATE_SUB(NOW(), INTERVAL 60 DAY), 'DEPOSITO', 2600.00, 'Depósito salario'),
(3, DATE_SUB(NOW(), INTERVAL 55 DAY), 'RETIRO', 450.00, 'Retiro compras'),
(3, DATE_SUB(NOW(), INTERVAL 30 DAY), 'DEPOSITO', 2550.00, 'Depósito salario'),
(3, DATE_SUB(NOW(), INTERVAL 25 DAY), 'RETIRO', 480.00, 'Retiro servicios');

-- Cliente 4 tiene pocos movimientos (no calificaría para crédito grande)
INSERT INTO MOVIMIENTO (id_cuenta, fecha_movimiento, tipo_movimiento, monto, descripcion) VALUES
(4, DATE_SUB(NOW(), INTERVAL 60 DAY), 'DEPOSITO', 800.00, 'Depósito'),
(4, DATE_SUB(NOW(), INTERVAL 30 DAY), 'DEPOSITO', 900.00, 'Depósito');

-- ============================================
-- VERIFICACIÓN DE DATOS
-- ============================================

-- Ver resumen de clientes y cuentas
SELECT 
    c.cedula,
    c.nombres,
    c.apellidos,
    c.estado_civil,
    cu.numero_cuenta,
    cu.saldo,
    TIMESTAMPDIFF(YEAR, c.fecha_nacimiento, CURDATE()) AS edad
FROM CLIENTE c
JOIN CUENTA cu ON c.id_cliente = cu.id_cliente;

-- Ver promedios de movimientos últimos 3 meses por cliente
SELECT 
    c.cedula,
    c.nombres,
    COUNT(m.id_movimiento) AS total_movimientos,
    AVG(CASE WHEN m.tipo_movimiento = 'DEPOSITO' THEN m.monto END) AS promedio_depositos,
    AVG(CASE WHEN m.tipo_movimiento = 'RETIRO' THEN m.monto END) AS promedio_retiros,
    (AVG(CASE WHEN m.tipo_movimiento = 'DEPOSITO' THEN m.monto END) - 
     AVG(CASE WHEN m.tipo_movimiento = 'RETIRO' THEN m.monto END)) AS diferencia
FROM CLIENTE c
JOIN CUENTA cu ON c.id_cliente = cu.id_cliente
JOIN MOVIMIENTO m ON cu.id_cuenta = m.id_cuenta
WHERE m.fecha_movimiento >= DATE_SUB(NOW(), INTERVAL 3 MONTH)
GROUP BY c.id_cliente, c.cedula, c.nombres;

-- Calcular monto máximo potencial para 12 meses
SELECT 
    c.cedula,
    c.nombres,
    ROUND((AVG(CASE WHEN m.tipo_movimiento = 'DEPOSITO' THEN m.monto END) - 
     AVG(CASE WHEN m.tipo_movimiento = 'RETIRO' THEN m.monto END)) * 0.50 * 12, 2) AS monto_maximo_12_meses
FROM CLIENTE c
JOIN CUENTA cu ON c.id_cliente = cu.id_cliente
JOIN MOVIMIENTO m ON cu.id_cuenta = m.id_cuenta
WHERE m.fecha_movimiento >= DATE_SUB(NOW(), INTERVAL 3 MONTH)
GROUP BY c.id_cliente, c.cedula, c.nombres;
