using HospitalCore_core.Models;
using HospitalCore_core.Context;
using HospitalCore_core.Utilities;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core.Services;

public class ConsultorioService(HospitalCore dbContext) : IConsultorioService
{
    private readonly LogManager<ConsultorioService> _logManager = new();

    public async Task<IEnumerable<Consultorio>> GetConsultoriosAsync()
    {
        try
        {
            return await dbContext.Consultorios.ToListAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogError("Something went wrong while trying to get Consultorios data.", ex);
            throw;
        }
    }

    public async Task<Consultorio> GetConsultorioByIdAsync(int idConsultorio)
    {
        try
        {
            return await dbContext.Consultorios.FindAsync(idConsultorio);
        }
        catch (Exception ex)
        {
            _logManager.LogError($"Something went wrong while trying to get Consultorio with ID {idConsultorio}.", ex);
            throw;
        }
    }

    public async Task<int> CreateConsultorioAsync(Consultorio consultorio)
    {
        try
        {
            dbContext.Consultorios.Add(consultorio);
            return await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogError("Something went wrong while trying to create a Consultorio.", ex);
            throw;
        }
    }

    public async Task<int> UpdateConsultorioAsync(Consultorio consultorio)
    {
        try
        {
            dbContext.Entry(consultorio).State = EntityState.Modified;
            return await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogError("Something went wrong while trying to update a Consultorio.", ex);
            throw;
        }
    }

    public async Task<int> DeleteConsultorioAsync(int idConsultorio)
    {
        try
        {
            var consultorio = await dbContext.Consultorios.FindAsync(idConsultorio);
            if (consultorio == null)
            {
                return 0;
            }

            dbContext.Consultorios.Remove(consultorio);
            return await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogError($"Something went wrong while trying to delete Consultorio with ID {idConsultorio}.", ex);
            throw;
        }
    }
}