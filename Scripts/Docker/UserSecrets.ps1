param (
	[string] $certkey,
	[string] $clientSecret
)

$serviceFolders = Get-ChildItem -Path "../../Services" -Directory -Force

foreach ($folder in $serviceFolders)
{
	Set-Location $folder.FullName
	
	dotnet user-secrets set MooMed.CertKey $certkey
	dotnet user-secrets set AZURE_CLIENT_SECRET $clientSecret
}
