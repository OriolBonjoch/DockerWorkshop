using PokemonWeb.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonWeb.Services
{
    public class PokemonsApiService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonDefaultOptions;

        public PokemonsApiService(HttpClient client)
        {
            _client = client;
            _jsonDefaultOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<Pokemon>> FetchAsync()
        {
            var response = await this._client.GetAsync("/api/pokemon");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Pokemon>>(responseStream, _jsonDefaultOptions);
        }

        public async Task<Pokemon> TrainAsync(string id)
        {
            var response = await this._client.PutAsync($"/api/pokemon/train/{id}", null);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Pokemon>(responseStream, _jsonDefaultOptions);
        }

        public async Task<Pokemon> CatchAsync(string id)
        {
            var response = await this._client.PutAsync($"/api/pokemon/catch/{id}", null);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Pokemon>(responseStream, _jsonDefaultOptions);
        }
    }
}