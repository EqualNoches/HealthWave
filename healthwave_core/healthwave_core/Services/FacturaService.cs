using HospitalCore_core.Models;
using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;
using HospitalCore_core.Context;


namespace HospitalCore_core.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly HospitalCore _dbContext;
        private readonly LogManager<FacturaService> _logManager = new();

        public FacturaService(HospitalCore dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddFacturaAsync(FacturaDto facturaDto)
        {
            try
            {
                var factura = Factura.FromDto(facturaDto);
                _dbContext.Facturas.Add(factura);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while creating a Factura.", ex);
                throw;
            }
        }

        public async Task<int> UpdateFacturaAsync(FacturaDto facturaDto)
        {
            try
            {
                var factura = await _dbContext.Facturas.FindAsync(facturaDto.FacturaCodigo);
                if (factura == null)
                    throw new KeyNotFoundException("Factura not found");

                factura.MontoTotal = facturaDto.MontoTotal;
                factura.MontoSubtotal = facturaDto.MontoSubtotal;
                factura.Fecha = facturaDto.Fecha;
                factura.Rnc = facturaDto.Rnc;
                factura.CodigoMetodoDePago = facturaDto.CodigoMetodoDePago;
                factura.CodigoPaciente = facturaDto.CodigoPaciente;
                factura.Idingreso = facturaDto.Idingreso;
                factura.Idcuenta = facturaDto.Idcuenta;
                factura.ConsultaCodigo = facturaDto.ConsultaCodigo;

                _dbContext.Facturas.Update(factura);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while updating the Factura.", ex);
                throw;
            }
        }

        public async Task<int> DeleteFacturaAsync(string FacturaCodigo)
        {
            try
            {
                var factura = await _dbContext.Facturas.FindAsync(FacturaCodigo);
                if (factura == null)
                    throw new KeyNotFoundException("Factura not found");

                _dbContext.Facturas.Remove(factura);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while deleting the Factura.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<FacturaDto>> GetFacturasAsync()
        {
            try
            {
                var facturas = await _dbContext.Facturas.ToListAsync();
                return facturas.Select(factura => FacturaDto.FromModel(factura));
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while retrieving Facturas.", ex);
                throw;
            }
        }

        public async Task<int> AddFacturaServicioAsync(FacturaServicioDto facturaServicioDto)
        {
            try
            {
                var facturaServicio = FacturaServicio.FromDto(facturaServicioDto);
                _dbContext.FacturaServicios.Add(facturaServicio);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while creating a FacturaServicio.", ex);
                throw;
            }
        }

        public async Task<int> DeleteFacturaServicioAsync(string FacturaCodigo, string ServicioCodigo)
        {
            try
            {
                var facturaServicio = await _dbContext.FacturaServicios
                    .FirstOrDefaultAsync(fs => fs.FacturaCodigo == FacturaCodigo && fs.ServicioCodigo == ServicioCodigo);
                if (facturaServicio == null)
                    throw new KeyNotFoundException("FacturaServicio not found");

                _dbContext.FacturaServicios.Remove(facturaServicio);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while deleting the FacturaServicio.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<FacturaServicioDto>> GetFacturaServiciosAsync(string FacturaCodigo)
        {
            try
            {
                var facturaServicios = await _dbContext.FacturaServicios
                    .Where(fs => fs.FacturaCodigo == FacturaCodigo)
                    .ToListAsync();
                return facturaServicios.Select(facturaServicio => FacturaServicioDto.FromModel(facturaServicio));
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while retrieving FacturaServicios.", ex);
                throw;
            }
        }

        public async Task<int> AddFacturaProductoAsync(FacturaProductoDto facturaProductoDto)
        {
            try
            {
                var facturaProducto = FacturaProducto.FromDto(facturaProductoDto);
                _dbContext.FacturaProductos.Add(facturaProducto);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while creating a FacturaProducto.", ex);
                throw;
            }
        }

        public async Task<int> DeleteFacturaProductoAsync(string FacturaCodigo, int Idproducto)
        {
            try
            {
                var facturaProducto = await _dbContext.FacturaProductos
                    .FirstOrDefaultAsync(fp => fp.FacturaCodigo == FacturaCodigo && fp.Idproducto == Idproducto);
                if (facturaProducto == null)
                    throw new KeyNotFoundException("FacturaProducto not found");

                _dbContext.FacturaProductos.Remove(facturaProducto);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while deleting the FacturaProducto.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<FacturaProductoDto>> GetFacturaProductosAsync(string FacturaCodigo)
        {
            try
            {
                var facturaProductos = await _dbContext.FacturaProductos
                    .Where(fp => fp.FacturaCodigo == FacturaCodigo)
                    .ToListAsync();
                return facturaProductos.Select(facturaProducto => FacturaProductoDto.FromModel(facturaProducto));
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while retrieving FacturaProductos.", ex);
                throw;
            }
        }

        public async Task<int> AddFacturaMetodoPagoAsync(string FacturaCodigo, int CodigoMetodoDePago)
        {
            try
            {
                var factura = await _dbContext.Facturas.FindAsync(FacturaCodigo);
                if (factura == null)
                    throw new KeyNotFoundException("Factura not found");

                factura.CodigoMetodoDePago = CodigoMetodoDePago;
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while adding MetodoDePago to Factura.", ex);
                throw;
            }
        }

        public async Task<int> DeleteFacturaMetodoPagoAsync(string FacturaCodigo, int CodigoMetodoDePago)
        {
            try
            {
                var factura = await _dbContext.Facturas.FindAsync(FacturaCodigo);
                if (factura == null)
                    throw new KeyNotFoundException("Factura not found");

                factura.CodigoMetodoDePago = null;
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while deleting MetodoDePago from Factura.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<MetodoDePago>> GetMetodoPagosAsync(string FacturaCodigo)
        {
            try
            {
                var factura = await _dbContext.Facturas
                    .Include(f => f.CodigoMetodoDePagoNavigation)
                    .FirstOrDefaultAsync(f => f.FacturaCodigo == FacturaCodigo);
                if (factura == null)
                    throw new KeyNotFoundException("Factura not found");

                return factura.CodigoMetodoDePagoNavigation != null ? new List<MetodoDePago> { factura.CodigoMetodoDePagoNavigation } : new List<MetodoDePago>();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while retrieving MetodoDePagos from Factura.", ex);
                throw;
            }
        }
    }
}
