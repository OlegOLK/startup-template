#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY [ "/src/Startup.sln", "Startup.sln" ]

COPY ["/src/Web/Startup.Web/Startup.Web.csproj", "Web/Startup.Web/"]
COPY ["/src/Web/Startup.Web.UnitTestes/Startup.Web.UnitTestes.csproj", "Web/Startup.Web.UnitTestes/"]

COPY ["/src/docker-compose.dcproj", "docker-compose.dcproj"]

COPY ["/src/NuGet.config", "NuGet.config"]
COPY . .

RUN dotnet restore "Startup.sln"

WORKDIR /Web/Startup.Web
RUN dotnet publish --no-restore -c Release -o /app

FROM build as unittest
WORKDIR /Web/Startup.Web.UnitTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Startup.Web.dll"]