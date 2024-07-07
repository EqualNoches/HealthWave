using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.Utilities;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace HospitalCore_core.Services
{
    public class PagoService(HospitalCore dbCore) : IPagoService
    {
        private readonly LogManager<PagoService> _logManager = new();

        public async Task<int> CreatePago(Pago pago)
        {
            try
            {
                dbCore.Pagos.Add(pago);
                await dbCore.SaveChangesAsync();
                _logManager.LogInfo("CreatePago executed successfully.");
                return 1;
            }
            catch (SqlException ex)
            {
                _logManager.LogFatal("Error al guardar el pago en la base de datos.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error inesperado al guardar el pago.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Pago>> GetPagos()
        {
            try
            {
                var pagos = await dbCore.Pagos.ToListAsync();
                _logManager.LogInfo("GetPagos executed successfully.");
                return pagos;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al obtener los pagos.", ex);
                throw;
            }
        }

        public async Task<Pago> GetPagoById(int idPago)
        {
            try
            {
                var pago = await dbCore.Pagos.FindAsync(idPago);
                if (pago == null)
                {
                    _logManager.LogInfo($"Pago with ID {idPago} not found.");
                    return null;
                }
                _logManager.LogInfo($"GetPagoById executed successfully for ID {idPago}.");
                return pago;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error al obtener el pago con ID {idPago}.", ex);
                throw;
            }
        }

        public async Task<int> UpdatePago(Pago pago)
        {
            try
            {
                dbCore.Entry(pago).State = EntityState.Modified;
                await dbCore.SaveChangesAsync();
                _logManager.LogInfo($"UpdatePago executed successfully for ID {pago.Idpago}.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error al actualizar el pago con ID {pago.Idpago}.", ex);
                throw;
            }
        }

        public async Task<int> DeletePago(uint idPago)
        {
            try
            {
                var pago = await dbCore.Pagos.FindAsync(idPago);
                if (pago == null)
                {
                    _logManager.LogInfo($"Pago with ID {idPago} not found.");
                    return 0;
                }

                dbCore.Pagos.Remove(pago);
                await dbCore.SaveChangesAsync();
                _logManager.LogInfo($"DeletePago executed successfully for ID {idPago}.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error al borrar el pago con ID {idPago}.", ex);
                throw;
            }
        }
    }
}
