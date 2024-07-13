using WebApiHealthWave.Models;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface IAseguradoraService
    {
        public Task<List<Aseguradora>> GetAllAseguradoras();
        public Task<Aseguradora?> GetAseguradoraById(uint id);
        public Task<int> UpdateAseguradora(Aseguradora aseguradora);
        public Task<int> CreateAseguradora(Aseguradora aseguradora);
        public Task<int> DeleteAseguradora(uint id);
    }
}
