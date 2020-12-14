param (
	[string] $configuration,
	[string] $registryPassword,
	[string] $kvClientSecret
)

cd Istio

Invoke-Expression ".\IstioInstall.ps1"

cd ..

Invoke-Expression ".\AllowLocalClusterToAccessACR.ps1 $registryPassword"

Invoke-Expression ".\KeyvaultSecret.ps1 $kvClientSecret"

Invoke-Expression ".\DeployAllK8s.ps1 $configuration"