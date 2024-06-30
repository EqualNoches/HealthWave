using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core.Services
{
    public class IngresoService : IIngresoService
    {
        private readonly HospitalCore _dbContext;

        public IngresoService(HospitalCore dbContext)
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
                    Idautorizacion = ingresoDto.Idautorizacion
                };

                _dbContext.Ingresos.Add(ingreso);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateIngresoAsync(IngresoDto ingresoDto)
        {
            try
            {
                var ingreso = await _dbContext.Ingresos.FindAsync(ingresoDto.IDIngreso);
                if (ingreso == null) return 0;

                ingreso.CostoEstancia = ingresoDto.CostoEstancia;
                ingreso.FechaIngreso = ingresoDto.FechaIngreso;
                ingreso.FechaAlta = ingresoDto.FechaAlta;
                ingreso.NumSala = ingresoDto.NumSala;
                ingreso.CodigoPaciente = ingresoDto.CodigoPaciente;
                ingreso.CodigoDocumentoMedico = ingresoDto.CodigoDocumentoMedico;
                ingreso.ConsultaCodigo = ingresoDto.ConsultaCodigo;
                ingreso.Idautorizacion = ingresoDto.Idautorizacion;

                _dbContext.Ingresos.Update(ingreso);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteIngresoAsync(int idIngreso)
        {
            try
            {
                var ingreso = await _dbContext.Ingresos.FindAsync(idIngreso);
                if (ingreso == null) return 0;

                _dbContext.Ingresos.Remove(ingreso);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<IngresoDto>> GetIngresosAsync()
        {
            try
            {
                return await _dbContext.Ingresos.Select(i => IngresoDto.FromModel(i)).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IngresoDto?> GetIngresoByIdAsync(int idIngreso)
        {
            try
            {
                return await _dbContext.Ingresos
                    .Where(i => i.IDIngreso == idIngreso)
                    .Select(i => IngresoDto.FromModel(i)).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddIngresoAfeccionAsync(int idIngreso, int idAfeccion)
        {
            var ingreso = await _dbContext.Ingresos
                .Include(i => i.Afecciones)
                .FirstOrDefaultAsync(i => i.IDIngreso == idIngreso);

            if (ingreso == null) return 0;

            var afeccion = await _dbContext.Afeccions.FindAsync(idAfeccion);
            if (afeccion == null) return 0;

            ingreso.Afecciones.Add(afeccion);
            await _dbContext.SaveChangesAsync();
            return 1;
        }

        public async Task<int> RemoveIngresoAfeccionAsync(int idIngreso, int idAfeccion)
        {
            var ingreso = await _dbContext.Ingresos
                .Include(i => i.Afecciones)
                .FirstOrDefaultAsync(i => i.IDIngreso == idIngreso);

            if (ingreso == null) return 0;

            var afeccion = ingreso.Afecciones.FirstOrDefault(a => a.IdAfeccion == idAfeccion);
            if (afeccion == null) return 0;

            ingreso.Afecciones.Remove(afeccion);
            await _dbContext.SaveChangesAsync();
            return 1;
        }

        public async Task<List<Afeccion>> GetIngresoAfeccionesAsync(int idIngreso)
        {
            var ingreso = await _dbContext.Ingresos
                .Include(i => i.Afecciones)
                .FirstOrDefaultAsync(i => i.IDIngreso == idIngreso);

            return ingreso?.Afecciones.ToList() ?? new List<Afeccion>();
        }
    }
}
