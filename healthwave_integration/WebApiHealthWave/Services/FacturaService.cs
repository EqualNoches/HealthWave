using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<FacturaService> _logManager = new();

        public FacturaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddFacturaAsync(FacturaDto facturaDto)
        {
            try
            {
                var factura = Factura.FromDto(facturaDto);
                await _dbContext.Facturas.AddAsync(factura);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al agregar una factura", ex);
                return 0;
            }
        }

        public async Task<int> UpdateFacturaAsync(FacturaDto facturaDto)
        {
            try
            {
                var factura = await _dbContext.Facturas.FindAsync(facturaDto.FacturaCodigo);
                if (factura == null)
                {
                    return 0;
                }

                factura.MontoTotal = facturaDto.MontoTotal;
                factura.MontoSubtotal = facturaDto.MontoSubtotal;
                factura.Fecha = facturaDto.Fecha;
                factura.RNC = facturaDto.RNC;
                factura.CodigoMetodoDePago = facturaDto.CodigoMetodoDePago;
                factura.CodigoPaciente = facturaDto.CodigoPaciente;
                factura.IDIngreso = facturaDto.IDIngreso;
                factura.IDCuenta = facturaDto.IDCuenta;
                factura.ConsultaCodigo = facturaDto.ConsultaCodigo;

                _dbContext.Facturas.Update(factura);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al actualizar una factura", ex);
                return 0;
            }
        }

        public async Task<int> DeleteFacturaAsync(string facturaCodigo)
        {
            try
            {
                var factura = await _dbContext.Facturas.FindAsync(int.Parse(facturaCodigo));
                if (factura == null)
                {
                    return 0;
                }

                _dbContext.Facturas.Remove(factura);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar una factura", ex);
                return 0;
            }
        }

        public async Task<IEnumerable<FacturaDto>> GetFacturasAsync()
        {
            try
            {
                var facturas = await _dbContext.Facturas.ToListAsync();
                return facturas.Select(FacturaDto.FromModel).ToList();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener las facturas", ex);
                return new List<FacturaDto>();
            }
        }

        public async Task<int> AddFacturaServicioAsync(FacturaServicioDto facturaServicioDto)
        {
            try
            {
                var facturaServicio = new FacturaServicio
                {
                    FacturaCodigoServicio = facturaServicioDto.FacturaCodigoServicio,
                    ServicioCodigo = facturaServicioDto.ServicioCodigo,
                    Costo = facturaServicioDto.Costo
                };

                await _dbContext.FacturaServicios.AddAsync(facturaServicio);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al agregar un servicio a la factura", ex);
                return 0;
            }
        }

        public async Task<int> DeleteFacturaServicioAsync(string facturaCodigo, string servicioCodigo)
        {
            try
            {
                var facturaServicio = await _dbContext.FacturaServicios
                    .FirstOrDefaultAsync(fs => fs.FacturaCodigoServicio == int.Parse(facturaCodigo) && fs.ServicioCodigo == servicioCodigo);

                if (facturaServicio == null)
                {
                    return 0;
                }

                _dbContext.FacturaServicios.Remove(facturaServicio);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar un servicio de la factura", ex);
                return 0;
            }
        }

        public async Task<IEnumerable<FacturaServicioDto>> GetFacturaServiciosAsync(string facturaCodigo)
        {
            try
            {
                var facturaServicios = await _dbContext.FacturaServicios
                    .Where(fs => fs.FacturaCodigoServicio == int.Parse(facturaCodigo))
                    .ToListAsync();
                return facturaServicios.Select(FacturaServicioDto.FromModel).ToList();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener los servicios de la factura", ex);
                return new List<FacturaServicioDto>();
            }
        }

        public async Task<int> AddFacturaProductoAsync(FacturaProductoDto facturaProductoDto)
        {
            try
            {
                var facturaProducto = new FacturaProducto
                {
                    FacturaCodigoProducto = facturaProductoDto.FacturaCodigoProducto,
                    IDProducto = facturaProductoDto.IDProducto,
                    Precio = facturaProductoDto.Precio,
                    Cantidad = facturaProductoDto.Cantidad
                };

                await _dbContext.FacturaProductos.AddAsync(facturaProducto);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al agregar un producto a la factura", ex);
                return 0;
            }
        }

        public async Task<int> DeleteFacturaProductoAsync(string facturaCodigo, uint idProducto)
        {
            try
            {
                var facturaProducto = await _dbContext.FacturaProductos
                    .FirstOrDefaultAsync(fp => fp.FacturaCodigoProducto == int.Parse(facturaCodigo) && fp.IDProducto == idProducto);

                if (facturaProducto == null)
                {
                    return 0;
                }

                _dbContext.FacturaProductos.Remove(facturaProducto);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar un producto de la factura", ex);
                return 0;
            }
        }

        public async Task<IEnumerable<FacturaProductoDto>> GetFacturaProductosAsync(string facturaCodigo)
        {
            try
            {
                var facturaProductos = await _dbContext.FacturaProductos
                    .Where(fp => fp.FacturaCodigoProducto == int.Parse(facturaCodigo))
                    .ToListAsync();
                return facturaProductos.Select(FacturaProductoDto.FromModel).ToList();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener los productos de la factura", ex);
                return new List<FacturaProductoDto>();
            }
        }

        public async Task<int> AddFacturaMetodoPagoAsync(string facturaCodigo, uint idMetodoPago)
        {
            try
            {
                var metodoPago = await _dbContext.MetodosDePago.FindAsync(idMetodoPago);
                if (metodoPago == null)
                {
                    return 0;
                }

                var factura = await _dbContext.Facturas.FindAsync(int.Parse(facturaCodigo));
                if (factura == null)
                {
                    return 0;
                }

                factura.CodigoMetodoDePago = (int)idMetodoPago;
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al agregar un método de pago a la factura", ex);
                return 0;
            }
        }

        public async Task<int> DeleteFacturaMetodoPagoAsync(string facturaCodigo, uint idMetodoPago)
        {
            try
            {
                var factura = await _dbContext.Facturas.FindAsync(int.Parse(facturaCodigo));
                if (factura == null)
                {
                    return 0;
                }

                if (factura.CodigoMetodoDePago == idMetodoPago)
                {
                    factura.CodigoMetodoDePago = 0;
                    return await _dbContext.SaveChangesAsync();
                }

                return 0;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar un método de pago de la factura", ex);
                return 0;
            }
        }

        public async Task<IEnumerable<MetodoDePago>> GetMetodoPagosAsync(string facturaCodigo)
        {
            try
            {
                var factura = await _dbContext.Facturas
                    .Include(f => f.MetodoDePago)
                    .FirstOrDefaultAsync(f => f.FacturaCodigo == int.Parse(facturaCodigo));

                return factura != null && factura.MetodoDePago != null ? new List<MetodoDePago> { factura.MetodoDePago } : new List<MetodoDePago>();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener los métodos de pago de la factura", ex);
                return new List<MetodoDePago>();
            }
        }
    }
}

