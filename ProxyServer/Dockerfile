FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7176
EXPOSE 7177

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProxyServer/ProxyServer.csproj", "."]
RUN dotnet restore "./ProxyServer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ProxyServer/ProxyServer.csproj" -c $BUILD_CONFIGURATION -o /app/build
	
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProxyServer/ProxyServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProxyServer.dll"]
