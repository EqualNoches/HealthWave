
using HospitalCore_core.DTO;

namespace HospitalCore_core.Services.Interfaces
{
    public interface IAutorizacionService
    {
        Task<int> AddAutorizacion(AutorizacionDTO autorizacion, int? idIngreso, string? consultaCodigo, string? facturaCodigo, string? servicioCodigo, int? idProducto);
        Task<int> DeleteAutorizacionAsync(int id);
        Task<List<AutorizacionDTO>> GetAllAutorizaciones();
        Task<AutorizacionDTO?> GetAutorizacionById(int id);
        Task<int> UpdateAutorizacionAsync(AutorizacionDTO autorizacion);
    }
}