using Microsoft.AspNetCore.Mvc;
using PokemonCore.Models;
using PokemonCore.Repositories;
using PokemonCore.Services;
using System.Collections.Generic;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonService pokemonService, IPokemonRepository pokemonRepository)
        {
            _pokemonService = pokemonService;
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> Get()
        {
            var pokemons = _pokemonRepository.Get();
            return Ok(pokemons);
        }

        [HttpPut("/train/{id}")]
        public ActionResult<Pokemon> Train(string id)
        {
            return Ok();
        }

        [HttpPut("/catch/{id}")]
        public ActionResult<Pokemon> Catch(string id)
        {
            return Ok();
        }
    }
}
