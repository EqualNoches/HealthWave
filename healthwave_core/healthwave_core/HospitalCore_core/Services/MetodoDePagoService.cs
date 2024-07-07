using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;

namespace HospitalCore_core.Services
{
    public class MetodoDePagoService : IMetodoDePagoService
    {
        private readonly HospitalCore _dbContext;
        private readonly LogManager<MetodoDePagoService> _logManager = new();

        public MetodoDePagoService(HospitalCore dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MetodoDePago> GetAllMetodosDePago()
        {
            try
            {
                var metodos = _dbContext.MetodosDePago.ToList();
                _logManager.LogInfo("GetAllMetodosDePago executed successfully.");
                return metodos;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while trying to get all MetodosDePago.", ex);
                throw;
            }
        }

        public MetodoDePago GetMetodoDePagoById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logManager.LogInfo($"Invalid id: {id}");
                    return null;
                }

                var metodoDePago = _dbContext.MetodosDePago.Find(id);
                if (metodoDePago == null)
                {
                    _logManager.LogInfo($"MetodoDePago with ID {id} not found.");
                    return null;
                }

                _logManager.LogInfo($"GetMetodoDePagoById executed successfully for ID {id}.");
                return metodoDePago;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to get MetodoDePago with ID {id}.", ex);
                throw;
            }
        }

        public void AddMetodoDePago(MetodoDePago metodoDePago)
        {
            try
            {
                _dbContext.MetodosDePago.Add(metodoDePago);
                _dbContext.SaveChanges();
                _logManager.LogInfo("AddMetodoDePago executed successfully.");
            }
            catch (Exception ex)
            {
                _logManager.LogError("Something went wrong while trying to add MetodoDePago.", ex);
                throw;
            }
        }

        public void UpdateMetodoDePago(MetodoDePago metodoDePago)
        {
            try
            {
                _dbContext.MetodosDePago.Update(metodoDePago);
                _dbContext.SaveChanges();
                _logManager.LogInfo($"UpdateMetodoDePago executed successfully for ID {metodoDePago.CodigoMetodoDePago}.");
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to update MetodoDePago with ID {metodoDePago.CodigoMetodoDePago}.", ex);
                throw;
            }
        }

        public void DeleteMetodoDePago(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logManager.LogInfo($"Invalid id: {id}");
                    return;
                }

                var metodoDePago = _dbContext.MetodosDePago.Find(id);
                if (metodoDePago == null)
                {
                    _logManager.LogInfo($"MetodoDePago with ID {id} not found.");
                    return;
                }

                _dbContext.MetodosDePago.Remove(metodoDePago);
                _dbContext.SaveChanges();
                _logManager.LogInfo($"DeleteMetodoDePago executed successfully for ID {id}.");
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Something went wrong while trying to delete MetodoDePago with ID {id}.", ex);
                throw;
            }
        }
    }
}
