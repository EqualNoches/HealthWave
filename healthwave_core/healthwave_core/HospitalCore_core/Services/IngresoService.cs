using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core.Services
{
    public class IngresoService : IIngresoService
    {
        private readonly HospitalCore _dbContext;
        private readonly LogManager<IngresoService> _logManager = new();

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
                _logManager.LogInfo("AddIngresoAsync executed successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while trying to add an Ingreso.", ex);
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
                    _logManager.LogInfo($"Ingreso with ID {ingresoDto.IDIngreso} not found.");
                    return 0;
                }

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
                _logManager.LogInfo($"UpdateIngresoAsync executed successfully for ID {ingresoDto.IDIngreso}.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to update Ingreso with ID {ingresoDto.IDIngreso}.", ex);
                throw;
            }
        }

        public async Task<int> DeleteIngresoAsync(int idIngreso)
        {
            try
            {
                if (idIngreso <= 0)
                {
                    _logManager.LogInfo($"Invalid idIngreso: {idIngreso}");
                    return 0;
                }

                var ingreso = await _dbContext.Ingresos.FindAsync(idIngreso);
                if (ingreso == null)
                {
                    _logManager.LogInfo($"Ingreso with ID {idIngreso} not found.");
                    return 0;
                }

                _dbContext.Ingresos.Remove(ingreso);
                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo($"DeleteIngresoAsync executed successfully for ID {idIngreso}.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to delete Ingreso with ID {idIngreso}.", ex);
                throw;
            }
        }

        public async Task<List<IngresoDto>> GetIngresosAsync()
        {
            try
            {
                var ingresos = await _dbContext.Ingresos.Select(i => IngresoDto.FromModel(i)).ToListAsync();
                _logManager.LogInfo("GetIngresosAsync executed successfully.");
                return ingresos;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while trying to get Ingresos data.", ex);
                throw;
            }
        }

        public async Task<IngresoDto?> GetIngresoByIdAsync(int idIngreso)
        {
            try
            {
                if (idIngreso <= 0)
                {
                    _logManager.LogInfo($"Invalid idIngreso: {idIngreso}");
                    return null;
                }

                var ingreso = await _dbContext.Ingresos
                    .Where(i => i.IDIngreso == idIngreso)
                    .Select(i => IngresoDto.FromModel(i)).FirstOrDefaultAsync();

                if (ingreso == null)
                {
                    _logManager.LogInfo($"Ingreso with ID {idIngreso} not found.");
                    return null;
                }

                _logManager.LogInfo($"GetIngresoByIdAsync executed successfully for ID {idIngreso}.");
                return ingreso;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to get Ingreso with ID {idIngreso}.", ex);
                throw;
            }
        }

        public async Task<int> AddIngresoAfeccionAsync(int idIngreso, int idAfeccion)
        {
            try
            {
                if (idIngreso <= 0 || idAfeccion <= 0)
                {
                    _logManager.LogInfo($"Invalid idIngreso: {idIngreso} or idAfeccion: {idAfeccion}");
                    return 0;
                }

                var ingreso = await _dbContext.Ingresos
                    .Include(i => i.Afecciones)
                    .FirstOrDefaultAsync(i => i.IDIngreso == idIngreso);

                if (ingreso == null)
                {
                    _logManager.LogInfo($"Ingreso with ID {idIngreso} not found.");
                    return 0;
                }

                var afeccion = await _dbContext.Afeccions.FindAsync(idAfeccion);
                if (afeccion == null)
                {
                    _logManager.LogInfo($"Afeccion with ID {idAfeccion} not found.");
                    return 0;
                }

                ingreso.Afecciones.Add(afeccion);
                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo($"AddIngresoAfeccionAsync executed successfully for Ingreso ID {idIngreso} and Afeccion ID {idAfeccion}.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to add Afeccion to Ingreso with ID {idIngreso}.", ex);
                throw;
            }
        }

        public async Task<int> RemoveIngresoAfeccionAsync(int idIngreso, int idAfeccion)
        {
            try
            {
                if (idIngreso <= 0 || idAfeccion <= 0)
                {
                    _logManager.LogInfo($"Invalid idIngreso: {idIngreso} or idAfeccion: {idAfeccion}");
                    return 0;
                }

                var ingreso = await _dbContext.Ingresos
                    .Include(i => i.Afecciones)
                    .FirstOrDefaultAsync(i => i.IDIngreso == idIngreso);

                if (ingreso == null)
                {
                    _logManager.LogInfo($"Ingreso with ID {idIngreso} not found.");
                    return 0;
                }

                var afeccion = ingreso.Afecciones.FirstOrDefault(a => a.IdAfeccion == idAfeccion);
                if (afeccion == null)
                {
                    _logManager.LogInfo($"Afeccion with ID {idAfeccion} not found in Ingreso ID {idIngreso}.");
                    return 0;
                }

                ingreso.Afecciones.Remove(afeccion);
                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo($"RemoveIngresoAfeccionAsync executed successfully for Ingreso ID {idIngreso} and Afeccion ID {idAfeccion}.");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to remove Afeccion from Ingreso with ID {idIngreso}.", ex);
                throw;
            }
        }

        public async Task<List<Afeccion>> GetIngresoAfeccionesAsync(int idIngreso)
        {
            try
            {
                if (idIngreso <= 0)
                {
                    _logManager.LogInfo($"Invalid idIngreso: {idIngreso}");
                    return new List<Afeccion>();
                }

                var ingreso = await _dbContext.Ingresos
                    .Include(i => i.Afecciones)
                    .FirstOrDefaultAsync(i => i.IDIngreso == idIngreso);

                if (ingreso == null)
                {
                    _logManager.LogInfo($"Ingreso with ID {idIngreso} not found.");
                    return new List<Afeccion>();
                }

                _logManager.LogInfo($"GetIngresoAfeccionesAsync executed successfully for Ingreso ID {idIngreso}.");
                return ingreso.Afecciones.ToList();
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to get Afecciones for Ingreso with ID {idIngreso}.", ex);
                throw;
            }
        }
    }
}
