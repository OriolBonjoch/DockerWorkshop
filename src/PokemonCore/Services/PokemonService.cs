using PokemonCore.Models;
using PokemonCore.Repositories;

namespace PokemonCore.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _repository;

        public PokemonService(IPokemonRepository repository)
        {
            _repository = repository;
        }

        public Pokemon TrainPokemon(string id)
        {
            var pokemon = _repository.Get(id);
            pokemon.Experience += 20;
            if (pokemon.Experience > 200)
            {
                pokemon.Fainted = true;
            }

            return pokemon;
        }

        public Pokemon CatchPokemon(string id)
        {
            var pokemon = _repository.Get(id);
            if (pokemon.Catched)
            {
                return pokemon;
            }

            pokemon.Catched = true;
            return pokemon;
        }
    }
}
