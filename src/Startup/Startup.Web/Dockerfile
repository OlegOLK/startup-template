#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Startup/Startup.Web.csproj", "Startup.Web/"]
COPY ["Startup/Startup.Web.UnitTests.csproj", "Startup.Web.UnitTests/"]
RUN dotnet restore "Startup.Web/Startup.Web.csproj"

COPY . .
WORKDIR "/src/Startup"
RUN dotnet build "Startup.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Startup.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Startup.Web.dll"]
