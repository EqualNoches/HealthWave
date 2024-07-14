using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class CuentaCobrarService : ICuentaCobrarService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<CuentaCobrarService> _logHandler = new();

        public CuentaCobrarService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddCuentaCobrarAsync(CuentaCobrarDto cuentaCobrarDto)
        {
            try
            {
                var cuentaCobrar = new CuentaCobrar
                {
                    Balance = cuentaCobrarDto.Balance,
                    Estado = cuentaCobrarDto.Estado,
                    CodigoPaciente = cuentaCobrarDto.CodigoPaciente
                };

                _dbContext.CuentasCobrar.Add(cuentaCobrar);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Cuenta por cobrar agregada exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al agregar la cuenta por cobrar.", ex);
                return 0;
            }
        }

        public async Task<List<CuentaCobrarDto>> GetCuentasCobrarAsync()
        {
            try
            {
                return await _dbContext.CuentasCobrar
                    .Select(c => CuentaCobrarDto.FromModel(c))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al obtener las cuentas por cobrar.", ex);
                return new List<CuentaCobrarDto>();
            }
        }

        public async Task<int> UpdateCuentaCobrarAsync(CuentaCobrarDto cuentaCobrarDto)
        {
            try
            {
                var cuentaCobrar = await _dbContext.CuentasCobrar.FindAsync(cuentaCobrarDto.IDCuenta);
                if (cuentaCobrar == null)
                {
                    _logHandler.LogInfo("Cuenta por cobrar no encontrada.");
                    return 0;
                }

                cuentaCobrar.Balance = cuentaCobrarDto.Balance;
                cuentaCobrar.Estado = cuentaCobrarDto.Estado;
                cuentaCobrar.CodigoPaciente = cuentaCobrarDto.CodigoPaciente;

                _dbContext.CuentasCobrar.Update(cuentaCobrar);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Cuenta por cobrar actualizada exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al actualizar la cuenta por cobrar.", ex);
                return 0;
            }
        }

        public async Task<int> DeleteCuentaCobrarAsync(int idCuenta)
        {
            try
            {
                var cuentaCobrar = await _dbContext.CuentasCobrar.FindAsync(idCuenta);
                if (cuentaCobrar == null)
                {
                    _logHandler.LogInfo("Cuenta por cobrar no encontrada.");
                    return 0;
                }

                _dbContext.CuentasCobrar.Remove(cuentaCobrar);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Cuenta por cobrar eliminada exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al eliminar la cuenta por cobrar.", ex);
                return 0;
            }
        }

        public async Task<CuentaCobrarDto?> GetCuentaCobrarByIdAsync(int idCuenta)
        {
            try
            {
                var cuentaCobrar = await _dbContext.CuentasCobrar.FindAsync(idCuenta);
                if (cuentaCobrar == null)
                {
                    _logHandler.LogInfo("Cuenta por cobrar no encontrada.");
                    return null;
                }

                return CuentaCobrarDto.FromModel(cuentaCobrar);
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al obtener la cuenta por cobrar por ID.", ex);
                return null;
            }
        }
    }
}
