FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Api/src/Pijze.Api/Pijze.Api.csproj", "Api/src/Pijze.Api/"]
COPY ["Api/src/Pijze.Infrastructure/Pijze.Infrastructure.csproj", "Api/src/Pijze.Infrastructure/"]
COPY ["Api/src/Pijze.Application/Pijze.Application.csproj", "Api/src/Pijze.Application/"]
COPY ["Api/src/Pijze.Domain/Pijze.Domain.csproj", "Api/src/Pijze.Domain/"]
RUN dotnet restore "Api/src/Pijze.Api/Pijze.Api.csproj"
COPY . .
WORKDIR "/src/Api/src/Pijze.Api"
RUN dotnet build "Pijze.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pijze.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
    
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pijze.Api.dll"]
