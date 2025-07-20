# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiraj sve fajlove
COPY . .

# Restore nuget paketa
RUN dotnet restore "StayNest-API/StayNest-API.csproj"

# Build i publish
RUN dotnet publish "StayNest-API/StayNest-API.csproj" -c Release -o /app/publish

# Final stage (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "StayNest-API.dll"]