using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core.Services
{
    public class CuentaCobrarService : ICuentaCobrarService
    {
        private readonly HospitalCore _context;

        public CuentaCobrarService(HospitalCore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CuentaCobrarDto>> GetCuentasCobrar()
        {
            return await _context.CuentaCobrars
                .Select(c => new CuentaCobrarDto
                {
                    Idcuenta = c.Idcuenta,
                    Balance = c.Balance,
                    Estado = c.Estado,
                    CodigoPaciente = c.CodigoPaciente
                }).ToListAsync();
        }

        public async Task<CuentaCobrarDto?> GetCuentaCobrarById(int id)
        {
            var cuenta = await _context.CuentaCobrars.FindAsync(id);

            if (cuenta == null)
                return null;

            return new CuentaCobrarDto
            {
                Idcuenta = cuenta.Idcuenta,
                Balance = cuenta.Balance,
                Estado = cuenta.Estado,
                CodigoPaciente = cuenta.CodigoPaciente
            };
        }

        public async Task<CuentaCobrarDto> CreateCuentaCobrar(CuentaCobrarDto cuentaCobrarDto)
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
            return cuentaCobrarDto;
        }

        public async Task<CuentaCobrarDto?> UpdateCuentaCobrar(int id, CuentaCobrarDto cuentaCobrarDto)
        {
            var cuenta = await _context.CuentaCobrars.FindAsync(id);

            if (cuenta == null)
                return null;

            cuenta.Balance = cuentaCobrarDto.Balance;
            cuenta.Estado = cuentaCobrarDto.Estado;
            cuenta.CodigoPaciente = cuentaCobrarDto.CodigoPaciente;

            await _context.SaveChangesAsync();
            return cuentaCobrarDto;
        }

        public async Task<bool> DeleteCuentaCobrar(int id)
        {
            var cuenta = await _context.CuentaCobrars.FindAsync(id);

            if (cuenta == null)
                return false;

            _context.CuentaCobrars.Remove(cuenta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
