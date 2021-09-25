param([string]$versions, [switch]$debug = $false)

Push-Location
Set-Location ..

try {
	$masks = ($versions.Replace('.', '\.').Split(';') | ForEach-Object { '^' + $_ + '-\w+$' }) -join '|'

	Get-ChildItem -Directory | ForEach-Object {
		$f = $_ | Get-ChildItem -Recurse | Where-Object -Property Length -GT 0 | Select-Object -First 1 -Property FullName
		if($debug) {
			$fn = $f ? (Resolve-Path -Path $f.FullName -Relative) : '<none>'
			"$($_.Fullname): $fn" | Write-Output
		}
		if(-not $f) {
			"Removing empty folder $_" | Write-Output;
			$_ | Remove-Item -Recurse #-WhatIf
		}
		elseif($_.Name -Match $masks) {
			"Removing non-release folder $_" | Write-Output;
			$_ | Remove-Item -Recurse #-WhatIf
		}
	}

	'Source\Content\Changelog\Images' | New-Item -ItemType Directory -ErrorAction SilentlyContinue
}
catch {
	throw $_
}
finally {
	Pop-Location
}
