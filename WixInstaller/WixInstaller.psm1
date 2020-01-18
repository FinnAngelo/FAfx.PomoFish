Set-StrictMode  -Version Latest

function Get-SourcesDirectory {
    if ([String]::IsNullOrWhiteSpace($env:Build_SourcesDirectory) ) {         
        $sourceDir = $PSScriptRoot + "\.."
    } else {
        $sourceDir = $env:Build_SourcesDirectory
    }

    $result = Convert-Path $sourceDir

    if (Test-Path $result -PathType Container) {
        return $result
    }
    else {
        throw "$result is not a valid path"
    }
}
