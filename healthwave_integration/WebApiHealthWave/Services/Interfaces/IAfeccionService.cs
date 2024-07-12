using WebApiHealthWave.Models;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface IAfeccionService
    {
        Task<int> CreateAfeccionAsync(Afeccion afeccion);
        Task<int> UpdateAfeccionAsync(Afeccion afeccion);
        Task<int> DeleteAfeccionAsync(uint id);
        Task<IEnumerable<Afeccion>> GetAfeccionesAsync();
        Task<Afeccion?> GetAfeccionByIdAsync(uint id);
    }
}
