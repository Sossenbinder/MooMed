param (
	[string] $password
)

kubectl create secret docker-registry acrimgpullsecret --docker-server=https://moomed.azurecr.io --docker-username=moomed --docker-password=$password