#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

ENV MooMed_Cache_BaseTtlInSeconds=3600
ENV MooMed_Logging_TableStorageConnectionString="DefaultEndpointsProtocol=https;AccountName=moomedlogging;AccountKey=EkZOplhXL0ogByoVww+9+YxstcSsdZ5/fxN4QdrGkONzWL47GNuZwfCGOlfaNJpHB/21hZIvQV75UrZt5seg4PQOH5+m1LfAZkKEPSvTHz0=;EndpointSuffix=core.windows.net"

ENV AZURE_CLIENT_ID "82259ded-f445-4db4-80d4-1c07f13c50b7"
ENV AZURE_TENANT_ID "ff009f2c-d651-472e-b73e-e0f894c6011e"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/MooMed/MooMed.Web.csproj", "Services/MooMed/"]
COPY ["Services/MooMed.Stateful.AccountValidationService/MooMed.Stateful.AccountValidationService.csproj", "Services/MooMed.Stateful.AccountValidationService/"]
COPY ["Common/MooMed.Common.ServiceBase/MooMed.Common.ServiceBase.csproj", "Common/MooMed.Common.ServiceBase/"]
COPY ["Common/MooMed.Grpc/MooMed.Grpc.csproj", "Common/MooMed.Grpc/"]
COPY ["Common/MooMed.Core/MooMed.Core.csproj", "Common/MooMed.Core/"]
COPY ["Common/MooMed.Common.Definitions/MooMed.Common.Definitions.csproj", "Common/MooMed.Common.Definitions/"]
COPY ["Common/MooMed.Azure/MooMed.Azure.csproj", "Common/MooMed.Azure/"]
COPY ["Modules/MooMed.Module.Account/MooMed.Module.Accounts.csproj", "Modules/MooMed.Module.Account/"]
COPY ["Common/MooMed.IPC/MooMed.IPC.csproj", "Common/MooMed.IPC/"]
COPY ["Common/MooMed.Caching/MooMed.Caching.csproj", "Common/MooMed.Caching/"]
COPY ["Common/MooMed.AspNetCore/MooMed.AspNetCore.csproj", "Common/MooMed.AspNetCore/"]
COPY ["Modules/MooMed.Module.Search/MooMed.Module.Search.csproj", "Modules/MooMed.Module.Search/"]
COPY ["Services/MooMed.Stateful.SearchService/MooMed.Stateful.SearchService.csproj", "Services/MooMed.Stateful.SearchService/"]
COPY ["Services/MooMed.Stateful.ProfilePictureService/MooMed.Stateful.ProfilePictureService.csproj", "Services/MooMed.Stateful.ProfilePictureService/"]
COPY ["Services/MooMed.Stateful.AccountService/MooMed.Stateful.AccountService.csproj", "Services/MooMed.Stateful.AccountService/"]
COPY ["Services/MooMed.Stateful.SessionService/MooMed.Stateful.SessionService.csproj", "Services/MooMed.Stateful.SessionService/"]
COPY ["Modules/MooMed.Module.Session/MooMed.Module.Session.csproj", "Modules/MooMed.Module.Session/"]
RUN dotnet restore "Services/MooMed/MooMed.Web.csproj"
COPY . .
WORKDIR "/src/Services/MooMed"
RUN dotnet build "MooMed.Web.csproj" -c Release -o /app/build

FROM build AS publish 
RUN dotnet publish "MooMed.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMed.Web.dll"]