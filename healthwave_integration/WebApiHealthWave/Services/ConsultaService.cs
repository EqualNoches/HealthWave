using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;

namespace WebApiHealthWave.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<ConsultaService> _logManager = new();

        public ConsultaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddConsultaAsync(ConsultaDto consulta)
        {
            try
            {
                var newConsulta = Consulta.FromDto(consulta);
                await _dbContext.Consultas.AddAsync(newConsulta);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al agregar consulta", ex);
                return 0;
            }
        }

        public async Task<int> UpdateConsultaAsync(ConsultaDto consulta)
        {
            try
            {
                var result = await _dbContext.Consultas.Where(e => e.ConsultaCodigo == consulta.ConsultaCodigo).FirstAsync();
                result.Fecha = consulta.Fecha;
                result.Estado = consulta.Estado;
                result.Costo = consulta.Costo;
                result.Motivo = consulta.Motivo;
                result.Descripcion = consulta.Descripcion;
                result.CodigoPaciente = consulta.CodigoPaciente;
                result.IDConsultorio = consulta.IDConsultorio;
                result.IDAutorizacion = consulta.IDAutorizacion;
                result.CodigoDocumentoMedico = consulta.CodigoDocumentoMedico;
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al actualizar consulta", ex);
                return 0;
            }
        }

        public async Task<int> RemoveConsultaAsync(string consultaCodigo)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var consulta = await _dbContext.Consultas.FirstAsync(e => e.ConsultaCodigo == consultaCodigoInt);
                _dbContext.Consultas.Remove(consulta);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar consulta", ex);
                return 0;
            }
        }

        public async Task<int> AddConsultaServicioAsync(string consultaCodigo, string servicioCodigo)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var consulta = await _dbContext.Consultas.Include(c => c.ConsultaServicios).FirstAsync(e => e.ConsultaCodigo == consultaCodigoInt);
                consulta.ConsultaServicios.Add(new ConsultaServicio { ConsultaCodigo = consultaCodigoInt, ServicioCodigo = int.Parse(servicioCodigo) });
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al agregar servicio a consulta", ex);
                return 0;
            }
        }

        public async Task<int> RemoveConsultaServicioAsync(string consultaCodigo, string servicioCodigo)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var consulta = await _dbContext.Consultas.Include(c => c.ConsultaServicios).FirstAsync(e => e.ConsultaCodigo == consultaCodigoInt);
                var consultaServicio = consulta.ConsultaServicios.First(cs => cs.ServicioCodigo == int.Parse(servicioCodigo));
                consulta.ConsultaServicios.Remove(consultaServicio);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar servicio de consulta", ex);
                return 0;
            }
        }

        public async Task<List<Servicio>> GetConsultaServiciosAsync(string consultaCodigo)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var servicios = await _dbContext.Consultas
                    .Where(e => e.ConsultaCodigo == consultaCodigoInt)
                    .Include(e => e.ConsultaServicios)
                    .ThenInclude(cs => cs.Servicio)
                    .SelectMany(e => e.ConsultaServicios.Select(cs => cs.Servicio))
                    .ToListAsync();
                return servicios;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener servicios de consulta", ex);
                return new List<Servicio>();
            }
        }

        public async Task<int> AddConsultaProductoAsync(string consultaCodigo, uint idProducto, int cantidad)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var consulta = await _dbContext.Consultas.Include(c => c.PrescripcionProductos).FirstAsync(e => e.ConsultaCodigo == consultaCodigoInt);
                consulta.PrescripcionProductos.Add(new PrescripcionProducto { ConsultaCodigo = consultaCodigoInt, IDProducto = (int)idProducto, Cantidad = cantidad });
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al agregar producto a consulta", ex);
                return 0;
            }
        }

        public async Task<int> RemoveConsultaProductoAsync(string consultaCodigo, uint idProducto, int cantidad)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var consulta = await _dbContext.Consultas.Include(c => c.PrescripcionProductos).FirstAsync(e => e.ConsultaCodigo == consultaCodigoInt);
                var prescripcionProducto = consulta.PrescripcionProductos.First(pp => pp.IDProducto == (int)idProducto && pp.Cantidad == cantidad);
                consulta.PrescripcionProductos.Remove(prescripcionProducto);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar producto de consulta", ex);
                return 0;
            }
        }

        public async Task<List<ProductoDto>> GetConsultaProductosAsync(string consultaCodigo)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var productos = await _dbContext.Consultas
                    .Where(e => e.ConsultaCodigo == consultaCodigoInt)
                    .Include(e => e.PrescripcionProductos)
                    .ThenInclude(pp => pp.Producto)
                    .SelectMany(e => e.PrescripcionProductos.Select(pp => ProductoDto.FromModel(pp.Producto)))
                    .ToListAsync();
                return productos;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener productos de consulta", ex);
                return new List<ProductoDto>();
            }
        }

        public async Task<int> AddConsultaAfeccionAsync(string consultaCodigo, uint idAfeccion)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var consulta = await _dbContext.Consultas.Include(c => c.ConsultaAfecciones).FirstAsync(e => e.ConsultaCodigo == consultaCodigoInt);
                consulta.ConsultaAfecciones.Add(new ConsultaAfeccion { ConsultaCodigo = consultaCodigoInt, IDAfeccion = (int)idAfeccion });
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al agregar afección a consulta", ex);
                return 0;
            }
        }

        public async Task<int> RemoveConsultaAfeccionAsync(string consultaCodigo, uint idAfeccion)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var consulta = await _dbContext.Consultas.Include(c => c.ConsultaAfecciones).FirstAsync(e => e.ConsultaCodigo == consultaCodigoInt);
                var consultaAfeccion = consulta.ConsultaAfecciones.First(ca => ca.IDAfeccion == (int)idAfeccion);
                consulta.ConsultaAfecciones.Remove(consultaAfeccion);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar afección de consulta", ex);
                return 0;
            }
        }

        public async Task<List<Afeccion>> GetConsultaAfeccionesAsync(string consultaCodigo)
        {
            try
            {
                int consultaCodigoInt = int.Parse(consultaCodigo); // Convertir el string a int
                var afecciones = await _dbContext.Consultas
                    .Where(e => e.ConsultaCodigo == consultaCodigoInt)
                    .Include(e => e.ConsultaAfecciones)
                    .ThenInclude(ca => ca.Afeccion)
                    .SelectMany(e => e.ConsultaAfecciones.Select(ca => ca.Afeccion))
                    .ToListAsync();
                return afecciones;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener afecciones de consulta", ex);
                return new List<Afeccion>();
            }
        }

        public async Task<List<ConsultaDto>> GetConsultasAsync()
        {
            try
            {
                var consultas = await _dbContext.Consultas.ToListAsync();
                return consultas.Select(e => ConsultaDto.FromModel(e)).ToList();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener consultas", ex);
                return new List<ConsultaDto>();
            }
        }
    }
}
