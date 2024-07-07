using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;


namespace HospitalCore_core.Services
{
    public class SalaService(HospitalCore dbContext) : ISalaService
    {
        private readonly LogManager<SalaService> _logManager = new LogManager<SalaService>();

        public async Task<int> CreateSalaAsync(SalaDto salaDto)
        {
            try
            {
                var sala = new Sala
                {
                    Estado = salaDto.Estado
                };

                dbContext.Salas.Add(sala);
                await dbContext.SaveChangesAsync();

                _logManager.LogInfo("Sala created successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error creating sala.", ex);
                throw;
            }
        }

        public async Task<List<SalaDto>> GetSalasAsync()
        {
            try
            {
                var salas = await dbContext.Salas
                    .Select(s => new SalaDto { NumSala = (uint)s.NumSala, Estado = s.Estado })
                    .ToListAsync();

                _logManager.LogInfo("Salas retrieved successfully.");
                return salas;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error retrieving salas.", ex);
                throw;
            }
        }

        public async Task<int> UpdateSalaEstadoAsync(int numSala)
        {
            try
            {
                var sala = await dbContext.Salas.FindAsync(numSala);
                if (sala == null)
                {
                    _logManager.LogInfo($"Sala with number {numSala} not found.");
                    return 0;
                }

                sala.Estado = sala.Estado == "D" ? "O" : "D";
                dbContext.Salas.Update(sala);
                await dbContext.SaveChangesAsync();

                _logManager.LogInfo($"Sala state updated successfully for number {numSala}.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error updating sala state for number {numSala}.", ex);
                throw;
            }
        }

        public async Task<int> DeleteSalaAsync(int numSala)
        {
            try
            {
                var sala = await dbContext.Salas.FindAsync(numSala);
                if (sala == null)
                {
                    _logManager.LogInfo($"Sala with number {numSala} not found for deletion.");
                    return 0;
                }

                dbContext.Salas.Remove(sala);
                await dbContext.SaveChangesAsync();

                _logManager.LogInfo($"Sala deleted successfully for number {numSala}.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error deleting sala with number {numSala}.", ex);
                throw;
            }
        }
    }
}
