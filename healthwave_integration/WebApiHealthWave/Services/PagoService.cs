using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class PagoService : IPagoService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<PagoService> _logHandler = new();

        public PagoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pago>> GetPagosAsync()
        {
            try
            {
                var pagos = await _dbContext.Pagos.ToListAsync();
                if (pagos.Any())
                {
                    _logHandler.LogInfo("Pagos obtenidos exitosamente desde la base de datos.");
                    return pagos;
                }
                else
                {
                    _logHandler.LogInfo("No se encontraron pagos en la base de datos.");
                    return new List<Pago>();
                }
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al obtener los pagos.", ex);
                throw;
            }
        }

        public async Task<Pago> GetPagoByIdAsync(uint idPago)
        {
            try
            {
                var pago = await _dbContext.Pagos.FindAsync(idPago);
                if (pago != null)
                {
                    _logHandler.LogInfo($"Pago con ID {idPago} obtenido exitosamente.");
                    return pago;
                }
                else
                {
                    _logHandler.LogInfo($"Pago con ID {idPago} no encontrado.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logHandler.LogError($"Error al obtener el pago con ID {idPago}.", ex);
                throw;
            }
        }

        public async Task<int> CreatePagoAsync(Pago pago)
        {
            try
            {
                _dbContext.Pagos.Add(pago);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Pago creado exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al crear el pago.", ex);
                throw;
            }
        }

        public async Task<int> UpdatePagoAsync(Pago pago)
        {
            try
            {
                var existingPago = await _dbContext.Pagos.FindAsync(pago.IDPago);
                if (existingPago == null)
                {
                    _logHandler.LogInfo("Pago no encontrado.");
                    return 0;
                }

                existingPago.MontoPagado = pago.MontoPagado;
                existingPago.Fecha = pago.Fecha;
                existingPago.IDCuenta = pago.IDCuenta;

                _dbContext.Entry(existingPago).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Pago actualizado exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al actualizar el pago.", ex);
                throw;
            }
        }

        public async Task<int> DeletePagoAsync(uint idPago)
        {
            try
            {
                var pago = await _dbContext.Pagos.FindAsync(idPago);
                if (pago == null)
                {
                    _logHandler.LogInfo("Pago no encontrado.");
                    return 0;
                }

                _dbContext.Pagos.Remove(pago);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Pago eliminado exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al eliminar el pago.", ex);
                throw;
            }
        }
    }
}

