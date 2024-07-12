using WebApiHealthWave.Data;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface ISalaService
    {
        Task<int> CreateSalaAsync(SalaDto salaDto);
        Task<List<SalaDto>> GetSalasAsync();
        Task<int> UpdateSalaEstadoAsync(uint numSala);
        Task<int> DeleteSalaAsync(uint numSala);
    }
}