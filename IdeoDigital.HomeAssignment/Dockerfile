#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IdeoDigital.HomeAssignment/IdeoDigital.HomeAssignment.csproj", "IdeoDigital.HomeAssignment/"]
COPY ["IdeoDigital.Entities/IdeoDigital.Entities.csproj", "IdeoDigital.Entities/"]
COPY ["IdeoDigital.Repository/IdeoDigital.Repository.csproj", "IdeoDigital.Repository/"]
COPY ["IdeoDigital.Contracts/IdeoDigital.Contracts.csproj", "IdeoDigital.Contracts/"]
RUN dotnet restore "IdeoDigital.HomeAssignment/IdeoDigital.HomeAssignment.csproj"
COPY . .
WORKDIR "/src/IdeoDigital.HomeAssignment"
RUN dotnet build "IdeoDigital.HomeAssignment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdeoDigital.HomeAssignment.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdeoDigital.HomeAssignment.dll"]