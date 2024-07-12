using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class ServicioService : IServicioService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<ServicioService> _logHandler = new();

        public ServicioService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateServicioAsync(ServicioDto servicioDto)
        {
            try
            {
                var servicio = new Servicio
                {
                    ServicioCodigo = servicioDto.ServicioCodigo,
                    Nombre = servicioDto.Nombre,
                    Descripción = servicioDto.Descripción,
                    TipoServicio = servicioDto.TipoServicio,
                    Costo = servicioDto.Costo,
                    IDAseguradora = servicioDto.IDAseguradora
                };

                _dbContext.Servicios.Add(servicio);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Servicio created successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to create servicio.", ex);
                throw;
            }
        }

        public async Task<List<ServicioDto>> GetServiciosAsync()
        {
            try
            {
                return await _dbContext.Servicios
                    .Select(s => new ServicioDto
                    {
                        ServicioCodigo = s.ServicioCodigo,
                        Nombre = s.Nombre,
                        Descripción = s.Descripción,
                        TipoServicio = s.TipoServicio,
                        Costo = s.Costo,
                        IDAseguradora = s.IDAseguradora
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve servicios.", ex);
                throw;
            }
        }

        public async Task<int> UpdateServicioAsync(ServicioDto servicioDto)
        {
            try
            {
                var servicio = await _dbContext.Servicios.FindAsync(servicioDto.ServicioCodigo);
                if (servicio == null)
                {
                    _logHandler.LogInfo("Servicio not found.");
                    return 0;
                }

                servicio.Nombre = servicioDto.Nombre;
                servicio.Descripción = servicioDto.Descripción;
                servicio.TipoServicio = servicioDto.TipoServicio;
                servicio.Costo = servicioDto.Costo;
                servicio.IDAseguradora = servicioDto.IDAseguradora;

                _dbContext.Servicios.Update(servicio);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Servicio updated successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to update servicio.", ex);
                throw;
            }
        }

        public async Task<int> DeleteServicioAsync(string servicioCodigo)
        {
            try
            {
                var servicio = await _dbContext.Servicios.FindAsync(servicioCodigo);
                if (servicio == null)
                {
                    _logHandler.LogInfo("Servicio not found.");
                    return 0;
                }

                _dbContext.Servicios.Remove(servicio);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Servicio deleted successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to delete servicio.", ex);
                throw;
            }
        }
    }
}
