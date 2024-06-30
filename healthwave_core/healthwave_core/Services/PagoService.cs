using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;
using HospitalCore_core.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace HospitalCore_core.Services;

public class PagoService(HospitalCore dbCore): IPagoService
{
    private readonly LogManager<ProductoService> _logManager = new();
    
    public async Task<int> CreatePago(Pago? pago)
    {
        try
        {
            dbCore.Pagos.Add(pago);
            await dbCore.SaveChangesAsync();
            return 1;
        }
        catch (SqlException ex)
        {
            _logManager.LogFatal("No se puedo guardar el pago",ex);
            throw;
        }
    }
    
    public async Task<IEnumerable<Pago?>> GetPagos()
    {
        try
        {
            return await dbCore.Pagos.ToListAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("No se pudieron traer los pagos", ex);
            throw;
        }
    }

    public async Task<Pago?> GetPagoById(int idPago)
    {
        try
        {
            return await dbCore.Pagos.FindAsync(idPago);
        }
        catch (Exception ex)
        {
            _logManager.LogFatal($"Error al obtener el pago{ex}", ex);
            throw;
        }
    }
    

    public async Task<int> UpdatePago(Pago pago)
    {
        try
        {
            dbCore.Entry(pago).State = EntityState.Modified;
            await dbCore.SaveChangesAsync();
            return 1;
        }
        catch (Exception ex)
        {
            _logManager.LogFatal($"No se pudo actualizar el pago", ex);
            throw;
        }
    }

    public async Task<int> DeletePago(uint idPago)
    {
        try
        {
            var pago = await dbCore.Pagos.FindAsync();
            if (pago == null)
                return 0;

            dbCore.Pagos.Remove(pago);
            await dbCore.SaveChangesAsync();
            return 1;
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error borrando este registro de pago", ex);
            throw;
        }
    }
}
