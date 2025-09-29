# ASP.NET Web Forms Development Server Launcher
# This script starts IIS Express for the HW4 project

Write-Host "Starting ASP.NET Web Forms Development Server..." -ForegroundColor Green
Write-Host "Project: HW4" -ForegroundColor Yellow
Write-Host "Port: 52061" -ForegroundColor Yellow
Write-Host ""

# Set the working directory to the project folder
$projectPath = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $projectPath

# Start IIS Express
Write-Host "Launching IIS Express..." -ForegroundColor Green
& "C:\Program Files\IIS Express\iisexpress.exe" /path:$projectPath /port:52061

# The server will keep running until you press Ctrl+C