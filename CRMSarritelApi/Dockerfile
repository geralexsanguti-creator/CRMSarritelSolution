# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy shared project
COPY CRMSarritel.Shared/CRMSarritel.Shared.csproj CRMSarritel.Shared/
RUN dotnet restore CRMSarritel.Shared/CRMSarritel.Shared.csproj

# Copy API project
COPY CRMSarritelApi/CRMSarritelApi.csproj CRMSarritelApi/
RUN dotnet restore CRMSarritelApi/CRMSarritelApi.csproj
COPY CRMSarritelApi/ CRMSarritelApi/

# Build
WORKDIR /src/CRMSarritelApi
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Install timezone data
RUN apt-get update && apt-get install -y tzdata && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# Expose ports
EXPOSE 5000 5001

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CRMSarritelApi.dll"]