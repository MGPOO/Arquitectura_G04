-- ============================================
-- DATOS DE PRUEBA PARA COMERCIALIZADORADB
-- ============================================

USE ComercializadoraDB;

-- Insertar productos de electrodomésticos
INSERT INTO PRODUCTO (codigo, nombre, descripcion, precio_venta) VALUES
-- Televisores
('TV001', 'Televisor Samsung 55" QLED', 'Smart TV 4K UHD QLED 55 pulgadas', 1200.00),
('TV002', 'Televisor LG 65" OLED', 'Smart TV 4K OLED 65 pulgadas', 2500.00),
('TV003', 'Televisor Sony 50" LED', 'Smart TV Full HD 50 pulgadas', 850.00),

-- Refrigeradoras
('REF001', 'Refrigeradora LG Side by Side', 'Refrigeradora No Frost 500L Inverter', 1800.00),
('REF002', 'Refrigeradora Samsung French Door', 'Refrigeradora 450L Twin Cooling', 2200.00),
('REF003', 'Refrigeradora Whirlpool', 'Refrigeradora No Frost 300L', 1100.00),

-- Lavadoras
('LAV001', 'Lavadora Whirlpool 20kg', 'Lavadora Automática Carga Superior 20kg', 950.00),
('LAV002', 'Lavadora Samsung 18kg', 'Lavadora Carga Frontal 18kg Inverter', 1300.00),
('LAV003', 'Lavadora LG 15kg', 'Lavadora Automática 15kg TurboWash', 1150.00),

-- Microondas
('MIC001', 'Microondas Samsung 1.5 pies', 'Microondas Digital 1.5 pies', 280.00),
('MIC002', 'Microondas LG Grill', 'Microondas con Grill 1.8 pies', 350.00),

-- Cocinas
('COC001', 'Cocina Indurama 6 quemadores', 'Cocina a gas 6 quemadores con horno', 580.00),
('COC002', 'Cocina Mabe 4 quemadores', 'Cocina a gas 4 quemadores', 420.00),

-- Aires Acondicionados
('AIR001', 'Aire Acondicionado LG 12000 BTU', 'Split Inverter 12000 BTU', 750.00),
('AIR002', 'Aire Acondicionado Samsung 18000 BTU', 'Split Inverter 18000 BTU', 950.00),

-- Licuadoras y pequeños electrodomésticos
('LIC001', 'Licuadora Oster 3 velocidades', 'Licuadora 600W 3 velocidades', 85.00),
('BAT001', 'Batidora KitchenAid', 'Batidora de pie 5 litros', 320.00),

-- Aspiradoras
('ASP001', 'Aspiradora Electrolux vertical', 'Aspiradora vertical sin bolsa', 280.00),
('ASP002', 'Aspiradora Robot Xiaomi', 'Robot aspiradora inteligente', 450.00),

-- Planchas y cuidado de ropa
('PLA001', 'Plancha a vapor Philips', 'Plancha a vapor 2400W', 65.00);

-- Insertar algunos clientes
INSERT INTO CLIENTE_COMERCIALIZADORA (cedula, nombres, apellidos, direccion, telefono, email) VALUES
('1234567890', 'Juan Carlos', 'Pérez Gómez', 'Av. Principal 123', '0987654321', 'juan.perez@email.com'),
('0987654321', 'María Fernanda', 'López Martínez', 'Calle Secundaria 456', '0976543210', 'maria.lopez@email.com'),
('1122334455', 'Pedro Antonio', 'Ramírez Silva', 'Av. Central 789', '0965432109', 'pedro.ramirez@email.com');

-- ============================================
-- VERIFICACIÓN DE DATOS
-- ============================================

-- Ver todos los productos con precios
SELECT 
    codigo,
    nombre,
    precio_venta,
    CASE 
        WHEN precio_venta < 500 THEN 'Económico'
        WHEN precio_venta BETWEEN 500 AND 1500 THEN 'Medio'
        ELSE 'Premium'
    END AS categoria
FROM PRODUCTO
ORDER BY precio_venta;

-- Ver productos por rango de precio
SELECT 
    COUNT(*) AS cantidad,
    MIN(precio_venta) AS precio_minimo,
    MAX(precio_venta) AS precio_maximo,
    AVG(precio_venta) AS precio_promedio
FROM PRODUCTO;

-- Calcular descuento del 33% para pago en efectivo
SELECT 
    codigo,
    nombre,
    precio_venta AS precio_original,
    ROUND(precio_venta * 0.33, 2) AS descuento_33,
    ROUND(precio_venta * 0.67, 2) AS precio_con_descuento
FROM PRODUCTO
ORDER BY precio_venta DESC;
