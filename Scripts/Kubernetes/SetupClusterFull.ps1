param (
	[string] $configuration,
	[string] $registryPassword,
	[string] $kvClientSecret
)

Invoke-Expression "linkerd install"

if ($configuration -ne "dev") {
	Invoke-Expression "helm install ingress-nginx ingress-nginx/ingress-nginx --namespace=ingress-nginx --create-namespace"
}
else {
	Invoke-Expression "kubectl apply -f .\Kind\kindIngress.yaml"
}

cd nginxIngress
Invoke-Expression ".\tlsCert.ps1"
cd ..

Invoke-Expression ".\AllowLocalClusterToAccessACR.ps1 $registryPassword"

Invoke-Expression ".\KeyvaultSecret.ps1 $kvClientSecret"

Invoke-Expression ".\DeployAllK8s.ps1 $configuration"