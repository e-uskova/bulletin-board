#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["bulletin-board.csproj", "."]
COPY ["src/BulletinBoard/Domain/BulletinBoard.Domain/BulletinBoard.Domain.csproj", "src/BulletinBoard/Domain/BulletinBoard.Domain/"]
COPY ["src/BulletinBoard/Hosts/BulletinBoard.Hosts.Api/BulletinBoard.Hosts.Api.csproj", "src/BulletinBoard/Hosts/BulletinBoard.Hosts.Api/"]
COPY ["src/BulletinBoard/Application/BulletinBoard.Application.AppServices/BulletinBoard.Application.AppServices.csproj", "src/BulletinBoard/Application/BulletinBoard.Application.AppServices/"]
COPY ["src/BulletinBoard/Contracts/BulletinBoard.Contracts/BulletinBoard.Contracts.csproj", "src/BulletinBoard/Contracts/BulletinBoard.Contracts/"]
COPY ["src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure.DataAccess/BulletinBoard.Infrastructure.DataAccess.csproj", "src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure.DataAccess/"]
COPY ["src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure/BulletinBoard.Infrastructure.csproj", "src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure/"]
COPY ["src/BulletinBoard/Clients/BulletinBoard.Clients/BulletinBoard.Clients.csproj", "src/BulletinBoard/Clients/BulletinBoard.Clients/"]
COPY ["src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure.ComponentRegistrar/BulletinBoard.Infrastructure.ComponentRegistrar.csproj", "src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure.ComponentRegistrar/"]
RUN dotnet restore "./bulletin-board.csproj"
COPY . .
WORKDIR "src/BulletinBoard/Hosts/BulletinBoard.Hosts.Api/"
RUN dotnet build "BulletinBoard.Hosts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BulletinBoard.Hosts.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BulletinBoard.Hosts.Api.dll"]