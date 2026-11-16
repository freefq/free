param(
    [switch]$CompileOnly
)

$ErrorActionPreference = 'Stop'

if ([Environment]::OSVersion.Platform -ne [PlatformID]::Win32NT) {
    throw 'This desktop pet can only run on Windows.'
}

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
$references = @(
    [System.Drawing.Graphics].Assembly.Location
    [System.Windows.Forms.Form].Assembly.Location
)
Add-Type -Path (Join-Path $PSScriptRoot 'PetWindow.cs') -ReferencedAssemblies $references

if ($CompileOnly) {
    Write-Output 'Desktop pet code compiled successfully.'
    exit 0
}

[DesktopPet.PetWindow]::Run()
