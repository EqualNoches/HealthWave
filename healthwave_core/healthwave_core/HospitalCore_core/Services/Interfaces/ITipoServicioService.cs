using HospitalCore_core.DTO;

namespace HospitalCore_core.Services.Interfaces
{
    public interface ITipoServicioService
    {
        Task<int> AddTipoServicioAsync(TipoServicioDto tipoServicioDto);
        Task<List<TipoServicioDto>> GetTipoServiciosAsync();
        Task<int> UpdateTipoServicioAsync(TipoServicioDto tipoServicioDto);
        Task<int> DeleteTipoServicioAsync(uint idTipoServicio);
    }
}