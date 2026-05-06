# ================== BUILD STAGE ==================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de solución y proyectos (para mejor cache)
COPY *.sln ./
COPY CRMSarritel.Shared/CRMSarritel.Shared.csproj CRMSarritel.Shared/
COPY CRMSarritelApi/CRMSarritelApi.csproj CRMSarritelApi/
COPY CRMSarritelClient/CRMSarritelClient.csproj CRMSarritelClient/
COPY CRMSarritelFront/CRMSarritelFront.csproj CRMSarritelFront/

# Restore de toda la solución
RUN dotnet restore CRMSarritelSolution.sln

# Copiar el código fuente completo
COPY CRMSarritel.Shared/ CRMSarritel.Shared/
COPY CRMSarritelApi/ CRMSarritelApi/
COPY CRMSarritelClient/ CRMSarritelClient/
COPY CRMSarritelFront/ CRMSarritelFront/

# Publicar solo el proyecto Web API
WORKDIR /src/CRMSarritelApi
RUN dotnet publish -c Release -o /app/publish --no-restore

# ================== RUNTIME STAGE ==================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Instalar zona horaria (útil para Colombia u otros países)
RUN apt-get update && apt-get install -y tzdata && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CRMSarritelApi.dll"]