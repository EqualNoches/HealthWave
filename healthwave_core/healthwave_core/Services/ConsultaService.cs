using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace HospitalCore_core.Services
{
    public class ConsultaService(HospitalCore _dbContext) : IConsultaService
    {
        private readonly LogManager<ConsultaService> _logManager = new();
        public async Task<int> AddConsultaAfeccionAsync(int consultaCodigo, int idAfeccion)
        {
            try
            {
                _dbContext.Consulta.First(e => e.ConsultaCodigo == consultaCodigo).Idafeccions.Add(_dbContext.Afeccions.First(e => e.IdAfeccion == idAfeccion));
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error relacionando afección", ex);
                return await Task.FromResult(0);
            }
        }

        public async Task<int> AddConsultaAsync(ConsultaDto consulta)
        {
            try
            {
                var consultum = Consultum.FromDto(consulta);
                await _dbContext.Consulta.AddAsync(consultum);
                await _dbContext.SaveChangesAsync();
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
                Consultum consultum = _dbContext.Consulta.Include(consultum => consultum.PrescripcionProductos).First(e => e.ConsultaCodigo == consultaCodigo);
                PrescripcionProducto? prescripcionProducto = consultum.PrescripcionProductos.FirstOrDefault(e => e.Idproducto == idProducto);
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
                return await _dbContext.SaveChangesAsync();
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
                _dbContext.Consulta
                    .First(e => e.ConsultaCodigo == consultaCodigo)
                    .ServicioCodigos
                    .Add(_dbContext.Servicios.First(e => e.ServicioCodigo == servicioCodigo));
                return await _dbContext.SaveChangesAsync();
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
                var consultum = await _dbContext.Consulta.Where(e => e.ConsultaCodigo == consultaCodigo).Include(e => e.Idafeccions).FirstAsync();
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
                return (await _dbContext.Consulta.ToListAsync()).Select(e => ConsultaDto.FromModel(e)).ToList();
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
                var productos = await _dbContext.Productos.FromSql($"EXEC dbo.spConsultaListarProductos {consultaCodigo}").ToListAsync();
                var productoDto = productos.Select(e => ProductoDto.FromModel(e)).ToList();
                return productoDto;
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
                var result = await _dbContext.Consulta.Where(e => e.ConsultaCodigo == consulta.ConsultaCodigo).FirstAsync();
                result.CodigoPaciente = consulta.DocumentoPaciente;
                result.CodigoDocumentoMedico = consulta.DocumentoMedico;
                result.Idconsultorio = consulta.IdConsultorio;
                result.Motivo = consulta.Motivo;
                result.Descripcion = consulta.Comentarios;
                result.Costo = consulta.costo;
                result.Estado = consulta.Estado;
                await _dbContext.SaveChangesAsync();
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
                _dbContext.Consulta.Remove(_dbContext.Consulta.First(e => e.ConsultaCodigo == consultaCodigo));
                await _dbContext.SaveChangesAsync();
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
                var result = await _dbContext.Consulta.Where(e => e.ConsultaCodigo == consultaCodigo)
                    .Include(consultum => consultum.ServicioCodigos).FirstAsync();
                result.ServicioCodigos.Remove(result.ServicioCodigos.First(e => e.ServicioCodigo == servicioCodigo));
                await _dbContext.SaveChangesAsync();
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
                List<Servicio> servicios = await _dbContext.Consulta
                    .Where(e => e.ConsultaCodigo == consultaCodigo)
                    .Include(e => e.ServicioCodigos)
                    .SelectMany(e => e.ServicioCodigos)
                    .ToListAsync();
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
                return await _dbContext.Database.ExecuteSqlAsync($"EXEC dbo.spConsultaDesrelacionarProducto {consultaCodigo}, {idProducto}");
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
                var result = await _dbContext.Consulta.Where(e => e.ConsultaCodigo == consultaCodigo)
                    .Include(consultum => consultum.Idafeccions).FirstAsync();
                result.Idafeccions.Remove(result.Idafeccions.First(e => e.IdAfeccion == idAfeccion));
                await _dbContext.SaveChangesAsync();
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
