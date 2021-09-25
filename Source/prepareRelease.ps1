param([string]$version, [string]$release = 'latest', [string]$outputPath, [switch]$force = $false, [switch]$WhatIf = $false)

. .\functions.ps1

Push-Location
Set-Location ..
$outputPath = $outputPath.TrimEnd('/')

try {
	# Take the latest release version from existing update news.
	$release = 'latest' -eq $release.toLower() ? ("Source\Content\Changelog\$version\*" | Get-ChildItem | ForEach-Object { $_.Name.Split('-')[0].Substring(1) } | Sort-Object { [Version]$_ } -Top 1) : $release

	try {
		[Version]$ver = $release ?? ''
	}
	catch {
		"Invalid release version `"$release`", aborting."
		throw
	}

	[xml]$metadata = Get-Content -Path 'About\About.xml'
	$parentName = sanitizeString($metadata.ModMetaData.packageId + '_UpdateNews')
	$templateOutputPath = "$outputPath\News\AbstractBase.xml"

	"Preparing release for RW $version and mod release v$release$($force ? ' (forced)' : '')…"
	"`tGenerating update news…"

	if($force -or !(Test-Path $templateOutputPath -PathType Leaf)) {
		"`t`tGenerating the base news entry at $templateOutputPath…"
		$templatePath = 'Source\Content\Changelog\News-template.xml'
		[xml]$baseXml = Get-Content -Path $templatePath

		$root = $baseXml.Defs
		$entry = $root.'HugsLib.UpdateFeatureDef'
		$root.RemoveAll()
		$root.AppendChild($entry) | Out-Null
		$entry.Name = $parentName
		$entry.modNameReadable = $metadata.ModMetaData.name
		$entry.linkUrl = $metadata.ModMetaData.url
		if(!$WhatIf) {
			Save-XML -xml $baseXml -path $templateOutputPath | Out-Null
		}
	}

	$newsOutputPath = "$outputPath\News\v$release.xml"
	$exists = Test-Path $newsOutputPath -PathType Leaf
	if($exists -and $force) {
		"`t`tWarning: $newsOutputPath already exists, overwriting."
	}
	if($force -or !$exists) {
		$sourcePath = "Source\Content\Changelog\$version\v$release-news.xml"
		[xml]$newsXml = Get-Content -Path $sourcePath

		$entry = $newsXml.'HugsLib.UpdateFeatureDef'
		$entry.SetAttribute('ParentName', $parentName)
		$entry.SelectNodes('assemblyVersion') + $entry.SelectNodes('defName') | Remove-Node | Out-Null
		Add-Node -parent $entry -name 'assemblyVersion' -value $release -prepend | Out-Null
		Add-Node -parent $entry -name 'defName' -value (sanitizeString($metadata.ModMetaData.packageId + "_UpdateNews_$release")) -prepend | Out-Null
		# Add the required `Defs` root node by swapping places between it and `$entry`.
		$newsXml.ReplaceChild($newsXml.createElement('Defs'), $entry) | Out-Null
		$newsXml.DocumentElement.AppendChild($entry) | Out-Null

		if(!$WhatIf) {
			Save-XML -xml $newsXml -path $newsOutputPath | Out-Null
		}
	}

	'Finished preparing release!'
}
catch {
	throw $_
}
finally {
	Pop-Location
	throw
}
