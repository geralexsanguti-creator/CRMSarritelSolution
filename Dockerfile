# ================== BUILD STAGE ==================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto (optimización de cache)
COPY CRMSarritelApi/CRMSarritelApi.csproj ./CRMSarritelApi/
COPY CRMSarritel.Shared/CRMSarritel.Shared.csproj ./CRMSarritel.Shared/

# Restaurar dependencias
WORKDIR /src/CRMSarritelApi
RUN dotnet restore ./CRMSarritelApi.csproj

# Copiar todo el código fuente
COPY . ./

# Limpiar cualquier archivo de build previo que pueda causar conflicto
RUN rm -rf ./obj/Release/net10.0/ ./bin/Release/net10.0/

# Publicar la API
RUN dotnet publish ./CRMSarritelApi.csproj -c Release -o /app/publish --no-restore

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