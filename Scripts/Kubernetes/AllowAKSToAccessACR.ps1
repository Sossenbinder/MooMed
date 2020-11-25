$AKS_RESOURCE_GROUP="moomed"
$AKS_CLUSTER_NAME="moomed"
$ACR_RESOURCE_GROUP="SchranzTesting"
$ACR_NAME="moomed"
 
# Get the id of the service principal configured for AKS
$CLIENT_ID=$(az aks show --resource-group $AKS_RESOURCE_GROUP --name $AKS_CLUSTER_NAME --query "servicePrincipalProfile.clientId" --output tsv)
 
# Get the ACR registry resource id
$ACR_ID=$(az acr show --name $ACR_NAME --resource-group $ACR_RESOURCE_GROUP --subscription "91fe1813-0715-441d-907b-0aba6bc5fc19" --query "id" --output tsv)
 
# Create role assignment
az role assignment create --assignee $CLIENT_ID --role acrpull --scope $ACR_ID