using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiHealthWave.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly HttpClient _httpClient;

        public AutorizacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CreateAutorizacionAsync(Autorizacion autorizacion, Aseguradora aseguradora)
        {
            var data = new
            {
                autorizacion,
                aseguradora
            };
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("autorizaciones", content);
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<Autorizacion> GetAutorizacionByIdAsync(uint id)
        {
            var response = await _httpClient.GetAsync($"autorizaciones/{id}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Autorizacion>(responseContent);
        }

        public async Task<int> UpdateAutorizacionAsync(Autorizacion autorizacion, Aseguradora aseguradora)
        {
            var data = new
            {
                autorizacion,
                aseguradora
            };
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"autorizaciones/{autorizacion.IDAutorizacion}", content);
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<int> DeleteAutorizacionAsync(uint id)
        {
            var response = await _httpClient.DeleteAsync($"autorizaciones/{id}");
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<List<Autorizacion>> GetAllAutorizacionesAsync()
        {
            var response = await _httpClient.GetAsync("autorizaciones");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Autorizacion>>(responseContent) ?? new List<Autorizacion>();
        }
    }
}
