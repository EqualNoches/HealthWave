using WebApiHealthWave.Data;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface IMetodoPagoService
    {
        Task<int> AddMetodoPagoAsync(MetodoDePagoDto metodoPago);
        Task<List<MetodoDePagoDto>> GetMetodosPagoAsync();
        Task<int> UpdateMetodoPagoAsync(MetodoDePagoDto metodoPago);
        Task<int> DeleteMetodoPagoAsync(uint idMetodoPago);
    }
}
