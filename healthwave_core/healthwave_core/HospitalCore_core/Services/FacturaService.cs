using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalCore_core.Utilities;

namespace HospitalCore_core.Services
{
    public class FacturaService(HospitalCore context) : IFacturaService
    {
        private readonly LogManager<FacturaService> _logManager = new();

        public async Task<int> AddFacturaAsync(FacturaDto facturaDto)
        {
            try
            {
                var factura = Factura.FromDto(facturaDto);
                context.Facturas.Add(factura);
                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"Factura {factura.FacturaCodigo} agregada exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al agregar factura.", ex);
                throw new Exception("Error adding factura.", ex);
            }
        }

        public async Task<int> UpdateFacturaAsync(FacturaDto facturaDto)
        {
            try
            {
                var factura = await context.Facturas.FindAsync(facturaDto.FacturaCodigo);
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

                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"Factura {factura.FacturaCodigo} actualizada exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al actualizar factura {facturaDto.FacturaCodigo}.", ex);
                throw new Exception("Error updating factura.", ex);
            }
        }

        public async Task<int> DeleteFacturaAsync(string facturaCodigo)
        {
            try
            {
                var factura = await context.Facturas.FindAsync(facturaCodigo);
                if (factura == null) return 0;

                context.Facturas.Remove(factura);
                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"Factura {facturaCodigo} eliminada exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al eliminar factura {facturaCodigo}.", ex);
                throw new Exception("Error deleting factura.", ex);
            }
        }

        public async Task<IEnumerable<FacturaDto>> GetFacturasAsync()
        {
            try
            {
                var facturas = await context.Facturas
                    .Select(f => FacturaDto.FromModel(f))
                    .ToListAsync();
                _logManager.LogInfo("Lista de facturas obtenida exitosamente.");
                return facturas;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al listar facturas.", ex);
                throw new Exception("Error retrieving facturas list.", ex);
            }
        }

        public async Task<int> AddFacturaServicioAsync(FacturaServicioDto facturaServicioDto)
        {
            try
            {
                var facturaServicio = FacturaServicio.FromDto(facturaServicioDto);
                context.FacturaServicios.Add(facturaServicio);
                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"FacturaServicio agregado exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al agregar FacturaServicio.", ex);
                throw new Exception("Error adding FacturaServicio.", ex);
            }
        }

        public async Task<int> DeleteFacturaServicioAsync(string facturaCodigo, string servicioCodigo)
        {
            try
            {
                var facturaServicio = await context.FacturaServicios
                    .FirstOrDefaultAsync(fs => fs.FacturaCodigoServicio == facturaCodigo && fs.ServicioCodigo == servicioCodigo);
                if (facturaServicio == null) return 0;

                context.FacturaServicios.Remove(facturaServicio);
                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"FacturaServicio {facturaCodigo}-{servicioCodigo} eliminado exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al eliminar FacturaServicio {facturaCodigo}-{servicioCodigo}.", ex);
                throw new Exception("Error deleting FacturaServicio.", ex);
            }
        }

        public async Task<IEnumerable<FacturaServicioDto>> GetFacturaServiciosAsync(string facturaCodigo)
        {
            try
            {
                var facturaServicios = await context.FacturaServicios
                    .Where(fs => fs.FacturaCodigoNavigation.FacturaCodigo == facturaCodigo)
                    .Select(fs => FacturaServicioDto.FromModel(fs))
                    .ToListAsync();
                _logManager.LogInfo($"Lista de FacturaServicios para la factura {facturaCodigo} obtenida exitosamente.");
                return facturaServicios;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al listar FacturaServicios para la factura {facturaCodigo}.", ex);
                throw new Exception("Error retrieving FacturaServicios list.", ex);
            }
        }

        public async Task<int> AddFacturaProductoAsync(FacturaProductoDto facturaProductoDto)
        {
            try
            {
                var facturaProducto = FacturaProducto.FromDto(facturaProductoDto);
                context.FacturaProductos.Add(facturaProducto);
                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"FacturaProducto agregado exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al agregar FacturaProducto.", ex);
                throw new Exception("Error adding FacturaProducto.", ex);
            }
        }

        public async Task<int> DeleteFacturaProductoAsync(string facturaCodigoProducto, int idProducto)
        {
            try
            {
                var facturaProducto = await context.FacturaProductos
                    .FirstOrDefaultAsync(fp => fp.FacturaCodigoProducto == facturaCodigoProducto && fp.Idproducto == idProducto);
                if (facturaProducto == null) return 0;

                context.FacturaProductos.Remove(facturaProducto);
                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"FacturaProducto {facturaCodigoProducto}-{idProducto} eliminado exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al eliminar FacturaProducto {facturaCodigoProducto}-{idProducto}.", ex);
                throw new Exception("Error deleting FacturaProducto.", ex);
            }
        }

        public async Task<IEnumerable<FacturaProductoDto>> GetFacturaProductosAsync(string facturaCodigoProducto)
        {
            try
            {
                var facturaProductos = await context.FacturaProductos
                    .Where(fp => fp.FacturaCodigoProducto == facturaCodigoProducto)
                    .Select(fp => FacturaProductoDto.FromModel(fp))
                    .ToListAsync();
                _logManager.LogInfo($"Lista de FacturaProductos para la factura {facturaCodigoProducto} obtenida exitosamente.");
                return facturaProductos;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al listar FacturaProductos para la factura {facturaCodigoProducto}.", ex);
                throw new Exception("Error retrieving FacturaProductos list.", ex);
            }
        }

        public async Task<int> AddFacturaMetodoPagoAsync(string facturaCodigo, int codigoMetodoDePago)
        {
            try
            {
                var factura = await context.Facturas.FindAsync(facturaCodigo);
                if (factura == null) return 0;

                var metodoDePago = await context.MetodosDePago.FindAsync(codigoMetodoDePago);
                if (metodoDePago == null) return 0;

                factura.CodigoMetodoDePago = codigoMetodoDePago;
                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"Método de pago {codigoMetodoDePago} agregado a la factura {facturaCodigo} exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al agregar método de pago {codigoMetodoDePago} a la factura {facturaCodigo}.", ex);
                throw new Exception("Error adding MetodoDePago.", ex);
            }
        }

        public async Task<int> DeleteFacturaMetodoPagoAsync(string facturaCodigo, int codigoMetodoDePago)
        {
            try
            {
                var factura = await context.Facturas.FindAsync(facturaCodigo);
                if (factura == null) return 0;

                if (factura.CodigoMetodoDePago == codigoMetodoDePago)
                {
                    factura.CodigoMetodoDePago = null;
                }

                int result = await context.SaveChangesAsync();
                _logManager.LogInfo($"Método de pago {codigoMetodoDePago} eliminado de la factura {facturaCodigo} exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al eliminar método de pago {codigoMetodoDePago} de la factura {facturaCodigo}.", ex);
                throw new Exception("Error deleting MetodoDePago.", ex);
            }
        }

        public async Task<IEnumerable<MetodoDePagoDto>> GetMetodoPagosAsync(string facturaCodigo)
        {
            try
            {
                var factura = await context.Facturas
                    .Include(f => f.CodigoMetodoDePagoNavigation)
                    .FirstOrDefaultAsync(f => f.FacturaCodigo == facturaCodigo);

                if (factura == null || factura.CodigoMetodoDePagoNavigation == null)
                {
                    return Enumerable.Empty<MetodoDePagoDto>();
                }

                _logManager.LogInfo($"Lista de Métodos de Pago para la factura {facturaCodigo} obtenida exitosamente.");
                return new List<MetodoDePagoDto>
                {
                    MetodoDePagoDto.FromModel(factura.CodigoMetodoDePagoNavigation)
                };
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al listar Métodos de Pago para la factura {facturaCodigo}.", ex);
                throw new Exception("Error retrieving MetodoDePagos list.", ex);
            }
        }
    }
}
