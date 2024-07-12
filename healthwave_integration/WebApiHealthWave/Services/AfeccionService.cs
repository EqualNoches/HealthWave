using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiHealthWave.Services
{
    public class AfeccionService : IAfeccionService
    {
        private readonly HttpClient _httpClient;

        public AfeccionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CreateAfeccionAsync(Afeccion afeccion)
        {
            var content = new StringContent(JsonSerializer.Serialize(afeccion), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("afecciones", content);
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<int> UpdateAfeccionAsync(Afeccion afeccion)
        {
            var content = new StringContent(JsonSerializer.Serialize(afeccion), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"afecciones/{afeccion.IDAfeccion}", content);
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<int> DeleteAfeccionAsync(uint id)
        {
            var response = await _httpClient.DeleteAsync($"afecciones/{id}");
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<IEnumerable<Afeccion>> GetAfeccionesAsync()
        {
            var response = await _httpClient.GetAsync("afecciones");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Afeccion>>(responseContent) ?? new List<Afeccion>();
        }

        public async Task<Afeccion?> GetAfeccionByIdAsync(uint id)
        {
            var response = await _httpClient.GetAsync($"afecciones/{id}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Afeccion>(responseContent);
        }
    }
}

