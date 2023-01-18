#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["FluxoCaixa.Gateway.API/FluxoCaixa.Gateway.API.csproj", "FluxoCaixa.Gateway.API/"]
RUN dotnet restore "FluxoCaixa.Gateway.API/FluxoCaixa.Gateway.API.csproj"
COPY . .
WORKDIR "/src/FluxoCaixa.Gateway.API"
RUN dotnet build "FluxoCaixa.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.Gateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Gateway.API.dll"]