using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace healthwave_integracion.Data
    public class CoreDbContext : DbContext
    {
        // ... DbContext configuration
    }

    public class CoreRepository
    {
        private readonly CoreDbContext _dbContext;

        public CoreRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public class Consulta


    public class HospitalApiClient
    {
        private readonly HttpClient _httpClient;

        public HospitalApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5406/");
        }

        // Métodos CRUD para Paciente
        public async Task<int> CreatePacienteAsync(PacienteDto pacienteDto)
        {
            string json = JsonConvert.SerializeObject(pacienteDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/paciente", content);

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<List<PacienteDto>> GetPacientesAsync()
        {
            var response = await _httpClient.GetAsync("api/paciente");
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PacienteDto>>(jsonResponse);
            }
            return new List<PacienteDto>();
        }

        public async Task<int> UpdatePacienteAsync(PacienteDto pacienteDto)
        {
            string json = JsonConvert.SerializeObject(pacienteDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/paciente/{pacienteDto.IdPaciente}", content);

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeletePacienteAsync(uint idPaciente)
        {
            var response = await _httpClient.DeleteAsync($"api/paciente/{idPaciente}");

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        // Métodos CRUD para Doctor
        public async Task<int> CreateDoctorAsync(DoctorDto doctorDto)
        {
            string json = JsonConvert.SerializeObject(doctorDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/doctor", content);

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<List<DoctorDto>> GetDoctoresAsync()
        {
            var response = await _httpClient.GetAsync("api/doctor");
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DoctorDto>>(jsonResponse);
            }
            return new List<DoctorDto>();
        }

        public async Task<int> UpdateDoctorAsync(DoctorDto doctorDto)
        {
            string json = JsonConvert.SerializeObject(doctorDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/doctor/{doctorDto.IdDoctor}", content);

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteDoctorAsync(uint idDoctor)
        {
            var response = await _httpClient.DeleteAsync($"api/doctor/{idDoctor}");

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        // Métodos CRUD para Hospitalización
        public async Task<int> CreateHospitalizacionAsync(HospitalizacionDto hospitalizacionDto)
        {
            string json = JsonConvert.SerializeObject(hospitalizacionDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/hospitalizacion", content);

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<List<HospitalizacionDto>> GetHospitalizacionesAsync()
        {
            var response = await _httpClient.GetAsync("api/hospitalizacion");
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<HospitalizacionDto>>(jsonResponse);
            }
            return new List<HospitalizacionDto>();
        }

        public async Task<int> UpdateHospitalizacionAsync(HospitalizacionDto hospitalizacionDto)
        {
            string json = JsonConvert.SerializeObject(hospitalizacionDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/hospitalizacion/{hospitalizacionDto.IdHospitalizacion}", content);

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteHospitalizacionAsync(uint idHospitalizacion)
        {
            var response = await _httpClient.DeleteAsync($"api/hospitalizacion/{idHospitalizacion}");

            return response.IsSuccessStatusCode ? 1 : 0;
        }
    }
}
