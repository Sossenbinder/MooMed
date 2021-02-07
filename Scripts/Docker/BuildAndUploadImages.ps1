param (
	[string] $configuration
)

[string[]] $availableConfigs = "dev", "testing", "prod"

if (!$availableConfigs -contains $configuration) {
	Write-Host "Invalid configuration, only dev / testing / prod are allowed";
	return;
}

class ImageInfo {
	[string]$Path
	[string]$Name
}

[ImageInfo[]] $imageInfos = @();

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
	Path = "Services/MooMed.SavingService/Dockerfile"
	Name = "savingservice"
}

$imageInfos += [ImageInfo]@{
	Path = "Services/MooMed.Frontend/Dockerfile"
	Name = "frontendservice"
}

az acr login --name moomed

foreach ($imageInfo in $imageInfos) {
	
	Write-Host "Building $($imageInfo.Path)"

	$imgHost = "moomed.azurecr.io";

	if ($configuration -eq "dev") {
		$imgHost = "localhost:5000";
	}

	$imgName = $imgHost + "/" + $imageInfo.Name + ":" + $configuration
	docker build -t $imgName -f "../../$($imageInfo.Path)" ../.. > Out-Null

	if ($LASTEXITCODE -eq 0) {
		
		Write-Host "Built $($imageInfo.Path)" -ForegroundColor Green

		docker push $imgName > Out-Null

		Write-Host "Pushed $($imageInfo.Path)" -ForegroundColor Green

		docker image rm $imgName > Out-Null
	}
	else {
		$ErrorString = $result -join [System.Environment]::NewLine
		Write-Host "Building $($imageInfo.Path) failed with $($ErrorString)" -ForegroundColor red
	}
}