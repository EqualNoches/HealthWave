using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly HospitalCore _context;

        public FacturaService(HospitalCore context)
        {
            _context = context;
        }

        public async Task<int> AddFacturaAsync(FacturaDto facturaDto)
        {
            var factura = Factura.FromDto(facturaDto);
            _context.Facturas.Add(factura);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateFacturaAsync(FacturaDto facturaDto)
        {
            var factura = await _context.Facturas.FindAsync(facturaDto.FacturaCodigo);
            if (factura == null) return 0;

            factura.MontoTotal = facturaDto.MontoTotal;
            factura.MontoSubtotal = facturaDto.MontoSubtotal;
            factura.Fecha = facturaDto.Fecha;
            factura.Rnc = facturaDto.Rnc;
            factura.CodigoMetodoDePago = facturaDto.CodigoMetodoDePago;
            factura.CodigoPaciente = facturaDto.CodigoPaciente;
            factura.Idingreso = facturaDto.Idingreso;
            factura.Idcuenta = facturaDto.Idcuenta;
            factura.ConsultaCodigo = facturaDto.ConsultaCodigo;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteFacturaAsync(string facturaCodigo)
        {
            var factura = await _context.Facturas.FindAsync(facturaCodigo);
            if (factura == null) return 0;

            _context.Facturas.Remove(factura);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FacturaDto>> GetFacturasAsync()
        {
            return await _context.Facturas
                .Select(f => FacturaDto.FromModel(f))
                .ToListAsync();
        }

        public async Task<int> AddFacturaServicioAsync(FacturaServicioDto facturaServicioDto)
        {
            var facturaServicio = FacturaServicio.FromDto(facturaServicioDto);
            _context.FacturaServicios.Add(facturaServicio);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteFacturaServicioAsync(string facturaCodigo, string servicioCodigo)
        {
            var facturaServicio = await _context.FacturaServicios
                .FirstOrDefaultAsync(fs => fs.FacturaCodigoServicio == facturaCodigo && fs.ServicioCodigo == servicioCodigo);
            if (facturaServicio == null) return 0;

            _context.FacturaServicios.Remove(facturaServicio);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FacturaServicioDto>> GetFacturaServiciosAsync(string facturaCodigo)
        {
            return await _context.FacturaServicios
                .Where(fs => fs.FacturaCodigoNavigation.FacturaCodigo == facturaCodigo)
                .Select(fs => FacturaServicioDto.FromModel(fs))
                .ToListAsync();
        }

        public async Task<int> AddFacturaProductoAsync(FacturaProductoDto facturaProductoDto)
        {
            var facturaProducto = FacturaProducto.FromDto(facturaProductoDto);
            _context.FacturaProductos.Add(facturaProducto);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteFacturaProductoAsync(string facturaCodigoProducto, int idProducto)
        {
            var facturaProducto = await _context.FacturaProductos
                .FirstOrDefaultAsync(fp => fp.FacturaCodigoProducto == facturaCodigoProducto && fp.Idproducto == idProducto);
            if (facturaProducto == null) return 0;

            _context.FacturaProductos.Remove(facturaProducto);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FacturaProductoDto>> GetFacturaProductosAsync(string facturaCodigoProducto)
        {
            return await _context.FacturaProductos
                .Where(fp => fp.FacturaCodigoProducto == facturaCodigoProducto)
                .Select(fp => FacturaProductoDto.FromModel(fp))
                .ToListAsync();
        }

        public async Task<int> AddFacturaMetodoPagoAsync(string facturaCodigo, int codigoMetodoDePago)
        {
            var factura = await _context.Facturas.FindAsync(facturaCodigo);
            if (factura == null) return 0;

            var metodoDePago = await _context.MetodosDePago.FindAsync(codigoMetodoDePago);
            if (metodoDePago == null) return 0;

            factura.CodigoMetodoDePago = codigoMetodoDePago;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteFacturaMetodoPagoAsync(string facturaCodigo, int codigoMetodoDePago)
        {
            var factura = await _context.Facturas.FindAsync(facturaCodigo);
            if (factura == null) return 0;

            if (factura.CodigoMetodoDePago == codigoMetodoDePago)
            {
                factura.CodigoMetodoDePago = null;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MetodoDePagoDto>> GetMetodoPagosAsync(string facturaCodigo)
        {
            var factura = await _context.Facturas
                .Include(f => f.CodigoMetodoDePagoNavigation)
                .FirstOrDefaultAsync(f => f.FacturaCodigo == facturaCodigo);

            if (factura == null || factura.CodigoMetodoDePagoNavigation == null)
            {
                return Enumerable.Empty<MetodoDePagoDto>();
            }

            return new List<MetodoDePagoDto>
            {
                MetodoDePagoDto.FromModel(factura.CodigoMetodoDePagoNavigation)
            };
        }
    }
}
