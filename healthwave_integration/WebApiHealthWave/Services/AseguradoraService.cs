using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiHealthWave.Services
{
    public class AseguradoraService : IAseguradoraService
    {
        private readonly HttpClient _httpClient;

        public AseguradoraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Aseguradora>> GetAllAseguradoras()
        {
            var response = await _httpClient.GetAsync("aseguradoras");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Aseguradora>>(responseContent) ?? new List<Aseguradora>();
        }

        public async Task<Aseguradora?> GetAseguradoraById(uint id)
        {
            var response = await _httpClient.GetAsync($"aseguradoras/{id}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Aseguradora>(responseContent);
        }

        public async Task<int> UpdateAseguradora(Aseguradora aseguradora)
        {
            var content = new StringContent(JsonSerializer.Serialize(aseguradora), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"aseguradoras/{aseguradora.IDAseguradora}", content);
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<int> CreateAseguradora(Aseguradora aseguradora)
        {
            var content = new StringContent(JsonSerializer.Serialize(aseguradora), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("aseguradoras", content);
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<int> DeleteAseguradora(uint id)
        {
            var response = await _httpClient.DeleteAsync($"aseguradoras/{id}");
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }
    }
}

