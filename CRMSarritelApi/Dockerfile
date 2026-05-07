# ================== BUILD STAGE ==================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto (optimización de cache)
COPY CRMSarritelApi.csproj ./
COPY ../CRMSarritel.Shared/CRMSarritel.Shared.csproj ../CRMSarritel.Shared/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código fuente
COPY . ./
COPY ../CRMSarritel.Shared ../CRMSarritel.Shared

# Publicar la API
RUN dotnet publish -c Release -o /app/publish --no-restore

# ================== RUNTIME STAGE ==================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Instalar herramientas útiles (opcional)
RUN apt-get update && apt-get install -y curl tzdata && rm -rf /var/lib/apt/lists/*

# Crear directorio para uploads
RUN mkdir -p /app/wwwroot/uploads

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CRMSarritelApi.dll"]