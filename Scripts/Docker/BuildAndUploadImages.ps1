param (
	[string] $configuration
)

[string[]] $availableConfigs = "dev", "testing", "prod"

class ImageInfo {
    [string]$Path
    [string]$Name
}

[ImageInfo[]] $imageInfos = @();

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed/Dockerfile"
    Name = "frontendservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.FinanceService/Dockerfile"
    Name = "financeservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.Stateful.AccountService/Dockerfile"
    Name = "accountservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.Stateful.AccountValidationService/Dockerfile"
    Name = "accountvalidationservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.Stateful.ProfilePictureService/Dockerfile"
    Name = "profilepictureservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.Stateful.SearchService/Dockerfile"
    Name = "searchservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.Stateful.SessionService/Dockerfile"
    Name = "sessionservice"
}

az acr login --name moomed

cd ..
cd ..

foreach ($imageInfo in $imageInfos) {
    
	Write-Host "Building $imageInfo.Path"

    $imgName = "moomed.azurecr.io/" + $imageInfo.Name + ":" + $configuration
    docker build -t $imgName -f $imageInfo.Path .
	
	Write-Host "Built $imageInfo.Path"

    docker push $imgName

	Write-Host "Pushed $imageInfo.Path"
}