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
        private readonly HospitalCore dbContext;
        private readonly LogManager<AutorizacionService> _logManager = new();

        public AutorizacionService(HospitalCore dbContext)
        {
            this.dbContext = dbContext;
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
                    var ingreso = await dbContext.Ingresos.FindAsync(idIngreso);
                    if (ingreso == null)
                        throw new InvalidOperationException("No existe este ingreso");

                    ingreso.Idautorizacion = autorizacion.Idautorizacion;
                    dbContext.Entry(ingreso).State = EntityState.Modified;
                }

                if (!string.IsNullOrEmpty(consultaCodigo))
                {
                    var consulta = await dbContext.Consulta.SingleOrDefaultAsync(c => c.ConsultaCodigo == int.Parse(consultaCodigo));
                    if (consulta == null)
                        throw new InvalidOperationException("No existe esa consulta");

                    consulta.Idautorizacion = autorizacion.Idautorizacion;
                    dbContext.Entry(consulta).State = EntityState.Modified;
                }

                if (!string.IsNullOrEmpty(servicioCodigo) && !string.IsNullOrEmpty(facturaCodigo))
                {
                    var facturaServicios = await dbContext.FacturaServicios.SingleOrDefaultAsync(fS => fS.ServicioCodigo == servicioCodigo && fS.FacturaCodigo == facturaCodigo);
                    if (facturaServicios == null)
                        throw new InvalidOperationException("El servicio proporcionado no existe");

                    facturaServicios.Idautorizacion = autorizacion.Idautorizacion;
                    dbContext.Entry(facturaServicios).State = EntityState.Modified;
                }

                if (idProducto != null && !string.IsNullOrEmpty(facturaCodigo))
                {
                    var facturaProducto = await dbContext.FacturaProductos.SingleOrDefaultAsync(fp => fp.Idproducto == idProducto && fp.FacturaCodigo == facturaCodigo);
                    if (facturaProducto == null)
                        throw new ArgumentException("El producto facturado provisto no existe");

                    facturaProducto.Idautorizacion = autorizacion.Idautorizacion;
                    dbContext.Entry(facturaProducto).State = EntityState.Modified;
                }

                dbContext.Autorizacions.Add(autorizacion);
                return await dbContext.SaveChangesAsync();
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
                var autorizacion = await dbContext.Autorizacions.FindAsync(id);
                if (autorizacion == null)
                {
                    return 0;
                }

                dbContext.Autorizacions.Remove(autorizacion);
                await dbContext.SaveChangesAsync();

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
                return await dbContext.Autorizacions.Select(a => new AutorizacionDTO
                {
                    Idautorizacion = a.Idautorizacion,
                    FechaAutorizacion = a.FechaAutorizacion,
                    MontoAutorizado = a.MontoAutorizado,
                    Idaseguradora = a.Idaseguradora
                }).ToListAsync();
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
                var autorizacion = await dbContext.Autorizacions.FindAsync(id);
                if (autorizacion == null)
                {
                    return null;
                }

                return new AutorizacionDTO
                {
                    Idautorizacion = autorizacion.Idautorizacion,
                    FechaAutorizacion = autorizacion.FechaAutorizacion,
                    MontoAutorizado = autorizacion.MontoAutorizado,
                    Idaseguradora = autorizacion.Idaseguradora
                };
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

                dbContext.Entry(autorizacion).State = EntityState.Modified;

                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    if (!AutorizacionExists(autorizacion.Idautorizacion))
                    {
                        return 0;
                    }

                    throw;
                }

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
            return dbContext.Autorizacions.Any(e => e.Idautorizacion == id);
        }
    }
}
