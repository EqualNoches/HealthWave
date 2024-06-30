using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NLog.Fluent;

namespace HospitalCore_core.Services;

public class SalaService(HospitalCore dbContext) : ISalaService
{
    private readonly LogManager<SalaService> _logManager = new LogManager<SalaService>();

    // Create Sala
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
            _logManager.LogInfo("Se agrego correctamente el registro");
            return 1;
        }
        catch (Exception ex)
        {
            _logManager.LogError("Error al tratar de crear la sala", ex);
            throw;
        }
    }

    public async Task<List<SalaDto>> GetSalasAsync()
    {
        try
        {
            return await dbContext.Salas.Select(s => new SalaDto { NumSala = (uint)s.NumSala, Estado = s.Estado }).ToListAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Erro al tratar de traer salas", ex);
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
                _logManager.LogInfo($"la sala no pudo ser encontrada");
                return 0;
            }
            sala.Estado = sala.Estado == "D" ? "O" : "D";
            dbContext.Salas.Update(sala);
            await dbContext.SaveChangesAsync();
            _logManager.LogInfo("Sala estado fue cambiado satisfactoriamente");
            return 1;
        }
        catch (Exception ex)
        {
            _logManager.LogFatal($"El estado de la sala no pudo ser modificado (Logic)", ex);
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
                return 0;
            }

            dbContext.Salas.Remove(sala);
            return await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogFatal($"Error al tratar de borrar el registro de la sala {numSala}", ex);
            throw;
        }

    }
}