param (
	[string] $configuration
)

[string[]] $availableConfigs = "dev", "testing", "prod"

if ($availableConfigs -contains $configuration) {
	Invoke-Expression "kubectl apply -f ../General/RabbitMQ/RabbitMq.yaml";
	Invoke-Expression "kubectl apply -f ../General/Redis/Redis.yaml";

	$serviceFolders = Get-ChildItem -Path "../../Services" -Directory -Force

	foreach ($folder in $serviceFolders) {
		$configKubeDir = Join-Path -Path $folder.FullName -ChildPath "Kubernetes/overlays/$configuration"
		Invoke-Expression "kubectl apply -k $configKubeDir"
	}
}
else {	
	Write-Host "$configuration not found in allowed configs."
	throw;
}