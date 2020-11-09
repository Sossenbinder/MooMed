param (
	[string] $configuration
)

[string[]] $availableConfigs = "dev", "testing", "prod"

if ($availableConfigs -contains $configuration)
{
	Write-Host "Invalid configuration, only dev / testing / prod are allowed";
	return;
}

class ImageInfo {
	[string]$Path
	[string]$Name
}

[ImageInfo[]] $imageInfos = @();

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.Frontend/Dockerfile"
	Name = "frontendservice"
}

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.FinanceService/Dockerfile"
	Name = "financeservice"
}

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.AccountService/Dockerfile"
	Name = "accountservice"
}

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.AccountValidationService/Dockerfile"
	Name = "accountvalidationservice"
}

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.ProfilePictureService/Dockerfile"
	Name = "profilepictureservice"
}

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.SearchService/Dockerfile"
	Name = "searchservice"
}

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.ChatService/Dockerfile"
	Name = "chatservice"
}

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.SessionService/Dockerfile"
	Name = "sessionservice"
}

$imageInfos += [ImageInfo]@{
    Path = "Services/MooMed.Monitoring/Dockerfile"
    Name = "monitoringservice"
}

az acr login --name moomed

foreach ($imageInfo in $imageInfos) {
	
	Write-Host "Building$($imageInfo.Path)"

	$imgName = "moomed.azurecr.io/" + $imageInfo.Name + ":" + $configuration
	docker build -t $imgName -f "../../$($imageInfo.Path)" ../..
	
	Write-Host "Built $($imageInfo.Path)"

	docker push $imgName

	Write-Host "Pushed $($imageInfo.Path)"

	docker image rm $imgName
}