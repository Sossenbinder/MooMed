param (
	[string] $configuration,
	[string] $registryPassword,
	[string] $kvClientSecret
)

Invoke-Expression "linkerd install"

Invoke-Expression "helm install ingress-nginx ingress-nginx/ingress-nginx --namespace=ingress-nginx --create-namespace"

Invoke-Expression ".\AllowLocalClusterToAccessACR.ps1 $registryPassword"

Invoke-Expression ".\KeyvaultSecret.ps1 $kvClientSecret"

Invoke-Expression ".\DeployAllK8s.ps1 $configuration"