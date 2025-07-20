# -----------------------------
# STAGE 1: Build
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiraj samo bitne fajlove
COPY . . 
RUN dotnet restore "StayNest_API/StayNest_API.csproj"

# Publish aplikaciju
RUN dotnet publish "StayNest_API/StayNest_API.csproj" -c Release -o /app/publish

# -----------------------------
# STAGE 2: Runtime
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Railway koristi port 8080
ENV ASPNETCORE_URLS=http://+:8080

# Kopiraj aplikaciju iz prethodnog stage-a
COPY --from=build /app/publish .

# Pokreni aplikaciju
ENTRYPOINT ["dotnet", "StayNest_API.dll"]
