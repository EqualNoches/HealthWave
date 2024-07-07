using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using HospitalCore_core.Utilities;

namespace HospitalCore_core.Services
{
    public class AfeccionService : IAfeccionService
    {
        private readonly HospitalCore _context;
        private readonly LogManager<AfeccionService> _logManager = new();

        public AfeccionService(HospitalCore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AfeccionDto>> GetAfecciones()
        {
            try
            {
                var afecciones = await _context.Afeccions
                    .Select(a => new AfeccionDto
                    {
                        IdAfeccion = a.IdAfeccion,
                        Nombre = a.Nombre,
                        Descripcion = a.Descripcion
                    }).ToListAsync();

                _logManager.LogInfo("GetAfecciones executed successfully.");
                return afecciones;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error in GetAfecciones: ", ex);
                throw;
            }
        }

        public async Task<AfeccionDto?> GetAfeccionById(int id)
        {
            try
            {
                var afeccion = await _context.Afeccions.FindAsync(id);

                if (afeccion == null)
                {
                    _logManager.LogInfo($"Afeccion with id {id} not found.");
                    return null;
                }

                _logManager.LogInfo($"GetAfeccionById executed successfully for id {id}.");
                return new AfeccionDto
                {
                    IdAfeccion = afeccion.IdAfeccion,
                    Nombre = afeccion.Nombre,
                    Descripcion = afeccion.Descripcion
                };
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error in GetAfeccionById for id {id}: ", ex);
                throw;
            }
        }

        public async Task<AfeccionDto> CreateAfeccion(AfeccionDto afeccionDto)
        {
            try
            {
                var afeccion = new Afeccion
                {
                    Nombre = afeccionDto.Nombre,
                    Descripcion = afeccionDto.Descripcion
                };

                _context.Afeccions.Add(afeccion);
                await _context.SaveChangesAsync();

                afeccionDto.IdAfeccion = afeccion.IdAfeccion;

                _logManager.LogInfo("CreateAfeccion executed successfully.");
                return afeccionDto;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error in CreateAfeccion: ", ex);
                throw;
            }
        }

        public async Task<AfeccionDto?> UpdateAfeccion(int id, AfeccionDto afeccionDto)
        {
            try
            {
                var afeccion = await _context.Afeccions.FindAsync(id);

                if (afeccion == null)
                {
                    _logManager.LogInfo($"Afeccion with id {id} not found.");
                    return null;
                }

                afeccion.Nombre = afeccionDto.Nombre;
                afeccion.Descripcion = afeccionDto.Descripcion;

                await _context.SaveChangesAsync();

                _logManager.LogInfo($"UpdateAfeccion executed successfully for id {id}.");
                return afeccionDto;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error in UpdateAfeccion for id {id}: ", ex);
                throw;
            }
        }

        public async Task<bool> DeleteAfeccion(int id)
        {
            try
            {
                var afeccion = await _context.Afeccions.FindAsync(id);

                if (afeccion == null)
                {
                    _logManager.LogInfo($"Afeccion with id {id} not found.");
                    return false;
                }

                _context.Afeccions.Remove(afeccion);
                await _context.SaveChangesAsync();

                _logManager.LogInfo($"DeleteAfeccion executed successfully for id {id}.");
                return true;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error in DeleteAfeccion for id {id}: ", ex);
                throw;
            }
        }
    }
}
