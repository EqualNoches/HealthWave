using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;


namespace HospitalCore_core.Services
{
    public class ServicioService(HospitalCore context) : IServicioService
    {
        private readonly LogManager<ServicioService> _logManager = new();

        public async Task<IEnumerable<ServicioDto>> GetAllServicios()
        {
            try
            {
                var servicios = await context.Servicios
                    .Select(s => new ServicioDto
                    {
                        ServicioCodigo = s.ServicioCodigo,
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion,
                        IDTipoServicio = s.IDTipoServicio,
                        Costo = s.Costo,
                        IDAseguradora = s.IDAseguradora
                    }).ToListAsync();

                _logManager.LogInfo("Obtenidos todos los servicios exitosamente");
                return servicios;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al obtener todos los servicios", ex);
                throw;
            }
        }

        public async Task<ServicioDto> GetServicioById(int id)
        {
            try
            {
                var servicio = await context.Servicios.FindAsync(id);

                if (servicio == null)
                {
                    _logManager.LogInfo($"No se encontró el servicio con ID {id}");
                    return null;
                }

                var servicioDto = new ServicioDto
                {
                    ServicioCodigo = servicio.ServicioCodigo,
                    Nombre = servicio.Nombre,
                    Descripcion = servicio.Descripcion,
                    IDTipoServicio = servicio.IDTipoServicio,
                    Costo = servicio.Costo,
                    IDAseguradora = servicio.IDAseguradora
                };

                _logManager.LogInfo($"Obtenido el servicio con ID {id} exitosamente");
                return servicioDto;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error al obtener el servicio con ID {id}", ex);
                throw;
            }
        }

        public async Task<ServicioDto> CreateServicio(ServicioDto servicioDto)
        {
            try
            {
                var servicio = new Servicio
                {
                    ServicioCodigo = servicioDto.ServicioCodigo,
                    Nombre = servicioDto.Nombre,
                    Descripcion = servicioDto.Descripcion,
                    IDTipoServicio = servicioDto.IDTipoServicio,
                    Costo = servicioDto.Costo,
                    IDAseguradora = servicioDto.IDAseguradora
                };

                context.Servicios.Add(servicio);
                await context.SaveChangesAsync();

                servicioDto.ServicioCodigo = servicio.ServicioCodigo;
                _logManager.LogInfo($"Creado el servicio con ID {servicio.ServicioCodigo} exitosamente");
                return servicioDto;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al crear el servicio", ex);
                throw;
            }
        }

        public async Task<ServicioDto> UpdateServicio(ServicioDto servicioDto)
        {
            try
            {
                var servicio = await context.Servicios.FindAsync(servicioDto.ServicioCodigo);

                if (servicio == null)
                {
                    _logManager.LogInfo($"No se encontró el servicio con ID {servicioDto.ServicioCodigo} para actualizar");
                    return null;
                }

                servicio.Nombre = servicioDto.Nombre;
                servicio.Descripcion = servicioDto.Descripcion;
                servicio.IDTipoServicio = servicioDto.IDTipoServicio;
                servicio.Costo = servicioDto.Costo;
                servicio.IDAseguradora = servicioDto.IDAseguradora;

                await context.SaveChangesAsync();
                _logManager.LogInfo($"Actualizado el servicio con ID {servicioDto.ServicioCodigo} exitosamente");
                return servicioDto;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error al actualizar el servicio con ID {servicioDto.ServicioCodigo}", ex);
                throw;
            }
        }

        public async Task<bool> DeleteServicio(int id)
        {
            try
            {
                var servicio = await context.Servicios.FindAsync(id);

                if (servicio == null)
                {
                    _logManager.LogInfo($"No se encontró el servicio con ID {id} para eliminar");
                    return false;
                }

                context.Servicios.Remove(servicio);
                await context.SaveChangesAsync();
                _logManager.LogInfo($"Eliminado el servicio con ID {id} exitosamente");
                return true;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error al eliminar el servicio con ID {id}", ex);
                throw;
            }
        }
    }
}
