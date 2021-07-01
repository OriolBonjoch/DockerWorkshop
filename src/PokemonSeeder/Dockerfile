#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PokemonSeeder/PokemonSeeder.csproj", "src/PokemonSeeder/"]
COPY ["src/PokemonCore/PokemonCore.csproj", "src/PokemonCore/"]
RUN dotnet restore "src/PokemonSeeder/PokemonSeeder.csproj"
COPY . .
WORKDIR "/src/src/PokemonSeeder"
RUN dotnet build "PokemonSeeder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonSeeder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN apt update && apt install -y curl
COPY --from=publish /app/publish .
RUN chmod +x /app/seed-and-wait.sh
HEALTHCHECK CMD test -f "/tmp/healthy" || exit 1
ENTRYPOINT ["dotnet", "PokemonSeeder.dll"]
