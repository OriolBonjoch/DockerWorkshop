using PokemonCore.Models;
using PokemonCore.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace PokemonCore.Repositories
{
    public class PokemonRepository
    {
        private readonly IMongoCollection<Pokemon> _pokemons;

        public PokemonRepository(PokemonDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _pokemons = database.GetCollection<Pokemon>(settings.PokemonCollectionName);
        }

        public List<Pokemon> Get() =>
            _pokemons.Find(pokemon => true).ToList();

        public Pokemon Get(string id) =>
            _pokemons.Find<Pokemon>(pokemon => pokemon.Id == id).FirstOrDefault();

        public Pokemon Create(Pokemon pokemon)
        {
            _pokemons.InsertOne(pokemon);
            return pokemon;
        }

        public void Update(string id, Pokemon pokemonIn) =>
            _pokemons.ReplaceOne(pokemon => pokemon.Id == id, pokemonIn);

        public void Remove(Pokemon pokemonIn) =>
            _pokemons.DeleteOne(pokemon => pokemon.Id == pokemonIn.Id);

        public void Remove(string id) =>
            _pokemons.DeleteOne(pokemon => pokemon.Id == id);
    }
}
