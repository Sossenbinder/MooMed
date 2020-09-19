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
    Path = "./Services/MooMed.AccountService/Dockerfile"
    Name = "accountservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.AccountValidationService/Dockerfile"
    Name = "accountvalidationservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.ProfilePictureService/Dockerfile"
    Name = "profilepictureservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.SearchService/Dockerfile"
    Name = "searchservice"
}

$imageInfos += [ImageInfo]@{
    Path = "./Services/MooMed.SessionService/Dockerfile"
    Name = "sessionservice"
}

cd ../../

foreach ($imageInfo in $imageInfos) {
    
    $imgName = "127.0.0.1:5000/" + $imageInfo.Name
    docker build -t $imgName -f $imageInfo.Path .

    docker push $imgName
}