#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update
RUN apt-get install curl -y

ENV MooMed_Cache_BaseTtlInSeconds=3600
ENV MooMed_Logging_TableStorageConnectionString="DefaultEndpointsProtocol=https;AccountName=moomedlogging;AccountKey=EkZOplhXL0ogByoVww+9+YxstcSsdZ5/fxN4QdrGkONzWL47GNuZwfCGOlfaNJpHB/21hZIvQV75UrZt5seg4PQOH5+m1LfAZkKEPSvTHz0=;EndpointSuffix=core.windows.net"

ENV AZURE_CLIENT_ID "f87c57a9-f688-436b-aa7b-34a1de5cf85a"
ENV AZURE_TENANT_ID "ff009f2c-d651-472e-b73e-e0f894c6011e"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/MooMed.Frontend/MooMed.Frontend.csproj", "Services/MooMed.Frontend/"]
COPY ["Services/MooMed.AccountValidationService/MooMed.AccountValidationService.csproj", "Services/MooMed.AccountValidationService/"]
COPY ["Common/MooMed.Common.ServiceBase/MooMed.Common.ServiceBase.csproj", "Common/MooMed.Common.ServiceBase/"]
COPY ["Common/MooMed.Grpc/MooMed.Grpc.csproj", "Common/MooMed.Grpc/"]
COPY ["Common/MooMed.Common.Definitions/MooMed.Common.Definitions.csproj", "Common/MooMed.Common.Definitions/"]
COPY ["Common/MooMed.Core/MooMed.Core.csproj", "Common/MooMed.Core/"]
COPY ["Common/MooMed.Azure/MooMed.Azure.csproj", "Common/MooMed.Azure/"]
COPY ["Common/MooMed.DotNet/MooMed.DotNet.csproj", "Common/MooMed.DotNet/"]
COPY ["Common/MooMed.Logging/MooMed.Logging.csproj", "Common/MooMed.Logging/"]
COPY ["Common/MooMed.Configuration/MooMed.Configuration.csproj", "Common/MooMed.Configuration/"]
COPY ["Common/MooMed.Encryption/MooMed.Encryption.csproj", "Common/MooMed.Encryption/"]
COPY ["Common/MooMed.Identity/MooMed.Identity.csproj", "Common/MooMed.Identity/"]
COPY ["Modules/MooMed.Module.Account/MooMed.Module.Accounts.csproj", "Modules/MooMed.Module.Account/"]
COPY ["Common/MooMed.SignalR/MooMed.SignalR.csproj", "Common/MooMed.SignalR/"]
COPY ["Common/MooMed.Eventing/MooMed.Eventing.csproj", "Common/MooMed.Eventing/"]
COPY ["Common/MooMed.Common.Database/MooMed.Common.Database.csproj", "Common/MooMed.Common.Database/"]
COPY ["Common/MooMed.IPC/MooMed.IPC.csproj", "Common/MooMed.IPC/"]
COPY ["Common/MooMed.Caching/MooMed.Caching.csproj", "Common/MooMed.Caching/"]
COPY ["Common/MooMed.AspNetCore/MooMed.AspNetCore.csproj", "Common/MooMed.AspNetCore/"]
COPY ["Services/MooMed.FinanceService/MooMed.FinanceService.csproj", "Services/MooMed.FinanceService/"]
COPY ["Modules/MooMed.Module.Finance/MooMed.Module.Finance.csproj", "Modules/MooMed.Module.Finance/"]
COPY ["Modules/MooMed.Module.Portfolio/MooMed.Module.Portfolio.csproj", "Modules/MooMed.Module.Portfolio/"]
COPY ["Services/MooMed.SearchService/MooMed.SearchService.csproj", "Services/MooMed.SearchService/"]
COPY ["Common/MooMed.DependencyInjection/MooMed.DependencyInjection.csproj", "Common/MooMed.DependencyInjection/"]
COPY ["Services/MooMed.AccountService/MooMed.AccountService.csproj", "Services/MooMed.AccountService/"]
COPY ["Services/MooMed.ProfilePictureService/MooMed.ProfilePictureService.csproj", "Services/MooMed.ProfilePictureService/"]
COPY ["Services/MooMed.SessionService/MooMed.SessionService.csproj", "Services/MooMed.SessionService/"]
COPY ["Modules/MooMed.Module.Session/MooMed.Module.Session.csproj", "Modules/MooMed.Module.Session/"]
COPY ["Services/MooMed.ChatService/MooMed.ChatService.csproj", "Services/MooMed.ChatService/"]
COPY ["Modules/MooMed.Module.Chat/MooMed.Module.Chat.csproj", "Modules/MooMed.Module.Chat/"]
RUN dotnet restore "Services/MooMed.Frontend/MooMed.Frontend.csproj"
COPY . .
WORKDIR "/src/Services/MooMed.Frontend"
RUN dotnet build "MooMed.Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MooMed.Frontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMed.Frontend.dll"]
