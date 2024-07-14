using WebApiHealthWave.Data;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface IConsultaService
    {
        Task<int> AddConsultaAsync(ConsultaDto consulta);
        Task<int> UpdateConsultaAsync(ConsultaDto consulta);
        Task<int> RemoveConsultaAsync(string consultaCodigo);
        Task<int> AddConsultaServicioAsync(string consultaCodigo, string servicioCodigo);
        Task<int> RemoveConsultaServicioAsync(string consultaCodigo, string servicioCodigo);
        Task<List<Servicio>> GetConsultaServiciosAsync(string consultaCodigo);
        Task<int> AddConsultaProductoAsync(string consultaCodigo, uint idProducto, int cantidad);
        Task<int> RemoveConsultaProductoAsync(string consultaCodigo, uint idProducto, int cantidad);
        Task<List<ProductoDto>> GetConsultaProductosAsync(string consultaCodigo);
        Task<int> AddConsultaAfeccionAsync(string consultaCodigo, uint idAfeccion);
        Task<int> RemoveConsultaAfeccionAsync(string consultaCodigo, uint idAfeccion);
        Task<List<Afeccion>> GetConsultaAfeccionesAsync(string consultaCodigo);
        Task<List<ConsultaDto>> GetConsultasAsync();
    }
}
