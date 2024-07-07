using HospitalCore_core.Models;
using HospitalCore_core.Context;
using HospitalCore_core.Utilities;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Services
{
    public class ConsultorioService : IConsultorioService
    {
        private readonly HospitalCore _dbContext;
        private readonly LogManager<ConsultorioService> _logManager = new();

        public ConsultorioService(HospitalCore dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ConsultorioDTO>> GetConsultoriosAsync()
        {
            try
            {
                var consultorios = await _dbContext.Consultorios.ToListAsync();
                // Mapping to DTO
                var consultorioDTOs = consultorios.Select(c => new ConsultorioDTO
                {
                    IDConsultorio = c.IDConsultorio,
                    Nombre = c.Nombre,
                    Direccion = c.Direccion,
                    Telefono = c.Telefono
                });
                _logManager.LogInfo("Successfully retrieved consultorios.");
                return consultorioDTOs;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error retrieving consultorios.", ex);
                throw;
            }
        }

        public async Task<ConsultorioDTO> GetConsultorioByIdAsync(int idConsultorio)
        {
            try
            {
                var consultorio = await _dbContext.Consultorios.FindAsync(idConsultorio);
                if (consultorio == null)
                {
                    _logManager.LogInfo($"Consultorio with ID {idConsultorio} not found.");
                    return null;
                }

                var consultorioDTO = new ConsultorioDTO
                {
                    IDConsultorio = consultorio.IDConsultorio,
                    Nombre = consultorio.Nombre,
                    Direccion = consultorio.Direccion,
                    Telefono = consultorio.Telefono
                };
                _logManager.LogInfo($"Successfully retrieved consultorio with ID {idConsultorio}.");
                return consultorioDTO;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error retrieving consultorio with ID {idConsultorio}.", ex);
                throw;
            }
        }

        public async Task<int> CreateConsultorioAsync(ConsultorioDTO consultorioDTO)
        {
            try
            {
                var consultorio = new Consultorio
                {
                    Nombre = consultorioDTO.Nombre,
                    Direccion = consultorioDTO.Direccion,
                    Telefono = consultorioDTO.Telefono
                };

                _dbContext.Consultorios.Add(consultorio);
                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Successfully created consultorio with ID {consultorio.IDConsultorio}.");
                return consultorio.IDConsultorio;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error creating consultorio.", ex);
                throw;
            }
        }

        public async Task<int> UpdateConsultorioAsync(ConsultorioDTO consultorioDTO)
        {
            try
            {
                var consultorio = await _dbContext.Consultorios.FindAsync(consultorioDTO.IDConsultorio);
                if (consultorio == null)
                {
                    _logManager.LogInfo($"Consultorio with ID {consultorioDTO.IDConsultorio} not found.");
                    return 0;
                }

                consultorio.Nombre = consultorioDTO.Nombre;
                consultorio.Direccion = consultorioDTO.Direccion;
                consultorio.Telefono = consultorioDTO.Telefono;

                _dbContext.Consultorios.Update(consultorio);
                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Successfully updated consultorio with ID {consultorio.IDConsultorio}.");
                return consultorio.IDConsultorio;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error updating consultorio with ID {consultorioDTO.IDConsultorio}.", ex);
                throw;
            }
        }

        public async Task<int> DeleteConsultorioAsync(int idConsultorio)
        {
            try
            {
                var consultorio = await _dbContext.Consultorios.FindAsync(idConsultorio);
                if (consultorio == null)
                {
                    _logManager.LogInfo($"Consultorio with ID {idConsultorio} not found.");
                    return 0;
                }

                _dbContext.Consultorios.Remove(consultorio);
                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Successfully deleted consultorio with ID {idConsultorio}.");
                return idConsultorio;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error deleting consultorio with ID {idConsultorio}.", ex);
                throw;
            }
        }
    }
}
