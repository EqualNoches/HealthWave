using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class IngresoService : IIngresoService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<IngresoService> _logHandler = new();

        public IngresoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddIngresoAsync(IngresoDto ingresoDto)
        {
            try
            {
                var ingreso = new Ingreso
                {
                    CostoEstancia = ingresoDto.CostoEstancia,
                    FechaIngreso = ingresoDto.FechaIngreso,
                    FechaAlta = ingresoDto.FechaAlta,
                    NumSala = ingresoDto.NumSala,
                    CodigoPaciente = ingresoDto.CodigoPaciente,
                    CodigoDocumentoMedico = ingresoDto.CodigoDocumentoMedico,
                    ConsultaCodigo = ingresoDto.ConsultaCodigo,
                    IDAutorizacion = ingresoDto.IDAutorizacion
                };

                _dbContext.Ingresos.Add(ingreso);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Ingreso added successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to add ingreso.", ex);
                throw;
            }
        }

        public async Task<int> UpdateIngresoAsync(IngresoDto ingresoDto)
        {
            try
            {
                var ingreso = await _dbContext.Ingresos.FindAsync(ingresoDto.IDIngreso);

                if (ingreso == null)
                {
                    _logHandler.LogInfo("Ingreso not found.");
                    return 0;
                }

                ingreso.CodigoPaciente = ingresoDto.CodigoPaciente;
                ingreso.CodigoDocumentoMedico = ingresoDto.CodigoDocumentoMedico;
                ingreso.ConsultaCodigo = ingresoDto.ConsultaCodigo;
                ingreso.IDAutorizacion = ingresoDto.IDAutorizacion;
                ingreso.NumSala = ingresoDto.NumSala;
                ingreso.CostoEstancia = ingresoDto.CostoEstancia;
                ingreso.FechaIngreso = ingresoDto.FechaIngreso;
                ingreso.FechaAlta = ingresoDto.FechaAlta;

                _dbContext.Ingresos.Update(ingreso);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Ingreso updated successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to update ingreso.", ex);
                throw;
            }
        }

        public async Task<int> DeleteIngresoAsync(uint idIngreso)
        {
            try
            {
                var ingreso = await _dbContext.Ingresos.FindAsync((int)idIngreso);
                if (ingreso == null)
                {
                    _logHandler.LogInfo("Ingreso not found.");
                    return 0;
                }

                _dbContext.Ingresos.Remove(ingreso);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Ingreso deleted successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to delete ingreso.", ex);
                throw;
            }
        }

        public async Task<List<IngresoDto>> GetIngresosAsync()
        {
            try
            {
                return await _dbContext.Ingresos.Select(i => IngresoDto.FromModel(i)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve ingresos.", ex);
                throw;
            }
        }

        public async Task<IngresoDto?> GetIngresoByIdAsync(uint idIngreso)
        {
            try
            {
                return await _dbContext.Ingresos
                    .Where(i => i.IDIngreso == (int)idIngreso)
                    .Select(i => IngresoDto.FromModel(i)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve ingreso by ID.", ex);
                throw;
            }
        }

        public async Task<int> AddIngresoAfeccionAsync(uint idIngreso, uint idAfeccion)
        {
            try
            {
                var ingreso = await _dbContext.Ingresos
                    .Include(i => i.Afecciones)
                    .FirstOrDefaultAsync(i => i.IDIngreso == (int)idIngreso);

                if (ingreso == null) return 0;

                var afeccion = await _dbContext.Afecciones.FindAsync((int)idAfeccion);
                if (afeccion == null) return 0;

                ingreso.Afecciones.Add(afeccion);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Afección added to ingreso successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to add afección to ingreso.", ex);
                throw;
            }
        }

        public async Task<int> RemoveIngresoAfeccionAsync(uint idIngreso, uint idAfeccion)
        {
            try
            {
                var ingreso = await _dbContext.Ingresos
                    .Include(i => i.Afecciones)
                    .FirstOrDefaultAsync(i => i.IDIngreso == (int)idIngreso);

                if (ingreso == null) return 0;

                var afeccion = ingreso.Afecciones.FirstOrDefault(a => a.IDAfeccion == (int)idAfeccion);
                if (afeccion == null) return 0;

                ingreso.Afecciones.Remove(afeccion);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Afección removed from ingreso successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to remove afección from ingreso.", ex);
                throw;
            }
        }

        public async Task<List<Afeccion>> GetIngresoAfeccionesAsync(uint idIngreso)
        {
            try
            {
                var ingreso = await _dbContext.Ingresos
                    .Include(i => i.Afecciones)
                    .FirstOrDefaultAsync(i => i.IDIngreso == (int)idIngreso);

                return ingreso?.Afecciones.ToList() ?? new List<Afeccion>();
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve afecciones from ingreso.", ex);
                throw;
            }
        }
    }
}
