#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV MooMed.ProfilePictures.StorageAccountUrl="https://moomedprofilepictures.blob.core.windows.net"
ENV MooMed.ProfilePictures.ConnectionString="DefaultEndpointsProtocol=https;AccountName=moomedprofilepictures;AccountKey=XLMa/K9ukAcqkd1eGXuD5tgGNPaVTdo5JK+nCny3ymdC8zhpAjPfXWyk5oJGNS+fBkA9lERn/+tSSkWFDgRcd37gT0sRqMTvb0FnqQbSl9s=;EndpointSuffix=core.windows.net"


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/MooMed.Stateful.ProfilePictureService/MooMed.Stateful.ProfilePictureService.csproj", "Services/MooMed.Stateful.ProfilePictureService/"]
COPY ["Common/MooMed.Common.ServiceBase/MooMed.Common.ServiceBase.csproj", "Common/MooMed.Common.ServiceBase/"]
COPY ["Common/MooMed.Grpc/MooMed.Grpc.csproj", "Common/MooMed.Grpc/"]
COPY ["Common/MooMed.Core/MooMed.Core.csproj", "Common/MooMed.Core/"]
COPY ["Common/MooMed.Common.Definitions/MooMed.Common.Definitions.csproj", "Common/MooMed.Common.Definitions/"]
COPY ["Common/MooMed.Azure/MooMed.Azure.csproj", "Common/MooMed.Azure/"]
COPY ["Common/MooMed.IPC/MooMed.IPC.csproj", "Common/MooMed.IPC/"]
COPY ["Common/MooMed.Caching/MooMed.Caching.csproj", "Common/MooMed.Caching/"]
COPY ["Common/MooMed.AspNetCore/MooMed.AspNetCore.csproj", "Common/MooMed.AspNetCore/"]
RUN dotnet restore "Services/MooMed.Stateful.ProfilePictureService/MooMed.Stateful.ProfilePictureService.csproj"
COPY . .
WORKDIR "/src/Services/MooMed.Stateful.ProfilePictureService"
RUN dotnet build "MooMed.Stateful.ProfilePictureService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MooMed.Stateful.ProfilePictureService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMed.Stateful.ProfilePictureService.dll"]
