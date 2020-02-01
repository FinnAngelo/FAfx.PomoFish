$RootDir = "[Environment]::GetFolderPath("Desktop")\GitHub\WixInstaller"
$ToolsDir = "[Environment]::GetFolderPath("Desktop")\GitHub\WixInstaller\Tools"
$OutputDir = "[Environment]::GetFolderPath("Desktop")\GitHub\WixInstaller\Output"

#----------------------------
Write-Debug("GetTools")
#----------------------------

if (-Not(Test-Path -Path "$ToolsDir\nuget.exe" -ErrorAction SilentlyContinue) ) {
    $url = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"  
    Invoke-WebRequest -Uri $url -OutFile "$ToolsDir\nuget.exe"
}

#This includes all the Wix build tools - harrah!
if (-Not(Test-Path -Path "$ToolsDir\WiX\tools\wix\candle.exe" -ErrorAction SilentlyContinue) ) {
    & "$ToolsDir\nuget.exe" install WiX -OutputDirectory "$ToolsDir" -ExcludeVersion
}

#----------------------------
Write-Debug("Build")
#----------------------------

# https://wixtoolset.org//documentation/manual/v3/wixui/wixui_dialog_library.html
# https://wixtoolset.org//documentation/manual/v3/wixui/wixui_customizations.html
# https://wixtoolset.org//documentation/manual/v3/wixui/dialog_reference/wixui_minimal.html

$version = '0.0.5'

cd $RootDir

& "$ToolsDir\WiX\tools\candle.exe" -dProductVersion="$version" -dBinDir="$version" PomoFish.wxs -out PomoFish.$version.wixobj
& "$ToolsDir\WiX\tools\light.exe" -ext WixUIExtension -dWixUILicenseRtf="License.rtf" PomoFish.$version.wixobj -out PomoFish.$version.msi 
