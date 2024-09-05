FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# restore
COPY ["API/API.csproj", "API/"]
RUN dotnet restore "API/API.csproj"

# build
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

# Stage 3: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# ENV ASPNETCORE_HTTP_PORTS=5001
# EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]

