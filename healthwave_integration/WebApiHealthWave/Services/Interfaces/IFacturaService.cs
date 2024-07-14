using WebApiHealthWave.Data;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Services.Interfaces
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
        Task<int> DeleteFacturaProductoAsync(string facturaCodigo, uint idProducto);
        Task<IEnumerable<FacturaProductoDto>> GetFacturaProductosAsync(string facturaCodigo);
        Task<int> AddFacturaMetodoPagoAsync(string facturaCodigo, uint idMetodoPago);
        Task<int> DeleteFacturaMetodoPagoAsync(string facturaCodigo, uint idMetodoPago);
        Task<IEnumerable<MetodoDePago>> GetMetodoPagosAsync(string facturaCodigo);
    }
}

