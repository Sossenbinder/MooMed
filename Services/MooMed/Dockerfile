FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

ENV MooMed_IV "mN/xDqijx3EVb69j98OdNueyrkuqjIHuVWCikALjLi5umlg4iS713N6ZKoXcsL+xkmIlEr3DXoDWGXO/LuvS0zs11f9yNbOpN7et0OU3xIH/T3suy4vPN3YXO86UkFCISEU/ntQ9CO+xkPgTGlY/iTGnEDcApVwOpFGFe33PpXWyPVW3VBU4yg3fH4ZRlAe93EPa6tLrXs73pBmpXsFCxHy03+rWTqIX/Uz6MGhoe+UIgun0LoHYaanG8LRswoZgCWPNGtGxwwrG0bcSREJThuDRWYWc6Rb7hn/MbFnfekFHdUWrIetYHTDt7iRXt/pj0bluclEGUNoGvLxGy7Sspg=="
ENV MooMed_Key "izyTzc49vAfJ4x5wZSq+EZBhGBtLlAQiCcFnRQlMhV6+3fJT1z35m4ITJ3/XE2YX+oNu9Z+UtVCGXN64+Izm+EAzZZiPk+cZ4OQ0wXQ2RoTnHsQODmpdl3oGltYOA6AzGvlVwQ6N4c6zZ4ikHKWqcn0gu72Adg259YpiJdcTwAaO2tO1C7Z6ykwYk2p2PPvG8GNmnevXu2YYiiNhswP35R2/CrK2YcWpxNhkAydsvLG8YTFDCQjsO+D527nCVu33TDKpbE1xymeZadz4jmPqAnpowUyjzLotjwPGncfmv1bHKEpjCckb7RjEvc6Y4gr0K51WW6mRV7Ms0ks4abcJFQ=="
ENV MooMed_Cache_BaseTtlInSeconds 3600

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["MooMed/MooMed.csproj", "MooMed/"]

RUN dotnet restore "MooMed/MooMed.csproj"
COPY . .
WORKDIR "/src/MooMed"
RUN dotnet build "MooMed.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MooMed.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMed.dll"]