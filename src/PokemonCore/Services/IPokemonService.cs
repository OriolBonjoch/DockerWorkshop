using PokemonCore.Models;

namespace PokemonCore.Services
{
    public interface IPokemonService
    {
        Pokemon TrainPokemon(string id);

        Pokemon CatchPokemon(string id);
    }
}
