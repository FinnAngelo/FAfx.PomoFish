#cd C:\Users\jfinnangelo\Desktop\pester
#Set-ExecutionPolicy Unrestricted
#Set-ExecutionPolicy Default

# Note - TesName filters to thye 'Describe' blocks
# Invoke-Pester -Path .\WixInstaller.Tests.ps1 -TestName "Check Get-SourceDir"

# https://powershellexplained.com/2017-05-27-Powershell-module-building-basics/
# New-ModuleManifest `
#   -Path '..\WixInstaller\WixInstaller.psd1' `
#   -RootModule 'WixInstaller.psm1' `
#   -Author "Jon Finn Angelo" `
#   -CompanyName "Jon Finn Angelo" `
#   -Description "Used to build a wix installer"




# Pester tests
Describe "Check Get-SourceDir" -Tags "ExcludeOnAzure" {

  BeforeAll {
    Set-StrictMode -Version Latest
    if ([String]::IsNullOrWhiteSpace($env:Build_SourcesDirectory) ) {        
      Import-Module .\WixInstaller.psm1
    }
    else {
      Import-Module $env:Build_SourcesDirectory\WixInstaller\WixInstaller.psm1
    }
    $localDevSourceDir = [Environment]::GetFolderPath("Desktop") + "\GitHub\PomoFish"
    $isAzureBuild = ($env:Build_BuildId -ne $null)
  }

  AfterAll {
  }
  
  It "Given an undefined value, Then there is an exception" {
    { $result = $undefinedValue } | Should Throw "cannot be retrieved because it has not been set"
  }

  It "Given an empty string, Then there is an exception" {
    $value = $null
    { 
      if ([String]::IsNullOrWhiteSpace($value)) {
        throw "string is empty"
      } 
    } | Should Throw "string is empty"
  }

  It "Given PSScriptRoot, Then it is not null" {
    $result = $PSScriptRoot
    $result | Should Not Be $null
  }

  It 'Given Senv:Build_BuildId, Then it is not null' {
    $result = $env:Build_BuildId
    $result | Should Not Be "xyzzy"
  }

  It 'Given the PSScriptRoot, Then can make a sourceDir' {
    $result = Convert-Path "$PSScriptRoot\.."
    $expected = [Environment]::GetFolderPath("Desktop") + "\GitHub\PomoFish" 
    $result | Should Be $expected
  }
    
  It 'Given the Get-SourceDir with null parameter, get my local dev source dir' {
    $result = Get-SourcesDirectory $null
    $result | Should Be $localDevSourceDir
  }
}

