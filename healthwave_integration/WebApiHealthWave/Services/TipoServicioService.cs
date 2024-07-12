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
    public class TipoServicioService : ITipoServicioService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<TipoServicioService> _logHandler = new();

        public TipoServicioService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddTipoServicioAsync(TipoServicioDto tipoServicioDto)
        {
            try
            {
                var tipoServicio = new TipoServicio
                {
                    Nombre = tipoServicioDto.Nombre
                };

                _dbContext.TipoServicios.Add(tipoServicio);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("TipoServicio added successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to add TipoServicio.", ex);
                throw;
            }
        }

        public async Task<List<TipoServicioDto>> GetTipoServiciosAsync()
        {
            try
            {
                var tipoServicios = await _dbContext.TipoServicios
                    .Select(ts => new TipoServicioDto
                    {
                        TipoServicioId = ts.TipoServicioId,
                        Nombre = ts.Nombre
                    })
                    .ToListAsync();

                _logHandler.LogInfo("TipoServicios retrieved successfully.");
                return tipoServicios;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve TipoServicios.", ex);
                throw;
            }
        }

        public async Task<int> UpdateTipoServicioAsync(TipoServicioDto tipoServicioDto)
        {
            try
            {
                var tipoServicio = await _dbContext.TipoServicios.FindAsync(tipoServicioDto.TipoServicioId);

                if (tipoServicio == null)
                {
                    _logHandler.LogInfo("TipoServicio not found.");
                    return 0;
                }

                tipoServicio.Nombre = tipoServicioDto.Nombre;

                _dbContext.TipoServicios.Update(tipoServicio);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("TipoServicio updated successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to update TipoServicio.", ex);
                throw;
            }
        }

        public async Task<int> DeleteTipoServicioAsync(uint idTipoServicio)
        {
            try
            {
                var tipoServicio = await _dbContext.TipoServicios.FindAsync(idTipoServicio);
                if (tipoServicio == null)
                {
                    _logHandler.LogInfo("TipoServicio not found.");
                    return 0;
                }

                _dbContext.TipoServicios.Remove(tipoServicio);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("TipoServicio deleted successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to delete TipoServicio.", ex);
                throw;
            }
        }
    }
}

