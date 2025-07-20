# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Kopiraj csproj i restore
COPY *.sln .
COPY StayNest-API/*.csproj ./StayNest-API/
RUN dotnet restore

# Kopiraj ostatak i build
COPY . .
WORKDIR /app/StayNest-API
RUN dotnet publish -c Release -o /out

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Expose port 5000 and 5001 (HTTP/HTTPS)
EXPOSE 5000
EXPOSE 5001

# Pokreni aplikaciju
ENTRYPOINT ["dotnet", "StayNest-API.dll"]
