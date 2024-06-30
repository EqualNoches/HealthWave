using HospitalCore_core.Models;
using HospitalCore_core.DTO;
using HospitalCore_core.Context;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCore_core.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly HospitalCore _dbContext;

        public FacturaService(HospitalCore dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddFacturaAsync(FacturaDto factura)
        {
            var facturaEntity = new Factura
            {
                FacturaCodigo = factura.FacturaCodigo,
                MontoTotal = factura.MontoTotal,
                MontoSubtotal = factura.MontoSubtotal,
                Fecha = factura.Fecha,
                Rnc = factura.Rnc,
                CodigoMetodoDePago = factura.CodigoMetodoDePago,
                CodigoPaciente = factura.CodigoPaciente,
                Idingreso = factura.Idingreso,
                Idcuenta = factura.Idcuenta,
                ConsultaCodigo = factura.ConsultaCodigo
            };

            _dbContext.Facturas.Add(facturaEntity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateFacturaAsync(FacturaDto factura)
        {
            var facturaEntity = await _dbContext.Facturas.FindAsync(factura.FacturaCodigo);
            if (facturaEntity == null) return 0;

            facturaEntity.MontoTotal = factura.MontoTotal;
            facturaEntity.MontoSubtotal = factura.MontoSubtotal;
            facturaEntity.Fecha = factura.Fecha;
            facturaEntity.Rnc = factura.Rnc;
            facturaEntity.CodigoMetodoDePago = factura.CodigoMetodoDePago;
            facturaEntity.CodigoPaciente = factura.CodigoPaciente;
            facturaEntity.Idingreso = factura.Idingreso;
            facturaEntity.Idcuenta = factura.Idcuenta;
            facturaEntity.ConsultaCodigo = factura.ConsultaCodigo;

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFacturaAsync(string facturaCodigo)
        {
            var facturaEntity = await _dbContext.Facturas.FindAsync(facturaCodigo);
            if (facturaEntity == null) return 0;

            _dbContext.Facturas.Remove(facturaEntity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FacturaDto>> GetFacturasAsync()
        {
            var facturas = await _dbContext.Facturas.ToListAsync();
            var facturaDtos = new List<FacturaDto>();

            foreach (var factura in facturas)
            {
                facturaDtos.Add(new FacturaDto
                {
                    FacturaCodigo = factura.FacturaCodigo,
                    MontoTotal = factura.MontoTotal,
                    MontoSubtotal = factura.MontoSubtotal,
                    Fecha = factura.Fecha,
                    Rnc = factura.Rnc,
                    CodigoMetodoDePago = factura.CodigoMetodoDePago,
                    CodigoPaciente = factura.CodigoPaciente,
                    Idingreso = factura.Idingreso,
                    Idcuenta = factura.Idcuenta,
                    ConsultaCodigo = factura.ConsultaCodigo
                });
            }

            return facturaDtos;
        }

        public async Task<int> AddFacturaServicioAsync(FacturaServicioDto facturaServicio)
        {
            var facturaServicioEntity = new FacturaServicio
            {
                FacturaCodigo = facturaServicio.FacturaCodigo,
                Idproducto = facturaServicio.Idproducto,
                Idautorizacion = facturaServicio.Idautorizacion,
                Costo = facturaServicio.Costo,
                ServicioCodigo = facturaServicio.ServicioCodigo
            };

            _dbContext.FacturaServicios.Add(facturaServicioEntity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFacturaServicioAsync(string facturaCodigo, string servicioCodigo)
        {
            var facturaServicioEntity = await _dbContext.FacturaServicios
                .FirstOrDefaultAsync(fs => fs.FacturaCodigo == facturaCodigo && fs.ServicioCodigo == servicioCodigo);

            if (facturaServicioEntity == null) return 0;

            _dbContext.FacturaServicios.Remove(facturaServicioEntity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FacturaServicioDto>> GetFacturaServiciosAsync(string facturaCodigo)
        {
            var facturaServicios = await _dbContext.FacturaServicios
                .Where(fs => fs.FacturaCodigo == facturaCodigo).ToListAsync();
            var facturaServicioDtos = new List<FacturaServicioDto>();

            foreach (var fs in facturaServicios)
            {
                facturaServicioDtos.Add(new FacturaServicioDto
                {
                    FacturaCodigo = fs.FacturaCodigo,
                    Idproducto = fs.Idproducto,
                    Idautorizacion = fs.Idautorizacion,
                    Costo = fs.Costo,
                    ServicioCodigo = fs.ServicioCodigo
                });
            }

            return facturaServicioDtos;
        }

        public async Task<int> AddFacturaProductoAsync(FacturaProductoDto facturaProducto)
        {
            var facturaProductoEntity = new FacturaProducto
            {
                FacturaCodigo = facturaProducto.FacturaCodigo,
                Idproducto = facturaProducto.Idproducto,
                Idautorizacion = facturaProducto.Idautorizacion,
                Precio = facturaProducto.Precio,
                Cantidad = facturaProducto.Cantidad
            };

            _dbContext.FacturaProductos.Add(facturaProductoEntity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFacturaProductoAsync(string facturaCodigo, int idProducto)
        {
            var facturaProductoEntity = await _dbContext.FacturaProductos
                .FirstOrDefaultAsync(fp => fp.FacturaCodigo == facturaCodigo && fp.Idproducto == idProducto);

            if (facturaProductoEntity == null) return 0;

            _dbContext.FacturaProductos.Remove(facturaProductoEntity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FacturaProductoDto>> GetFacturaProductosAsync(string facturaCodigo)
        {
            var facturaProductos = await _dbContext.FacturaProductos
                .Where(fp => fp.FacturaCodigo == facturaCodigo).ToListAsync();
            var facturaProductoDtos = new List<FacturaProductoDto>();

            foreach (var fp in facturaProductos)
            {
                facturaProductoDtos.Add(new FacturaProductoDto
                {
                    FacturaCodigo = fp.FacturaCodigo,
                    Idproducto = fp.Idproducto,
                    Idautorizacion = fp.Idautorizacion,
                    Precio = fp.Precio,
                    Cantidad = fp.Cantidad
                });
            }

            return facturaProductoDtos;
        }

        public async Task<int> AddFacturaMetodoPagoAsync(string facturaCodigo, int idMetodoPago)
        {
            var factura = await _dbContext.Facturas.FindAsync(facturaCodigo);
            if (factura == null) return 0;

            factura.CodigoMetodoDePago = idMetodoPago;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFacturaMetodoPagoAsync(string facturaCodigo, int idMetodoPago)
        {
            var factura = await _dbContext.Facturas.FindAsync(facturaCodigo);
            if (factura == null || factura.CodigoMetodoDePago != idMetodoPago) return 0;

            factura.CodigoMetodoDePago = null;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MetodoDePago>> GetMetodoPagosAsync(string facturaCodigo)
        {
            var factura = await _dbContext.Facturas.Include(f => f.CodigoMetodoDePagoNavigation)
                                                   .FirstOrDefaultAsync(f => f.FacturaCodigo == facturaCodigo);

            if (factura == null) return new List<MetodoDePago>();

            return new List<MetodoDePago> { factura.CodigoMetodoDePagoNavigation };
        }
    }
}
