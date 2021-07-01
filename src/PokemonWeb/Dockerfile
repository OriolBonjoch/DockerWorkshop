#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:13.8.0-stretch as node
WORKDIR /app
COPY ["src/PokemonWeb/ClientApp/package.json", "./"]
RUN npm install
COPY src/PokemonWeb/ClientApp/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PokemonWeb/PokemonWeb.csproj", "src/PokemonWeb/"]
RUN dotnet restore "src/PokemonWeb/PokemonWeb.csproj"
COPY . .
WORKDIR "/src/src/PokemonWeb"
RUN dotnet build "PokemonWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app/ClientApp/build
RUN apt update && apt install -y curl
COPY --from=node /app/build/ .
WORKDIR /app
COPY --from=publish /app/publish .
HEALTHCHECK --interval=10s --retries=10 \
  CMD curl -f http://localhost/health || exit 1
ENTRYPOINT ["dotnet", "PokemonWeb.dll"]
