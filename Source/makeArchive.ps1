using namespace System.Collections.Generic
param([string]$path, [string]$root, [string]$versions = 'v1.x', [string]$configuration = 'Release')

$assets = 'About', 'LICENSE', 'loadFolders.xml'
[List[string]]$versions = $versions.Split(';')

# Handle RW v1.0.
$versions | Where-Object { $_ -contains 'v1.0' } | ForEach-Object { $versions.Remove($_); $assets += 'Textures', 'Languages', 'Defs', 'News', 'Patches' } | Out-Null
$assets += $versions | ForEach-Object { $_, ($configuration.Equals( 'Release') ? $null : "$_-$configuration") }
$assets | Select-Object -Unique | Where-Object { $_ -and (Test-Path -Path "$root\$_") } | ForEach-Object { "Zipping ``$_``…"; Get-Item -Path "$root\$_" | Compress-Archive -Update -DestinationPath $path }
