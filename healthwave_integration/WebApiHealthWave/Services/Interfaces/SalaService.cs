using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class SalaService : ISalaService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<SalaService> _logHandler = new();

        public SalaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateSalaAsync(SalaDto salaDto)
        {
            try
            {
                var sala = new Sala
                {
                    NumSala = salaDto.NumSala,
                    Estado = salaDto.Estado
                };

                _dbContext.Salas.Add(sala);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Sala created successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to create sala.", ex);
                throw;
            }
        }

        public async Task<List<SalaDto>> GetSalasAsync()
        {
            try
            {
                return await _dbContext.Salas
                    .Select(s => new SalaDto
                    {
                        NumSala = s.NumSala,
                        Estado = s.Estado
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve salas.", ex);
                throw;
            }
        }

        public async Task<int> UpdateSalaEstadoAsync(uint numSala)
        {
            try
            {
                var sala = await _dbContext.Salas.FindAsync(numSala);
                if (sala == null)
                {
                    _logHandler.LogInfo("Sala not found.");
                    return 0;
                }

                string nuevoEstado = sala.Estado == "Disponible" ? "Ocupada" : "Disponible";
                sala.Estado = nuevoEstado;
                _dbContext.Salas.Update(sala);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Sala estado updated successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to toggle estado of sala.", ex);
                throw;
            }
        }

        public async Task<int> DeleteSalaAsync(uint numSala)
        {
            try
            {
                var sala = await _dbContext.Salas.FindAsync(numSala);
                if (sala == null)
                {
                    _logHandler.LogInfo("Sala not found.");
                    return 0;
                }

                _dbContext.Salas.Remove(sala);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Sala deleted successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to delete sala.", ex);
                throw;
            }
        }
    }
}

