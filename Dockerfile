#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src

COPY ["src/Startup.sln", "src/Startup.sln" ]
COPY ["src/Web/Startup.Web/Startup.Web.csproj", "src/Web/Startup.Web/"]
COPY ["src/Web/Startup.Web.UnitTestes/Startup.Web.UnitTestes.csproj", "src/Web/Startup.Web.UnitTestes/"]
COPY ["src/docker-compose.dcproj", "src/docker-compose.dcproj"]
COPY ["src/NuGet.config", "src/NuGet.config"]

RUN dotnet restore "src/Startup.sln"

COPY . .
WORKDIR /src/Web/Startup.Web
RUN dotnet publish --no-restore -c Release -o /app

FROM build as unittest
WORKDIR /src/Web/Startup.Web.UnitTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Startup.Web.dll"]