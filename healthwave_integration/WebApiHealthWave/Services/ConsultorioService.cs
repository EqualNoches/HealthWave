using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class ConsultorioService : IConsultorioService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<ConsultorioService> _logManager = new();

        public ConsultorioService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Consultorio>> GetConsultoriosAsync()
        {
            try
            {
                return await _dbContext.Consultorios.ToListAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener los consultorios", ex);
                throw;
            }
        }

        public async Task<Consultorio> GetConsultorioByIdAsync(uint idConsultorio)
        {
            try
            {
                return await _dbContext.Consultorios.FindAsync((int)idConsultorio);
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al obtener el consultorio con ID {idConsultorio}", ex);
                throw;
            }
        }

        public async Task<int> CreateConsultorioAsync(Consultorio consultorio)
        {
            try
            {
                _dbContext.Consultorios.Add(consultorio);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al crear un consultorio", ex);
                throw;
            }
        }

        public async Task<int> UpdateConsultorioAsync(Consultorio consultorio)
        {
            try
            {
                _dbContext.Entry(consultorio).State = EntityState.Modified;
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al actualizar un consultorio", ex);
                throw;
            }
        }

        public async Task<int> DeleteConsultorioAsync(uint idConsultorio)
        {
            try
            {
                var consultorio = await _dbContext.Consultorios.FindAsync((int)idConsultorio);
                if (consultorio == null)
                {
                    return 0;
                }

                _dbContext.Consultorios.Remove(consultorio);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al eliminar el consultorio con ID {idConsultorio}", ex);
                throw;
            }
        }
    }
}
