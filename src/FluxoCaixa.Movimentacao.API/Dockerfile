#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["FluxoCaixa.Movimentacao.API/FluxoCaixa.Movimentacao.API.csproj", "FluxoCaixa.Movimentacao.API/"]
RUN dotnet restore "FluxoCaixa.Movimentacao.API/FluxoCaixa.Movimentacao.API.csproj"
COPY . .
WORKDIR "/src/FluxoCaixa.Movimentacao.API"
RUN dotnet build "FluxoCaixa.Movimentacao.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.Movimentacao.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Movimentacao.API.dll"]