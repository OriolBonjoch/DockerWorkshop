using Microsoft.AspNetCore.Mvc;
using PokemonCore.Models;
using System.Collections.Generic;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> Get()
        {
            return Ok();
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
