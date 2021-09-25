param([string]$configuration, [string]$in, [string]$out)

. .\functions.ps1

"`loadFolders.xml` fixup ($configuration)…"
[xml]$xml = Get-Content -Path $in

$suffix = $configuration -ne 'Release' ? "-$configuration" : ''
foreach($ver in $xml.loadFolders.ChildNodes) {
	$ver.ChildNodes | Where-Object -Property InnerText -Like 'v?.?' | ForEach-Object {
		$f = $_
		$ver.ChildNodes | Where-Object -Property InnerText -Like ($f.InnerText + '-?*') | ForEach-Object {
			$_.ParentNode.RemoveChild($_)
		}
		if ($suffix) {
			$n = $xml.createElement('li')
			$n.innerText = "$($f.InnerText)$suffix"
			$ver.InsertAfter($n, $f)
		}
	} | Out-Null
}

Save-XML -xml $xml -path $out | Out-Null

$ts = Get-Date
$in, $out | Get-Item | ForEach-Object { $_.lastaccesstime = $_.lastwritetime = $ts }
'Fixup done.'
