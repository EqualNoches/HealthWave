using HospitalCore_core.Models;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Services.Interfaces
{
    public interface IFacturaService
    {
        Task<int> AddFacturaAsync(FacturaDto factura);
        Task<int> UpdateFacturaAsync(FacturaDto factura);
        Task<int> DeleteFacturaAsync(string facturaCodigo);
        Task<IEnumerable<FacturaDto>> GetFacturasAsync();
        Task<int> AddFacturaServicioAsync(FacturaServicioDto facturaServicio);
        Task<int> DeleteFacturaServicioAsync(string facturaCodigo, string servicioCodigo);
        Task<IEnumerable<FacturaServicioDto>> GetFacturaServiciosAsync(string facturaCodigo);
        Task<int> AddFacturaProductoAsync(FacturaProductoDto facturaProducto);
        Task<int> DeleteFacturaProductoAsync(string facturaCodigo, int idProducto);
        Task<IEnumerable<FacturaProductoDto>> GetFacturaProductosAsync(string facturaCodigo);
        Task<int> AddFacturaMetodoPagoAsync(string facturaCodigo, int idMetodoPago);
        Task<int> DeleteFacturaMetodoPagoAsync(string facturaCodigo, int idMetodoPago);
        Task<IEnumerable<MetodoDePago>> GetMetodoPagosAsync(string facturaCodigo);
    }
}