using PokemonCore.Models;
using System.Collections.Generic;

namespace PokemonCore.Repositories
{
    public interface IPokemonRepository
    {
        IEnumerable<Pokemon> Get();

        Pokemon Get(string id);

        Pokemon Create(Pokemon pokemon);

        void Update(string id, Pokemon pokemonIn);

        void Remove(Pokemon pokemonIn);

        void Remove(string id);
    }
}
