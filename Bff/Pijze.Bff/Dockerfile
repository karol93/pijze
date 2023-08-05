FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bff/Pijze.Bff/Pijze.Bff.csproj", "Bff/Pijze.Bff/"]
RUN dotnet restore "Bff/Pijze.Bff/Pijze.Bff.csproj"
COPY . .
WORKDIR "/src/Bff/Pijze.Bff"
RUN dotnet build "Pijze.Bff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pijze.Bff.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pijze.Bff.dll"]
