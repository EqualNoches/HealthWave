using HospitalCore_core.Models;
using HospitalCore_core.DTO;
    
namespace HospitalCore_core.Services.Interfaces
{
    public interface IConsultaService
    {
        Task<int> AddConsultaAsync(ConsultaDto consulta);
        Task<int> UpdateConsultaAsync(ConsultaDto consulta);
        Task<int> RemoveConsultaAsync(int consultaCodigo);
        Task<int> AddConsultaServicioAsync(int consultaCodigo, int servicioCodigo);
        Task<int> RemoveConsultaServicioAsync(int consultaCodigo, int servicioCodigo);
        Task<List<Servicio>> GetConsultaServiciosAsync(int consultaCodigo);
        Task<int> AddConsultaProductoAsync(int consultaCodigo, int idProducto, int cantidad);
        Task<int> RemoveConsultaProductoAsync(int consultaCodigo, int idProducto);  
        Task<List<ProductoDto>> GetConsultaProductosAsync(int consultaCodigo);
        Task<int> AddConsultaAfeccionAsync(int consultaCodigo, int idAfeccion);
        Task<int> RemoveConsultaAfeccionAsync(int consultaCodigo, uint idAfeccion);
        Task<List<Afeccion>> GetConsultaAfeccionesAsync(int consultaCodigo);
        Task<List<ConsultaDto>> GetConsultasAsync();
        }
}
