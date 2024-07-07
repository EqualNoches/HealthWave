using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;


namespace HospitalCore_core.Services
{
    public class ConsultaService(HospitalCore dbContext) : IConsultaService
    {
        private readonly LogManager<ConsultaService> _logManager = new();

        public async Task<int> AddConsultaAfeccionAsync(int consultaCodigo, int idAfeccion)
        {
            try
            {
                dbContext.Consulta.First(e => e.ConsultaCodigo == consultaCodigo).Idafeccions
                    .Add(dbContext.Afeccions.First(e => e.IdAfeccion == idAfeccion));
                int result = await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Afección {idAfeccion} agregada a consulta {consultaCodigo} exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error relacionando afección", ex);
                return 0;
            }
        }

        public async Task<int> AddConsultaAsync(ConsultaDto consulta)
        {
            try
            {
                var consultum = Consultum.FromDto(consulta);
                await dbContext.Consulta.AddAsync(consultum);
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Consulta {consulta.ConsultaCodigo} creada exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al crear consulta", ex);
                return 0;
            }
        }

        public async Task<int> AddConsultaProductoAsync(int consultaCodigo, int idProducto, int cantidad)
        {
            try
            {
                var consultum = dbContext.Consulta.Include(consultum => consultum.PrescripcionProductos)
                    .First(e => e.ConsultaCodigo == consultaCodigo);
                var prescripcionProducto = consultum.PrescripcionProductos.FirstOrDefault(e => e.Idproducto == idProducto);
                if (prescripcionProducto == null)
                {
                    consultum.PrescripcionProductos.Add(new PrescripcionProducto
                    {
                        Idproducto = idProducto,
                        Cantidad = cantidad
                    });
                }
                else
                {
                    prescripcionProducto.Cantidad += cantidad;
                }

                int result = await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Producto {idProducto} agregado a consulta {consultaCodigo} exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al relacionar producto", ex);
                throw;
            }
        }

        public async Task<int> AddConsultaServicioAsync(int consultaCodigo, int servicioCodigo)
        {
            try
            {
                dbContext.Consulta.First(e => e.ConsultaCodigo == consultaCodigo).ServicioCodigos
                    .Add(dbContext.Servicios.First(e => e.ServicioCodigo == servicioCodigo));
                int result = await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Servicio {servicioCodigo} agregado a consulta {consultaCodigo} exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al relacionar servicio", ex);
                throw;
            }
        }

        public async Task<List<Afeccion>> GetConsultaAfeccionesAsync(int consultaCodigo)
        {
            try
            {
                var consultum = await dbContext.Consulta.Where(e => e.ConsultaCodigo == consultaCodigo)
                    .Include(e => e.Idafeccions).FirstAsync();
                _logManager.LogInfo($"Afecciones obtenidas para la consulta {consultaCodigo} exitosamente.");
                return consultum.Idafeccions.ToList();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al listar afecciones", ex);
                return new List<Afeccion>();
            }
        }

        public async Task<List<ConsultaDto>> GetConsultasAsync()
        {
            try
            {
                var consultas = await dbContext.Consulta.ToListAsync();
                _logManager.LogInfo("Consultas obtenidas exitosamente.");
                return consultas.Select(e => ConsultaDto.FromModel(e)).ToList();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al listar consultas", ex);
                return new List<ConsultaDto>();
            }
        }

        public async Task<List<ProductoDto>> GetConsultaProductosAsync(int consultaCodigo)
        {
            try
            {
                var productos = await dbContext.Productos
                    .FromSql($"EXEC dbo.spConsultaListarProductos {consultaCodigo}").ToListAsync();
                _logManager.LogInfo($"Productos obtenidos para la consulta {consultaCodigo} exitosamente.");
                return productos.Select(e => ProductoDto.FromModel(e)).ToList();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al listar productos", ex);
                return new List<ProductoDto>();
            }
        }

        public async Task<int> UpdateConsultaAsync(ConsultaDto consulta)
        {
            try
            {
                var result = await dbContext.Consulta.Where(e => e.ConsultaCodigo == consulta.ConsultaCodigo).FirstAsync();
                result.CodigoPaciente = consulta.DocumentoPaciente;
                result.CodigoDocumentoMedico = consulta.DocumentoMedico;
                result.Idconsultorio = consulta.IdConsultorio;
                result.Motivo = consulta.Motivo;
                result.Descripcion = consulta.Comentarios;
                result.Costo = consulta.costo;
                result.Estado = consulta.Estado;
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Consulta {consulta.ConsultaCodigo} actualizada exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al actualizar consulta", ex);
                return 0;
            }
        }

        public async Task<int> RemoveConsultaAsync(int consultaCodigo)
        {
            try
            {
                dbContext.Consulta.Remove(dbContext.Consulta.First(e => e.ConsultaCodigo == consultaCodigo));
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Consulta {consultaCodigo} eliminada exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar consulta", ex);
                return 0;
            }
        }

        public async Task<int> RemoveConsultaServicioAsync(int consultaCodigo, int servicioCodigo)
        {
            try
            {
                var result = await dbContext.Consulta.Where(e => e.ConsultaCodigo == consultaCodigo)
                    .Include(consultum => consultum.ServicioCodigos).FirstAsync();
                result.ServicioCodigos.Remove(result.ServicioCodigos.First(e => e.ServicioCodigo == servicioCodigo));
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Servicio {servicioCodigo} desrelacionado de consulta {consultaCodigo} exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al desrelacionar servicio", ex);
                return 0;
            }
        }

        public async Task<List<Servicio>> GetConsultaServiciosAsync(int consultaCodigo)
        {
            try
            {
                var servicios = await dbContext.Consulta.Where(e => e.ConsultaCodigo == consultaCodigo)
                    .Include(e => e.ServicioCodigos).SelectMany(e => e.ServicioCodigos).ToListAsync();
                _logManager.LogInfo($"Servicios obtenidos para la consulta {consultaCodigo} exitosamente.");
                return servicios;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al listar servicios", ex);
                return new List<Servicio>();
            }
        }

        public async Task<int> RemoveConsultaProductoAsync(int consultaCodigo, int idProducto)
        {
            try
            {
                int result = await dbContext.Database.ExecuteSqlAsync($"EXEC dbo.spConsultaDesrelacionarProducto {consultaCodigo}, {idProducto}");
                _logManager.LogInfo($"Producto {idProducto} desrelacionado de consulta {consultaCodigo} exitosamente.");
                return result;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al desrelacionar producto", ex);
                return 0;
            }
        }

        public async Task<int> RemoveConsultaAfeccionAsync(int consultaCodigo, uint idAfeccion)
        {
            try
            {
                var result = await dbContext.Consulta.Where(e => e.ConsultaCodigo == consultaCodigo)
                    .Include(consultum => consultum.Idafeccions).FirstAsync();
                result.Idafeccions.Remove(result.Idafeccions.First(e => e.IdAfeccion == idAfeccion));
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Afección {idAfeccion} desrelacionada de consulta {consultaCodigo} exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al desrelacionar afección", ex);
                return 0;
            }
        }
    }
}
