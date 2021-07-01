using MongoDB.Driver;
using PokemonCore.Models;
using PokemonCore.Settings;
using Serilog;
using System.Threading.Tasks;

namespace PokemonSeeder
{
    public class PokemonSeeder
    {
        private readonly IMongoCollection<Pokemon> _pokemons;

        public PokemonSeeder(PokemonDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pokemons = database.GetCollection<Pokemon>(settings.PokemonCollectionName);
        }

        public async Task Add(Pokemon pokemon)
        {
            var existing = await _pokemons.Find(p => p.PokepediaId == pokemon.PokepediaId).FirstOrDefaultAsync();
            if (existing != null)
            {
                return;
            }

            Log.Information("A wild {0} has been born", pokemon.Species);
            await _pokemons.InsertOneAsync(pokemon);
        }
    }
}
