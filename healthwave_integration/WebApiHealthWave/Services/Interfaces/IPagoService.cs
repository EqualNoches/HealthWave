using WebApiHealthWave.Models;
using WebApiHealthWave.Data;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface IPagoService
    {
        Task<IEnumerable<Pago>> GetPagosAsync();
        Task<Pago> GetPagoByIdAsync(uint idPago);
        Task<int> CreatePagoAsync(Pago pago);
        Task<int> UpdatePagoAsync(Pago pago);
        Task<int> DeletePagoAsync(uint idPago);
    }
}
