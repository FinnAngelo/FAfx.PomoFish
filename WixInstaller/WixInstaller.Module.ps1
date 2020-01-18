Set-StrictMode  -Version Latest

function Get-SourceDir([string]$sourceDir) {
    if ([String]::IsNullOrWhiteSpace($sourceDir) ) {         
        $sourceDir = $PSScriptRoot + "\.."
    }

    $result = Convert-Path $sourceDir

    if (Test-Path $result -PathType Container) {
        return $result
    }
    else {
        throw "$result is not a valid path"
    }
}
