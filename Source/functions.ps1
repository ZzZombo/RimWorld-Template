$ErrorActionPreference = 'Stop'
function Add-Node($parent, [string]$name, [string]$value, [switch]$prepend = $false) {
	$child = $parent.OwnerDocument.createElement($name)
	$child.InnerText = $value
	$prepend ? $parent.PrependChild($child) : $parent.AppendChild($child) | Out-Null
	return $child
}

function Import-Node($parent, $value) {
	$node = $parent.OwnerDocument.ImportNode($value)
	$parent.AppendChild($node) | Out-Null
	return $node
}

filter Remove-Node {
	try {
		$_.ParentNode.RemoveChild($_) | Out-Null
		return $_
	}
	catch { }
}

function Save-XML([OutputType([string])][xml]$xml, [string]$path) {
	$ws = New-Object -TypeName System.XML.XmlWriterSettings
	$ws.Indent = $true
	$ws.WriteEndDocumentOnClose = $false
	$ws.IndentChars = "`t"
	New-Item -ItemType Directory -Force -Path "$path\.." -ErrorAction SilentlyContinue | Out-Null
	$fs = New-Object -TypeName System.IO.FileStream -ArgumentList $path, OpenOrCreate
	try {
		$writer = [System.Xml.XmlWriter]::Create($fs, $ws)
		$xml.Save($writer)
		$writer.Flush()
	}
	finally {
		$writer.Close()
		$fs.Close()
	}
	return Get-Content -Path $path
}

function sanitizeString([OutputType([string])][string]$s) {
	return $s -replace '\W', '_'
}

. .\normalizePath.ps1
function relativePath([OutputType([string])][string]$base, [string]$other) {
	return (Get-NormalizedFileSystemPath -Path "$base\$other").Replace($PWD, '').Substring(1)
}
