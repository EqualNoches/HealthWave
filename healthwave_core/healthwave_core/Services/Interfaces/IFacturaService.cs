using HospitalCore_core.Models;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Services.Interfaces
{
    public interface IFacturaService
    {
        Task<int> AddFacturaAsync(FacturaDto  facturaDto);
        Task<int> UpdateFacturaAsync(FacturaDto facturaDto);
        Task<int> DeleteFacturaAsync(string FacturaCodigo);
        Task<IEnumerable<FacturaDto>> GetFacturasAsync();
        Task<int> AddFacturaServicioAsync(FacturaServicioDto facturaServicioDto);
        Task<int> DeleteFacturaServicioAsync(string FacturaCodigo, string ServicioCodigo);
        Task<IEnumerable<FacturaServicioDto>> GetFacturaServiciosAsync(string FacturaCodigo);
        Task<int> AddFacturaProductoAsync(FacturaProductoDto facturaProductoDto);
        Task<int> DeleteFacturaProductoAsync(string FacturaCodigo, int Idproducto);
        Task<IEnumerable<FacturaProductoDto>> GetFacturaProductosAsync(string FacturaCodigo);
        Task<int> AddFacturaMetodoPagoAsync(string FacturaCodigo, int CodigoMetodoDePago);
        Task<int> DeleteFacturaMetodoPagoAsync(string FacturaCodigo, int CodigoMetodoDePago);
        Task<IEnumerable<MetodoDePago>> GetMetodoPagosAsync(string FacturaCodigo);
    }
}