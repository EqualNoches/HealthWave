using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Services
{
    public class ReservaServicioService : IReservaServicio
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ReservaServicioService> _logger;

        public ReservaServicioService(AppDbContext dbContext, ILogger<ReservaServicioService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<ReservaServicioDto>> GetReservaServiciosListAsync()
        {
            try
            {
                var reservaServicios = await _dbContext.ReservaServicios
                    .Select(rs => new ReservaServicioDto
                    {
                        ServicioCodigo = rs.ServicioCodigo,
                        CodigoPaciente = rs.CodigoPaciente
                    })
                    .ToListAsync();

                _logger.LogInformation("ReservaServicios retrieved successfully.");
                return reservaServicios;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrieve ReservaServicios.", ex);
                throw;
            }
        }

        public async Task<int> AddReservaServicioAsync(ReservaServicioDto reservaServicioDto)
        {
            try
            {
                var reservaServicio = new ReservaServicio
                {
                    ServicioCodigo = reservaServicioDto.ServicioCodigo,
                    CodigoPaciente = reservaServicioDto.CodigoPaciente
                };

                _dbContext.ReservaServicios.Add(reservaServicio);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("ReservaServicio added successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add ReservaServicio.", ex);
                throw;
            }
        }

        public async Task<int> UpdateReservaServicioAsync(ReservaServicioDto reservaServicioDto)
        {
            try
            {
                var reservaServicio = await _dbContext.ReservaServicios
                    .FirstOrDefaultAsync(rs => rs.ServicioCodigo == reservaServicioDto.ServicioCodigo && rs.CodigoPaciente == reservaServicioDto.CodigoPaciente);

                if (reservaServicio == null)
                {
                    _logger.LogInformation("ReservaServicio not found.");
                    return 0;
                }

                reservaServicio.ServicioCodigo = reservaServicioDto.ServicioCodigo;
                reservaServicio.CodigoPaciente = reservaServicioDto.CodigoPaciente;

                _dbContext.ReservaServicios.Update(reservaServicio);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("ReservaServicio updated successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update ReservaServicio.", ex);
                throw;
            }
        }

        public async Task<int> ToggleEstadoReservaServicioAsync(int idReserva)
        {
            try
            {
                var reservaServicio = await _dbContext.ReservaServicios.FindAsync(idReserva);
                if (reservaServicio == null)
                {
                    _logger.LogInformation("ReservaServicio not found.");
                    return 0;
                }

                // Assuming there's a status to toggle; adjust logic as needed
                // reservaServicio.Estado = !reservaServicio.Estado;

                _dbContext.ReservaServicios.Update(reservaServicio);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("ReservaServicio status toggled successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to toggle status of ReservaServicio.", ex);
                throw;
            }
        }

        public async Task<int> DeleteReservaServicioAsync(int idReserva)
        {
            try
            {
                var reservaServicio = await _dbContext.ReservaServicios.FindAsync(idReserva);
                if (reservaServicio == null)
                {
                    _logger.LogInformation("ReservaServicio not found.");
                    return 0;
                }

                _dbContext.ReservaServicios.Remove(reservaServicio);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("ReservaServicio deleted successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete ReservaServicio.", ex);
                throw;
            }
        }
    }
}