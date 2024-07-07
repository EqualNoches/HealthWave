using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;


namespace HospitalCore_core.Services
{
    public class TipoServicioService(HospitalCore dbContext) : ITipoServicioService
    {
        private readonly LogManager<TipoServicioService> _logManager = new();

        public async Task<int> AddTipoServicioAsync(TipoServicioDto tipoServicioDto)
        {
            try
            {
                var tipoServicio = new TipoServicio
                {
                    Nombre = tipoServicioDto.Nombre
                };

                dbContext.TipoServicios.Add(tipoServicio);
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo("Tipo de servicio creado con éxito.");
                return tipoServicio.IdTipoServicio; // Retorna el ID del tipo de servicio creado
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al crear el tipo de servicio.", ex);
                throw;
            }
        }

        public async Task<List<TipoServicioDto>> GetTipoServiciosAsync()
        {
            try
            {
                var tipoServicios = await dbContext.TipoServicios
                    .Select(ts => new TipoServicioDto { IdTipoServicio = ts.IdTipoServicio, Nombre = ts.Nombre })
                    .ToListAsync();

                _logManager.LogInfo("Tipos de servicio recuperados con éxito.");
                return tipoServicios;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al recuperar los tipos de servicios.", ex);
                throw;
            }
        }

        public async Task<int> UpdateTipoServicioAsync(TipoServicioDto tipoServicioDto)
        {
            try
            {
                var tipoServicio = await dbContext.TipoServicios.FindAsync(tipoServicioDto.IdTipoServicio);
                if (tipoServicio == null)
                {
                    _logManager.LogInfo("Tipo de servicio no encontrado.");
                    return 0;
                }

                tipoServicio.Nombre = tipoServicioDto.Nombre;
                dbContext.TipoServicios.Update(tipoServicio);
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo("Tipo de servicio actualizado con éxito.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al actualizar el tipo de servicio.", ex);
                throw;
            }
        }

        public async Task<int> DeleteTipoServicioAsync(uint idTipoServicio)
        {
            try
            {
                var tipoServicio = await dbContext.TipoServicios.FindAsync(idTipoServicio);
                if (tipoServicio == null)
                {
                    _logManager.LogInfo("Tipo de servicio no encontrado.");
                    return 0;
                }

                dbContext.TipoServicios.Remove(tipoServicio);
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo("Tipo de servicio eliminado con éxito.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar el tipo de servicio.", ex);
                throw;
            }
        }
    }
}
