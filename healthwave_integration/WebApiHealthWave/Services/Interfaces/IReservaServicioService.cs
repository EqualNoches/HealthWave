using WebApiHealthWave.Data;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface IReservaServicio
    {
        Task<List<ReservaServicioDto>> GetReservaServiciosListAsync();
        Task<int> AddReservaServicioAsync(ReservaServicioDto reservaServicioDto);
        Task<int> UpdateReservaServicioAsync(ReservaServicioDto reservaServicioDto);
        Task<int> ToggleEstadoReservaServicioAsync(int idReserva);
        Task<int> DeleteReservaServicioAsync(int idReserva);
    }
}
