#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY [ "Startup.sln", "Startup.sln" ]

COPY ["Web/Startup.Web/Startup.Web.csproj", "Web/Startup.Web/"]
COPY ["Web/Startup.Web.UnitTestes/Startup.Web.UnitTestes.csproj", "Web/Startup.Web.UnitTestes/"]

RUN dotnet restore "Startup.sln"

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