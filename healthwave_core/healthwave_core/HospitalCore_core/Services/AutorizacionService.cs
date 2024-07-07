using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;


namespace HospitalCore_core.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly HospitalCore _dbContext;
        private readonly LogManager<AutorizacionService> _logManager = new();

        public AutorizacionService(HospitalCore dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> AddAutorizacion(AutorizacionDTO autorizacionDto, int? idIngreso, string? consultaCodigo, string? facturaCodigo, string? servicioCodigo, int? idProducto)
        {
            try
            {
                var autorizacion = new Autorizacion
                {
                    Idautorizacion = autorizacionDto.Idautorizacion,
                    FechaAutorizacion = autorizacionDto.FechaAutorizacion,
                    MontoAutorizado = autorizacionDto.MontoAutorizado,
                    Idaseguradora = autorizacionDto.Idaseguradora
                };

                if (AutorizacionExists(autorizacion.Idautorizacion))
                    throw new InvalidOperationException("Autorizacion ya existente");

                if (idIngreso != null)
                {
                    var ingreso = await _dbContext.Ingresos.FindAsync(idIngreso);
                    if (ingreso == null)
                        throw new InvalidOperationException("No existe este ingreso");

                    ingreso.Idautorizacion = autorizacion.Idautorizacion;
                    _dbContext.Entry(ingreso).State = EntityState.Modified;
                }

                if (!string.IsNullOrEmpty(consultaCodigo))
                {
                    var consulta = await _dbContext.Consulta.SingleOrDefaultAsync(c => c.ConsultaCodigo == int.Parse(consultaCodigo));
                    if (consulta == null)
                        throw new InvalidOperationException("No existe esa consulta");

                    consulta.Idautorizacion = autorizacion.Idautorizacion;
                    _dbContext.Entry(consulta).State = EntityState.Modified;
                }

                if (!string.IsNullOrEmpty(servicioCodigo) && !string.IsNullOrEmpty(facturaCodigo))
                {
                    var facturaServicios = await _dbContext.FacturaServicios.SingleOrDefaultAsync(fS => fS.ServicioCodigo == servicioCodigo && fS.FacturaCodigoServicio == facturaCodigo);
                    if (facturaServicios == null)
                        throw new InvalidOperationException("El servicio proporcionado no existe");

                    facturaServicios.Idautorizacion = autorizacion.Idautorizacion;
                    _dbContext.Entry(facturaServicios).State = EntityState.Modified;
                }

                if (idProducto != null && !string.IsNullOrEmpty(facturaCodigo))
                {
                    var facturaProducto = await _dbContext.FacturaProductos.SingleOrDefaultAsync(fp => fp.Idproducto == idProducto && fp.FacturaCodigoProducto == facturaCodigo);
                    if (facturaProducto == null)
                        throw new ArgumentException("El producto facturado provisto no existe");

                    facturaProducto.Idautorizacion = autorizacion.Idautorizacion;
                    _dbContext.Entry(facturaProducto).State = EntityState.Modified;
                }

                _dbContext.Autorizacions.Add(autorizacion);
                var response = await _dbContext.SaveChangesAsync();
                _logManager.LogInfo("La autorizacion fue creada correctamente");
                return response;

            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Autorización no pudo ser creada", ex);
                throw;
            }
        }

        public async Task<int> DeleteAutorizacionAsync(int id)
        {
            try
            {
                var autorizacion = await _dbContext.Autorizacions.FindAsync(id);
                if (autorizacion == null)
                {
                    return 0;
                }

                _dbContext.Autorizacions.Remove(autorizacion);
                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo("Se borro correctamente la autorización");
                return 1;
            }
            catch (Exception e)
            {
                _logManager.LogFatal("Error al borrar esta aseguradora", e);
                throw;
            }
        }

        public async Task<List<AutorizacionDTO>> GetAllAutorizaciones()
        {
            try
            {
                var response = await _dbContext.Autorizacions.Select(a => new AutorizacionDTO
                {
                    Idautorizacion = a.Idautorizacion,
                    FechaAutorizacion = a.FechaAutorizacion,
                    MontoAutorizado = a.MontoAutorizado,
                    Idaseguradora = a.Idaseguradora
                }).ToListAsync();
                _logManager.LogInfo($"Impreso correctamente \n{response}");
                return response;
            }
            catch (Exception exception)
            {
                _logManager.LogFatal("Error al obtener autorizaciones", exception);
                throw;
            }
        }

        public async Task<AutorizacionDTO?> GetAutorizacionById(int id)
        {
            try
            {
                var autorizacion = await _dbContext.Autorizacions.FindAsync(id);
                if (autorizacion == null)
                {
                    _logManager.LogInfo("no se encontro el registro en la base de datos");
                    return null;
                }

                var response = new AutorizacionDTO
                {
                    Idautorizacion = autorizacion.Idautorizacion,
                    FechaAutorizacion = autorizacion.FechaAutorizacion,
                    MontoAutorizado = autorizacion.MontoAutorizado,
                    Idaseguradora = autorizacion.Idaseguradora
                };
                _logManager.LogInfo("Llamada exitosa");
                return response;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al obtener autorizacion", ex);
                throw;
            }
        }

        public async Task<int> UpdateAutorizacionAsync(AutorizacionDTO autorizacionDto)
        {
            try
            {
                var autorizacion = new Autorizacion
                {
                    Idautorizacion = autorizacionDto.Idautorizacion,
                    FechaAutorizacion = autorizacionDto.FechaAutorizacion,
                    MontoAutorizado = autorizacionDto.MontoAutorizado,
                    Idaseguradora = autorizacionDto.Idaseguradora
                };

                _dbContext.Entry(autorizacion).State = EntityState.Modified;

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    if (!AutorizacionExists(autorizacion.Idautorizacion))
                    {
                        return 0;
                    }

                    throw;
                }

                _logManager.LogInfo($"Se pudo actualizar la autorización {autorizacionDto.Idautorizacion} correctamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al actualizar la autorizacion", ex);
                throw;
            }
        }

        private bool AutorizacionExists(int id)
        {
            return _dbContext.Autorizacions.Any(e => e.Idautorizacion == id);
        }
    }
}
