using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;


namespace HospitalCore_core.Services
{
    public class AseguradoraService(HospitalCore dbContext) : IAseguradoraService
    {
        private readonly LogManager<AseguradoraService> _logManager = new();

        public async Task<int> CreateAseguradora(Aseguradora aseguradora)
        {
            try
            {
                if (AseguradoraExists((uint)aseguradora.Idaseguradora))
                {
                    _logManager.LogInfo($"La aseguradora con ID {aseguradora.Idaseguradora} ya existe.");
                    return 0;
                }

                dbContext.Aseguradoras.Add(aseguradora);
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo("La aseguradora fue creada correctamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al crear la aseguradora", ex);
                throw;
            }
        }

        public async Task<List<Aseguradora>> GetAllAseguradoras()
        {
            try
            {
                return await dbContext.Aseguradoras.ToListAsync();
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener las aseguradoras", ex);
                throw;
            }
        }

        public async Task<Aseguradora?> GetAseguradoraById(int id)
        {
            try
            {
                var aseguradora = await dbContext.Aseguradoras.FindAsync(id);
                if (aseguradora == null)
                {
                    _logManager.LogInfo($"No se encontró la aseguradora con ID {id}.");
                }
                return aseguradora;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al obtener la aseguradora con ID: {id}", ex);
                throw;
            }
        }

        public async Task<int> UpdateAseguradora(Aseguradora aseguradora)
        {
            try
            {
                dbContext.Entry(aseguradora).State = EntityState.Modified;

                try
                {
                    await dbContext.SaveChangesAsync();
                    _logManager.LogInfo($"La aseguradora con ID {aseguradora.Idaseguradora} fue actualizada correctamente.");
                    return 1;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AseguradoraExists((uint)aseguradora.Idaseguradora))
                    {
                        _logManager.LogInfo($"No se pudo encontrar la aseguradora con ID {aseguradora.Idaseguradora} para actualizar.");
                        return 0;
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al actualizar la aseguradora", ex);
                throw;
            }
        }

        public async Task<int> DeleteAseguradora(int id)
        {
            try
            {
                var aseguradora = await dbContext.Aseguradoras.FindAsync(id);
                if (aseguradora == null)
                {
                    _logManager.LogInfo($"No se encontró la aseguradora con ID {id} para eliminar.");
                    return 0;
                }

                dbContext.Aseguradoras.Remove(aseguradora);
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"La aseguradora con ID {id} fue eliminada correctamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al eliminar la aseguradora", ex);
                throw;
            }
        }

        private bool AseguradoraExists(uint id)
        {
            return dbContext.Aseguradoras.Any(e => e.Idaseguradora == id);
        }
    }
}
