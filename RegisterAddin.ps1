<#
.SYNOPSIS
    Register / unregister the AIPTool (.NET 8) Inventor AddIn.

.DESCRIPTION
    .NET 8 no longer supports RegisterForComInterop. This script registers the
    AddIn the ".NET Core COM host (comhost)" way instead of regasm:

      HKLM\SOFTWARE\Classes\CLSID\{4D6D2EAC-11B9-4BB1-9244-86AB8D5F8014}\InprocServer32
          = <AddInDir>\AIPTool.comhost.dll     (ThreadingModel = Both)
      HKLM\SOFTWARE\Classes\InventorAddIn.StandardAddInServer\CLSID
          = {4D6D2EAC-11B9-4BB1-9244-86AB8D5F8014}
      HKLM\SOFTWARE\Autodesk\Inventor\RegistryVersion<Ver>\AddIns\InventorAddIn.StandardAddInServer
          LoadOnStartup / AddInCLSID / DisplayName / Description

    Notes:
      * Run as ADMINISTRATOR (writes to HKLM).
      * Build the project first; the output folder must contain
        AIPTool.dll, AIPTool.comhost.dll and AIPTool.runtimeconfig.json.
      * .NET 8 Desktop Runtime (Microsoft.WindowsDesktop.App 8.0.x) must be installed.
      * Pass the Inventor version with -InventorVersion, e.g. 2026 -> 30.0.

.PARAMETER AddInDir
    AddIn output folder. Defaults to the folder of this script.

.PARAMETER InventorVersion
    Inventor registry version. Default 30.0 (Inventor 2026).

.PARAMETER Unregister
    Remove the COM class, ProgID and Inventor AddIns keys.

.EXAMPLE
    powershell -ExecutionPolicy Bypass -File RegisterAddin.ps1 -InventorVersion 30.0

.EXAMPLE
    powershell -ExecutionPolicy Bypass -File RegisterAddin.ps1 -Unregister
#>
[CmdletBinding()]
Param(
    [string] $AddInDir,
    [string] $InventorVersion = '30.0',
    [switch] $Unregister
)

$ErrorActionPreference = 'Stop'

$ClsId       = '{4D6D2EAC-11B9-4BB1-9244-86AB8D5F8014}'
$ProgId      = 'InventorAddIn.StandardAddInServer'
$DisplayName = 'AIPTool'
$Description = 'A Inventor AddIn For Inventor 2016-2026 (.NET 8)'

if ([string]::IsNullOrWhiteSpace($AddInDir)) {
    $AddInDir = Split-Path -Parent $MyInvocation.MyCommand.Path
}
$AddInDir = (Resolve-Path $AddInDir).Path

$comhost = Join-Path $AddInDir 'AIPTool.comhost.dll'
$managed = Join-Path $AddInDir 'AIPTool.dll'
$regVer  = 'RegistryVersion' + $InventorVersion

# ---- administrator check ----
$id = [Security.Principal.WindowsIdentity]::GetCurrent()
$principal = New-Object Security.Principal.WindowsPrincipal($id)
if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Error 'Please run this script as ADMINISTRATOR (HKLM write access required).'
    return
}

function Remove-KeyIfExists([string] $Path) {
    if (Test-Path $Path) { Remove-Item $Path -Recurse -Force }
}

# ---- unregister ----
if ($Unregister) {
    Remove-KeyIfExists "HKLM:\SOFTWARE\Classes\CLSID\$ClsId"
    Remove-KeyIfExists "HKLM:\SOFTWARE\Classes\$ProgId"
    Remove-KeyIfExists "HKLM:\SOFTWARE\Autodesk\Inventor\$regVer\AddIns\$ProgId"
    Write-Host "Unregistered AIPTool ($ProgId)."
    return
}

# ---- verify build output ----
if (-not (Test-Path $comhost)) { Write-Error "Not found: $comhost . Build the project first." ; return }
if (-not (Test-Path $managed)) { Write-Error "Not found: $managed . Build the project first." ; return }

# ---- COM class ----
$clsidKey = "HKLM:\SOFTWARE\Classes\CLSID\$ClsId"
New-Item -Path $clsidKey -Force | Out-Null
New-Item -Path "$clsidKey\InprocServer32" -Force | Out-Null
Set-ItemProperty -Path "$clsidKey\InprocServer32" -Name '(default)' -Value $comhost -Type String
Set-ItemProperty -Path "$clsidKey\InprocServer32" -Name 'ThreadingModel' -Value 'Both' -Type String
Set-ItemProperty -Path $clsidKey -Name 'ProgID' -Value $ProgId -Type String
Set-ItemProperty -Path $clsidKey -Name 'Class' -Value 'StandardAddInServer' -Type String
Set-ItemProperty -Path $clsidKey -Name 'Assembly' -Value 'AIPTool' -Type String

# ---- ProgID ----
$progKey = "HKLM:\SOFTWARE\Classes\$ProgId"
New-Item -Path $progKey -Force | Out-Null
Set-ItemProperty -Path $progKey -Name '(default)' -Value $ProgId -Type String
New-Item -Path "$progKey\CLSID" -Force | Out-Null
Set-ItemProperty -Path "$progKey\CLSID" -Name '(default)' -Value $ClsId -Type String

# ---- Inventor AddIns key ----
$addinKey = "HKLM:\SOFTWARE\Autodesk\Inventor\$regVer\AddIns\$ProgId"
New-Item -Path $addinKey -Force | Out-Null
Set-ItemProperty -Path $addinKey -Name 'LoadOnStartup' -Value 1 -Type DWord
Set-ItemProperty -Path $addinKey -Name 'AddInCLSID'   -Value $ClsId -Type String
Set-ItemProperty -Path $addinKey -Name 'DisplayName'  -Value $DisplayName -Type String
Set-ItemProperty -Path $addinKey -Name 'Description'  -Value $Description -Type String

Write-Host "Registered:"
Write-Host "  COM host : $comhost"
Write-Host "  Inventor : HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\Inventor\$regVer\AddIns\$ProgId"
Write-Host "Restart Inventor to load AIPTool."
