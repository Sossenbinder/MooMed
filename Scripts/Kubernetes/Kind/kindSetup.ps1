param (
	[string] $configuration,
	[string] $registryPassword,
	[string] $kvClientSecret
)

kind create cluster --config ./kindConfig.yaml

Start-Sleep 30

docker network connect "kind" "kindRegistry"

kubectl apply -f ./kindLocalRegistry.yaml

Invoke-Expression "..\SetupClusterFull.ps1 $configuration $registryPassword $kvClientSecret"