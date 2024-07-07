
using HospitalCore_core.DTO;
using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces
{
    public interface IIngresoService
    {
        Task<int> AddIngresoAsync(IngresoDto ingresoDto);
        Task<int> UpdateIngresoAsync(IngresoDto ingresoDto);
        Task<int> DeleteIngresoAsync(int idIngreso);
        Task<List<IngresoDto>> GetIngresosAsync();
        Task<IngresoDto?> GetIngresoByIdAsync(int idIngreso);
        Task<int> AddIngresoAfeccionAsync(int idIngreso, int idAfeccion);
        Task<int> RemoveIngresoAfeccionAsync(int idIngreso, int idAfeccion);
        Task<List<Afeccion>> GetIngresoAfeccionesAsync(int idIngreso);
    }
}