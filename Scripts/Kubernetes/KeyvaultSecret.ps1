param (
	[string] $secretVal
)

kubectl create secret generic akv --from-literal client_secret=$secretVal