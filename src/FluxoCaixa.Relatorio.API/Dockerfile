#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["FluxoCaixa.Relatorio.API/FluxoCaixa.Relatorio.API.csproj", "FluxoCaixa.Relatorio.API/"]
RUN dotnet restore "FluxoCaixa.Relatorio.API/FluxoCaixa.Relatorio.API.csproj"
COPY . .
WORKDIR "/src/FluxoCaixa.Relatorio.API"
RUN dotnet build "FluxoCaixa.Relatorio.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.Relatorio.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Relatorio.API.dll"]