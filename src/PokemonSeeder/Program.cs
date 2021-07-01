using AutoMapper;
using Microsoft.Extensions.Configuration;
using PokeApiNet;
using PokemonCore.Settings;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PokemonSeeder
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console()
                .CreateLogger();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            var settings = configuration.GetSection("DatabaseSettings");
            var seeder = new PokemonSeeder(new PokemonDatabaseSettings()
            {
                PokemonCollectionName = settings[nameof(PokemonDatabaseSettings.PokemonCollectionName)],
                ConnectionString = settings[nameof(PokemonDatabaseSettings.ConnectionString)],
                DatabaseName = settings[nameof(PokemonDatabaseSettings.DatabaseName)]
            });

            var limit = GetLimit(args, 15);
            var offset = GetRandomOffset(args, limit);
            var mapper = CreateMapper();

            using var pokemonClient = new PokeApiClient();
            var pokemons = await pokemonClient.GetNamedResourcePageAsync<Pokemon>(limit, offset);
            Log.Information("Fetched {0} out of {1} pokemons", pokemons.Results.Count, pokemons.Count);

            foreach (var pokemonRef in pokemons.Results)
            {
                var pokemon = await pokemonClient.GetResourceAsync<Pokemon>(pokemonRef.Name);
                var newPokemon = mapper.Map<PokemonCore.Models.Pokemon>(pokemon);
                await seeder.Add(newPokemon);
            }
        }

        static int GetLimit(string[] args, int defaultLimit)
        {
            if (args.Length > 0 && int.TryParse(args[0], out var limit))
            {
                return limit;
            }

            return defaultLimit;
        }

        static int GetRandomOffset(string[] args, int limit)
        {
            if (args.Length > 1 && int.TryParse(args[0], out var page))
            {
                return page;
            }
            
            var maxPages = (int)Math.Floor(1100f / limit);
            var rnd = new Random();
            return rnd.Next(0, maxPages);
        }

        static IMapper CreateMapper()
        {
            var automapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Pokemon, PokemonCore.Models.Pokemon>()
                    .ForMember(dst => dst.Id, opt => opt.Ignore())
                    .ForMember(dst => dst.PokepediaId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Species, opt => opt.MapFrom(src => src.Species.Name))
                    .ForMember(dst => dst.Experience, opt => opt.MapFrom(src => src.BaseExperience));
            });

            return new Mapper(automapperConfiguration);
        }
    }
}
