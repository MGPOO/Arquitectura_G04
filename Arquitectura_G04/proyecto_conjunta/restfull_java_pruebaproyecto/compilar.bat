@echo off
REM ============================================
REM Script de Compilacion (Batch)
REM ============================================

echo ========================================
echo   Compilacion - Sistema BanQuito
echo ========================================
echo.

REM Verificar Maven
where mvn >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Maven no esta instalado o no esta en el PATH
    pause
    exit /b 1
)
echo [OK] Maven encontrado
echo.

REM Compilar BanQuito Server
echo ========================================
echo   COMPILANDO BANQUITO SERVER
echo ========================================
cd banquito-server
call mvn clean package
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Error al compilar BanQuito Server
    cd ..
    pause
    exit /b 1
)
cd ..
echo [OK] BanQuito Server compilado
echo.

REM Compilar Comercializadora Server
echo ========================================
echo   COMPILANDO COMERCIALIZADORA SERVER
echo ========================================
cd comercializadora-server
call mvn clean package
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Error al compilar Comercializadora Server
    cd ..
    pause
    exit /b 1
)
cd ..
echo [OK] Comercializadora Server compilado
echo.

echo ========================================
echo [OK] Compilacion completada
echo ========================================
echo.
echo Archivos WAR generados:
echo   - banquito-server\target\banquito-server.war
echo   - comercializadora-server\target\comercializadora-server.war
echo.
pause
