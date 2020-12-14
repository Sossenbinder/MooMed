param (
	[string] $configuration
)

[string[]] $availableConfigs = "dev", "testing", "prod"

if ($availableConfigs -contains $configuration)
{
	Invoke-Expression "kubectl delete -f ../General/RabbitMQ/RabbitMq.yaml";

	$serviceFolders = Get-ChildItem -Path "../../Services" -Directory -Force

	foreach ($folder in $serviceFolders)
	{
		$configKubeDir = Join-Path -Path $folder.FullName -ChildPath "Kubernetes/overlays/$configuration"
		Invoke-Expression "kubectl delete -k $configKubeDir"
	}
}
else 
{	
	Write-Host "$configuration not found in allowed configs."
	throw;
}