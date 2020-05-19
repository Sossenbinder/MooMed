#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV MooMed_Cache_BaseTtlInSeconds=3600
ENV MooMed_IV="IfY8XLg/SiXOXR0vWMBaZb6SPaaFDD3FdPnlgZ6AX4FOE9mqwQfprwYReJ2nEEvJokuPI+sC0mmGDeqpR+HOFQRzycY2wOLpAtSnVqLxrCizxqS7hnJ2naHUDeiXOwwR0A+R0jJbBu4eEDaRMaImj2r1n+HBWZz25/3klNYVsFfFxUcjloHGX1P3snELPjyGrqfGYvpdKqOoAfI3nqCkN2VnbAVYW2pXCvqL1KcvS8L6dbaGbxlE39JjdPIyLQ5ZRilQs4CZQRyhjxWl2BL+AKc3ziaVMFzdBQPAwTSiPwN9VSIWGKBPKi6guHwn2KgnFVwaIxWArXTk/bjLCmobFm4lOHU0qP+aNJR6GzQwBGK62/Q8WLBFYQ+9PYqiMzPhZmDrzHjeXZD+ECNdk5LK+8y3c0KmTUG1gGLjEXkuQ9r9D8am/qCsqhc50H+2owCnJVmZs5QTailkKyrtQjSEh34y5fHGV3RaAYmMC8F8vR5l7gexGs0aChQJMxyYdFZ8Kwhf/zuyOzD/vQ84dAmFf0Xya36mAIoHOiaJlpg2avqe+u3L+P98MBeQzXX/LAL10QxnMaIBas3iJOuebb1opADkeCxGAmPuj/oWIGbbJfv320UEmniKez6HNSklZBGYUDRaCowFH1tXZ01idE863gZ0f/mhhFhB9+WWtZ5s8uw="
ENV MooMed_Key="M5ouyc/+l1kTcpowC2APV2pWadQgUxEkV0KT+VmElsdNttvgJ1vVX/7tGAQA+5sIuFLHLnesoagP/UoyD9moZYxIWD2q/RR2AT4+0uZ8xD4dc3F9Tr5F3+B14lfDfLUIp2D9ewvDLti9I/zWoXQHtPlRAs3k2QDQTWmHRbN1uROG0p0KuZBhy5ilyadeOTP/Jl4EQl318y2NKO2E1/5Dv+M8IIb8daiM/g5M0HMJ/4Ff2p8EBVSjqAENwmysZF9KbhtMniU3MevRxXm/9WetTCVMpsfAq5bFRD1yJ24FCAt/zH+RMIt0m3GZMjuKfqH8Qh3FXFqhSgYcRjzPL9HmeP8RLdsG20nm8VRmfxLYg3/4qoUxCiEyCoVw3EQ9utWkAGxF34Cxq8AwXfavB8Oh8HtHkDZ/HKMQ9rUmJOn0YZ3EhVWP3x8h2lBAIZLLKhv6K29wA1JXcyH9qm2IrlSrY7teNI1yRdJXjQGmr6NsOD1CTY4/FrsUEq8RUkh9mzFgkOqF3R5hL18fXrFwhdXmbD5FYwxGdm9AhgQJZNlFPOi6WObiwFkq6lHERFDg5KlsEKNTZ5K+uCwvCGwSRy35OHNdz5902mpaVciv07cU0Tvso4ayG0eRjJR9XEJm6LlgeJLivUROOVLjAJOECTWIvKUBSHV+vIYEe+T/zL9K3dg="
ENV MooMed_Logging_TableStorageConnectionString="DefaultEndpointsProtocol=https;AccountName=moomedlogging;AccountKey=EkZOplhXL0ogByoVww+9+YxstcSsdZ5/fxN4QdrGkONzWL47GNuZwfCGOlfaNJpHB/21hZIvQV75UrZt5seg4PQOH5+m1LfAZkKEPSvTHz0=;EndpointSuffix=core.windows.net"
ENV MooMed_Db_Finance="Server=tcp:moomeddbserver.database.windows.net,1433;Initial Catalog=Finance;Persist Security Info=False;User ID=moomedadmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

ENV AZURE_CLIENT_ID "82259ded-f445-4db4-80d4-1c07f13c50b7"
ENV AZURE_TENANT_ID "ff009f2c-d651-472e-b73e-e0f894c6011e"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/MooMed.FinanceService/MooMed.FinanceService.csproj", "Services/MooMed.FinanceService/"]
COPY ["Common/MooMed.Caching/MooMed.Caching.csproj", "Common/MooMed.Caching/"]
COPY ["Common/MooMed.Core/MooMed.Core.csproj", "Common/MooMed.Core/"]
COPY ["Common/MooMed.Common.Definitions/MooMed.Common.Definitions.csproj", "Common/MooMed.Common.Definitions/"]
COPY ["Common/MooMed.Grpc/MooMed.Grpc.csproj", "Common/MooMed.Grpc/"]
COPY ["Common/MooMed.Azure/MooMed.Azure.csproj", "Common/MooMed.Azure/"]
COPY ["Common/MooMed.IPC/MooMed.IPC.csproj", "Common/MooMed.IPC/"]
COPY ["Common/MooMed.AspNetCore/MooMed.AspNetCore.csproj", "Common/MooMed.AspNetCore/"]
COPY ["Common/MooMed.Common.ServiceBase/MooMed.Common.ServiceBase.csproj", "Common/MooMed.Common.ServiceBase/"]
COPY ["../MooMed.Module.Finance/MooMed.Module.Finance.csproj", "../MooMed.Module.Finance/"]
RUN dotnet restore "Services/MooMed.FinanceService/MooMed.FinanceService.csproj"
COPY . .
WORKDIR "/src/Services/MooMed.FinanceService"
RUN dotnet build "MooMed.FinanceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MooMed.FinanceService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMed.FinanceService.dll"]
