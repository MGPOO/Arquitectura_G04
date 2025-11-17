@echo off
REM ============================================
REM Script de Carga de Datos MySQL (Batch)
REM ============================================

echo ========================================
echo   Carga de Datos - Sistema BanQuito
echo ========================================
echo.

REM Buscar MySQL en ubicaciones comunes
set MYSQL_EXE=

if exist "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" (
    set MYSQL_EXE=C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe
    goto :found
)

if exist "C:\Program Files\MySQL\MySQL Server 8.4\bin\mysql.exe" (
    set MYSQL_EXE=C:\Program Files\MySQL\MySQL Server 8.4\bin\mysql.exe
    goto :found
)

if exist "C:\Program Files\MySQL\MySQL Server 9.0\bin\mysql.exe" (
    set MYSQL_EXE=C:\Program Files\MySQL\MySQL Server 9.0\bin\mysql.exe
    goto :found
)

if exist "C:\xampp\mysql\bin\mysql.exe" (
    set MYSQL_EXE=C:\xampp\mysql\bin\mysql.exe
    goto :found
)

echo [ERROR] MySQL no encontrado en ubicaciones comunes
echo.
echo Por favor, edita este archivo y especifica la ruta de MySQL manualmente
echo en la linea: set MYSQL_EXE=C:\ruta\a\mysql.exe
echo.
pause
exit /b 1

:found
echo [OK] MySQL encontrado: %MYSQL_EXE%
echo.

REM Cargar scripts
echo [1/3] Creando bases de datos...
"%MYSQL_EXE%" -u root -pcomand < query1.sql
if %ERRORLEVEL% EQU 0 (
    echo [OK] Bases de datos creadas
) else (
    echo [ERROR] Error al crear bases de datos
)
echo.

echo [2/3] Cargando datos de prueba del banco...
"%MYSQL_EXE%" -u root -pcomand < datos_prueba_banco.sql
if %ERRORLEVEL% EQU 0 (
    echo [OK] Datos del banco cargados
) else (
    echo [ERROR] Error al cargar datos del banco
)
echo.

echo [3/3] Cargando datos de prueba de la comercializadora...
"%MYSQL_EXE%" -u root -pcomand < datos_prueba_comercializadora.sql
if %ERRORLEVEL% EQU 0 (
    echo [OK] Datos de la comercializadora cargados
) else (
    echo [ERROR] Error al cargar datos de la comercializadora
)
echo.

echo ========================================
echo [OK] Carga de datos completada
echo ========================================
echo.

pause
