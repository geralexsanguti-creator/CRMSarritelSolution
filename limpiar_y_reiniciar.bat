@echo off
echo Finalizando procesos de CRMSarritel...
taskkill /F /IM CRMSarritelApi.exe /T 2>nul
taskkill /F /IM dotnet.exe /T 2>nul
taskkill /F /IM MSBuild.exe /T 2>nul
taskkill /F /IM VBCSCompiler.exe /T 2>nul
echo.
echo Limpiando directorios bin y obj...
rd /s /q "CRMSarritelApi\bin" 2>nul
rd /s /q "CRMSarritelApi\obj" 2>nul
echo.
echo Proceso de limpieza completado. Por favor, vuelve a ejecutar 'dotnet run' desde la carpeta del proyecto.
pause
