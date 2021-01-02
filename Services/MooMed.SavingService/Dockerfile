#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update
RUN apt-get install curl -y

ENV MooMed_Cache_BaseTtlInSeconds=3600
ENV MooMed_Logging_TableStorageConnectionString="DefaultEndpointsProtocol=https;AccountName=moomedlogging;AccountKey=EkZOplhXL0ogByoVww+9+YxstcSsdZ5/fxN4QdrGkONzWL47GNuZwfCGOlfaNJpHB/21hZIvQV75UrZt5seg4PQOH5+m1LfAZkKEPSvTHz0=;EndpointSuffix=core.windows.net"
ENV MooMed_Db_Saving="Server=tcp:moomeddbserver.database.windows.net,1433;Initial Catalog=Saving;Persist Security Info=False;User ID=moomedadmin;Password=bRlJ+y2DiUBAEnxADvaUbxVuLB7N7ASdnYbmzBtFHsQ=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

ENV AZURE_CLIENT_ID "82259ded-f445-4db4-80d4-1c07f13c50b7"
ENV AZURE_TENANT_ID "ff009f2c-d651-472e-b73e-e0f894c6011e"

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/MooMed.SavingService/MooMed.SavingService.csproj", "Services/MooMed.SavingService/"]
COPY ["Common/MooMed.Logging.Abstractions/MooMed.Logging.Abstractions.csproj", "Common/MooMed.Logging.Abstractions/"]
COPY ["Common/MooMed.Common.Definitions/MooMed.Common.Definitions.csproj", "Common/MooMed.Common.Definitions/"]
COPY ["Common/MooMed.Common.ServiceBase/MooMed.Common.ServiceBase.csproj", "Common/MooMed.Common.ServiceBase/"]
COPY ["Common/MooMed.Core/MooMed.Core.csproj", "Common/MooMed.Core/"]
COPY ["Common/MooMed.Azure/MooMed.Azure.csproj", "Common/MooMed.Azure/"]
COPY ["Common/MooMed.DotNet/MooMed.DotNet.csproj", "Common/MooMed.DotNet/"]
COPY ["Common/MooMed.Eventing/MooMed.Eventing.csproj", "Common/MooMed.Eventing/"]
COPY ["Common/MooMed.SignalR/MooMed.SignalR.csproj", "Common/MooMed.SignalR/"]
COPY ["Common/MooMed.ServiceBase/MooMed.ServiceBase.csproj", "Common/MooMed.ServiceBase/"]
COPY ["Common/MooMed.Identity/MooMed.Identity.csproj", "Common/MooMed.Identity/"]
COPY ["Common/MooMed.RemotingProxies/MooMed.RemotingProxies.csproj", "Common/MooMed.RemotingProxies/"]
COPY ["Common/MooMed.IPC/MooMed.IPC.csproj", "Common/MooMed.IPC/"]
COPY ["Common/MooMed.Grpc/MooMed.Grpc.csproj", "Common/MooMed.Grpc/"]
COPY ["Modules/MooMed.Module.Monitoring/MooMed.Module.Monitoring.csproj", "Modules/MooMed.Module.Monitoring/"]
COPY ["Common/MooMed.Caching/MooMed.Caching.csproj", "Common/MooMed.Caching/"]
COPY ["Common/MooMed.Serialization/MooMed.Serialization.csproj", "Common/MooMed.Serialization/"]
COPY ["Common/MooMed.Configuration/MooMed.Configuration.csproj", "Common/MooMed.Configuration/"]
COPY ["Common/MooMed.Encryption/MooMed.Encryption.csproj", "Common/MooMed.Encryption/"]
COPY ["Common/MooMed.AspNetCore/MooMed.AspNetCore.csproj", "Common/MooMed.AspNetCore/"]
COPY ["Common/MooMed.DependencyInjection/MooMed.DependencyInjection.csproj", "Common/MooMed.DependencyInjection/"]
COPY ["Common/MooMed.Logging/MooMed.Logging.csproj", "Common/MooMed.Logging/"]
COPY ["Modules/MooMed.Module.Saving/MooMed.Module.Saving.csproj", "Modules/MooMed.Module.Saving/"]
COPY ["Modules/MooMed.Module.Account/MooMed.Module.Accounts.csproj", "Modules/MooMed.Module.Account/"]
COPY ["Common/MooMed.AspNetCore.Identity/MooMed.AspNetCore.Identity.csproj", "Common/MooMed.AspNetCore.Identity/"]
COPY ["Common/MooMed.Common.Database/MooMed.Common.Database.csproj", "Common/MooMed.Common.Database/"]
RUN dotnet restore "Services/MooMed.SavingService/MooMed.SavingService.csproj"
COPY . .
WORKDIR "/src/Services/MooMed.SavingService"
RUN dotnet build "MooMed.SavingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MooMed.SavingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMed.SavingService.dll"]