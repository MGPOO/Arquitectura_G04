# Script para restaurar paquetes NuGet de ambos proyectos

Write-Host "=== Restaurando paquetes NuGet ===" -ForegroundColor Green
Write-Host ""

# Verificar que estamos en el directorio correcto
if (-not (Test-Path "BancoSoapService") -or -not (Test-Path "ComercializadoraAPI")) {
    Write-Host "Error: Debe ejecutar este script desde el directorio raíz del proyecto" -ForegroundColor Red
    exit 1
}

Write-Host "1. Restaurando paquetes de BancoSoapService..." -ForegroundColor Cyan
cd BancoSoapService
dotnet restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al restaurar paquetes de BancoSoapService" -ForegroundColor Red
    cd ..
    exit 1
}

Write-Host "   ✓ BancoSoapService restaurado correctamente" -ForegroundColor Green
cd ..

Write-Host ""
Write-Host "2. Restaurando paquetes de ComercializadoraAPI..." -ForegroundColor Cyan
cd ComercializadoraAPI
dotnet restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al restaurar paquetes de ComercializadoraAPI" -ForegroundColor Red
    cd ..
    exit 1
}

Write-Host "   ✓ ComercializadoraAPI restaurado correctamente" -ForegroundColor Green
cd ..

Write-Host ""
Write-Host "=== Restauración completada ===" -ForegroundColor Green
Write-Host ""
Write-Host "Ahora puede ejecutar: .\iniciar-servidores.ps1" -ForegroundColor Yellow
