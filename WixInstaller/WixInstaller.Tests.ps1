#cd C:\Users\jfinnangelo\Desktop\pester
#Set-ExecutionPolicy Unrestricted
#Set-ExecutionPolicy Default

# Note - TesName filters to thye 'Describe' blocks
# Invoke-Pester -Path .\WixInstaller.Tests.ps1 -TestName "Check Get-SourceDir"

# Pester tests
Describe "Check Get-SourceDir" {

  BeforeAll {
    Set-StrictMode -Version Latest
    . .\WixInstaller.Module.ps1
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
    $result | Should Not Be "xyzzy"#$null
  }

  It "Given $env:Build_BuildId, Then it is not null" {
    $result = $env:Build_BuildId
    $result | Should Not Be "xyzzy"
  }

  It "Given the PSScriptRoot, Then can make a sourceDir" {
    $result = Convert-Path "$PSScriptRoot\.."
    $expected = [Environment]::GetFolderPath("Desktop") + "\GitHub\PomoFish" 
    $result | Should Be $expected
  }
    
  It "Given the Get-SourceDir with null parameter, get my local dev source dir" {
    $result = Get-SourceDir $null
    $result | Should Be $localDevSourceDir
  }
}

