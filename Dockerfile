# ================== RUNTIME STAGE ==================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Instalar herramientas útiles (opcional)
RUN apt-get update && apt-get install -y curl tzdata && rm -rf /var/lib/apt/lists/*

# Crear directorio para uploads
RUN mkdir -p /app/wwwroot/uploads

# Copiar los archivos publicados directamente
COPY publish/ .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CRMSarritelApi.dll"]