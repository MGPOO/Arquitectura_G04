# Scripts de inicio rápido

# Script para iniciar ambos servidores

Write-Host "=== Iniciando Sistema Banco BanQuito + Comercializadora ===" -ForegroundColor Green
Write-Host ""

# Verificar que estamos en el directorio correcto
if (-not (Test-Path "BancoSoapService") -or -not (Test-Path "ComercializadoraAPI")) {
    Write-Host "Error: Debe ejecutar este script desde el directorio raíz del proyecto" -ForegroundColor Red
    exit 1
}

Write-Host "1. Iniciando Servidor del Banco (SOAP) en puerto 5000..." -ForegroundColor Cyan
$bancoProceso = Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PWD\BancoSoapService'; Write-Host 'Servidor del Banco BanQuito' -ForegroundColor Yellow; dotnet run" -PassThru

Write-Host "2. Esperando 8 segundos para que el banco inicie..." -ForegroundColor Yellow
Start-Sleep -Seconds 8

Write-Host "3. Iniciando API de la Comercializadora (REST) en puerto 5001..." -ForegroundColor Cyan
$comercializadoraProceso = Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PWD\ComercializadoraAPI'; Write-Host 'API de la Comercializadora' -ForegroundColor Yellow; dotnet run" -PassThru

Write-Host ""
Write-Host "=== Servidores Iniciados ===" -ForegroundColor Green
Write-Host ""
Write-Host "Servicio SOAP del Banco:" -ForegroundColor White
Write-Host "  URL: http://localhost:5000/BancoService.asmx" -ForegroundColor Gray
Write-Host "  WSDL: http://localhost:5000/BancoService.asmx?wsdl" -ForegroundColor Gray
Write-Host ""
Write-Host "API REST de la Comercializadora:" -ForegroundColor White
Write-Host "  URL: http://localhost:5001" -ForegroundColor Gray
Write-Host "  Swagger: http://localhost:5001/swagger" -ForegroundColor Gray
Write-Host ""
Write-Host "Presiona cualquier tecla para abrir Swagger en el navegador..." -ForegroundColor Yellow
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

Start-Process "http://localhost:5001/swagger"

Write-Host ""
Write-Host "Para detener los servidores, cierra las ventanas de PowerShell correspondientes." -ForegroundColor Yellow
Write-Host ""
