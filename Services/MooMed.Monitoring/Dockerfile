#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV MooMed_Cache_BaseTtlInSeconds=3600
ENV MooMed_Logging_TableStorageConnectionString="DefaultEndpointsProtocol=https;AccountName=moomedlogging;AccountKey=EkZOplhXL0ogByoVww+9+YxstcSsdZ5/fxN4QdrGkONzWL47GNuZwfCGOlfaNJpHB/21hZIvQV75UrZt5seg4PQOH5+m1LfAZkKEPSvTHz0=;EndpointSuffix=core.windows.net"

ENV AZURE_CLIENT_ID "82259ded-f445-4db4-80d4-1c07f13c50b7"
ENV AZURE_TENANT_ID "ff009f2c-d651-472e-b73e-e0f894c6011e"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/MooMed.Monitoring/MooMed.Monitoring.csproj", "Services/MooMed.Monitoring/"]
RUN dotnet restore "Services/MooMed.Monitoring/MooMed.Monitoring.csproj"
COPY . .
WORKDIR "/src/Services/MooMed.Monitoring"
RUN dotnet build "MooMed.Monitoring.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MooMed.Monitoring.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMed.Monitoring.dll"]
