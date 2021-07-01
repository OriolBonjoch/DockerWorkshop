using System.Collections.Generic;
using PokemonWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonWeb.Services;
using System.Threading.Tasks;
using System.Linq;

namespace PokemonWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly PokemonsApiService _apiService;

        public PokemonController(ILogger<PokemonController> logger, PokemonsApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> Get()
        {
            var pokemons = await _apiService.FetchAsync();
            _logger.LogDebug($"Fetched {pokemons.Count()} pokemons");
            return pokemons;
        }

        [HttpPut("train/{id}")]
        public async Task<IActionResult> Train(string id)
        {
            var pokemon = await _apiService.TrainAsync(id);
            return pokemon == null ? NotFound() : Ok(pokemon);
        }

        [HttpPut("catch/{id}")]
        public async Task<IActionResult> Catch(string id)
        {
            var pokemon = await _apiService.CatchAsync(id);
            return pokemon == null ? NotFound() : Ok(pokemon);
        }
    }
}
