#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MooMed.Stateful.AccountValidationService/MooMed.Stateful.AccountValidationService.csproj", "MooMed.Stateful.AccountValidationService/"]
RUN dotnet restore "MooMed.Stateful.AccountValidationService/MooMed.Stateful.AccountValidationService.csproj"
COPY . .
WORKDIR "/src/MooMed.Stateful.AccountValidationService"
RUN dotnet build "MooMed.Stateful.AccountValidationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MooMed.Stateful.AccountValidationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMed.Stateful.AccountValidationService.dll"]