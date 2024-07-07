using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using HospitalCore_core.Utilities;

namespace HospitalCore_core.Services
{
    public class CuentaCobrarService : ICuentaCobrarService
    {
        private readonly HospitalCore _context;
        private readonly LogManager<CuentaCobrarService> _logManager = new();

        public CuentaCobrarService(HospitalCore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CuentaCobrarDto>> GetCuentasCobrar()
        {
            try
            {
                var cuentas = await _context.CuentaCobrars
                    .Select(c => new CuentaCobrarDto
                    {
                        Idcuenta = c.Idcuenta,
                        Balance = c.Balance,
                        Estado = c.Estado,
                        CodigoPaciente = c.CodigoPaciente
                    }).ToListAsync();

                _logManager.LogInfo("Lista de cuentas por cobrar obtenida exitosamente.");
                return cuentas;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al obtener la lista de cuentas por cobrar.", ex);
                throw new Exception("Error retrieving cuentas por cobrar list.", ex);
            }
        }

        public async Task<CuentaCobrarDto?> GetCuentaCobrarById(int id)
        {
            try
            {
                var cuenta = await _context.CuentaCobrars.FindAsync(id);

                if (cuenta == null)
                    return null;

                _logManager.LogInfo($"Cuenta por cobrar con ID {id} obtenida exitosamente.");
                return new CuentaCobrarDto
                {
                    Idcuenta = cuenta.Idcuenta,
                    Balance = cuenta.Balance,
                    Estado = cuenta.Estado,
                    CodigoPaciente = cuenta.CodigoPaciente
                };
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al obtener cuenta por cobrar con ID {id}.", ex);
                throw new Exception($"Error retrieving cuenta por cobrar with ID {id}.", ex);
            }
        }

        public async Task<CuentaCobrarDto> CreateCuentaCobrar(CuentaCobrarDto cuentaCobrarDto)
        {
            try
            {
                var cuenta = new CuentaCobrar
                {
                    Balance = cuentaCobrarDto.Balance,
                    Estado = cuentaCobrarDto.Estado,
                    CodigoPaciente = cuentaCobrarDto.CodigoPaciente
                };

                _context.CuentaCobrars.Add(cuenta);
                await _context.SaveChangesAsync();

                cuentaCobrarDto.Idcuenta = cuenta.Idcuenta;
                _logManager.LogInfo($"Cuenta por cobrar creada exitosamente con ID {cuenta.Idcuenta}.");
                return cuentaCobrarDto;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al crear cuenta por cobrar.", ex);
                throw new Exception("Error creating cuenta por cobrar.", ex);
            }
        }

        public async Task<CuentaCobrarDto?> UpdateCuentaCobrar(int id, CuentaCobrarDto cuentaCobrarDto)
        {
            try
            {
                var cuenta = await _context.CuentaCobrars.FindAsync(id);

                if (cuenta == null)
                    return null;

                cuenta.Balance = cuentaCobrarDto.Balance;
                cuenta.Estado = cuentaCobrarDto.Estado;
                cuenta.CodigoPaciente = cuentaCobrarDto.CodigoPaciente;

                await _context.SaveChangesAsync();
                _logManager.LogInfo($"Cuenta por cobrar con ID {id} actualizada exitosamente.");
                return cuentaCobrarDto;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al actualizar cuenta por cobrar con ID {id}.", ex);
                throw new Exception($"Error updating cuenta por cobrar with ID {id}.", ex);
            }
        }

        public async Task<bool> DeleteCuentaCobrar(int id)
        {
            try
            {
                var cuenta = await _context.CuentaCobrars.FindAsync(id);

                if (cuenta == null)
                    return false;

                _context.CuentaCobrars.Remove(cuenta);
                await _context.SaveChangesAsync();
                _logManager.LogInfo($"Cuenta por cobrar con ID {id} eliminada exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al eliminar cuenta por cobrar con ID {id}.", ex);
                throw new Exception($"Error deleting cuenta por cobrar with ID {id}.", ex);
            }
        }
    }
}
