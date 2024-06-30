using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HospitalCore_core.Services;

public class AseguradoraService(HospitalCore dbContext) : IAseguradoraService
{
    private readonly LogManager<AseguradoraService> _logManager = new();

    public async Task<int> CreateAseguradora(Aseguradora aseguradora)
    {
        try
        {
            if (AseguradoraExists((uint)aseguradora.Idaseguradora))
            {
                return 0;
            }

            dbContext.Aseguradoras.Add(aseguradora);
            await dbContext.SaveChangesAsync();
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
            return await dbContext.Aseguradoras.FindAsync(id);
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
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AseguradoraExists((uint)aseguradora.Idaseguradora))
                {
                    return 0;
                }
                throw;
            }
            return 1;
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
               return 0;
           }

           dbContext.Aseguradoras.Remove(aseguradora);
           await dbContext.SaveChangesAsync();

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
