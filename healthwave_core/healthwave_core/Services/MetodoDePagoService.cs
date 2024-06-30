using System.Collections.Generic;
using System.Linq;
using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;

namespace HospitalCore_core.Services
{
    public class MetodoDePagoService : IMetodoDePagoService
    {
        private readonly HospitalCore _dbContext;

        public MetodoDePagoService(HospitalCore dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MetodoDePago> GetAllMetodosDePago()
        {
            return _dbContext.MetodosDePago.ToList();
        }

        public MetodoDePago GetMetodoDePagoById(int id)
        {
            return _dbContext.MetodosDePago.Find(id);
        }

        public void AddMetodoDePago(MetodoDePago metodoDePago)
        {
            _dbContext.MetodosDePago.Add(metodoDePago);
            _dbContext.SaveChanges();
        }

        public void UpdateMetodoDePago(MetodoDePago metodoDePago)
        {
            _dbContext.MetodosDePago.Update(metodoDePago);
            _dbContext.SaveChanges();
        }

        public void DeleteMetodoDePago(int id)
        {
            var metodoDePago = _dbContext.MetodosDePago.Find(id);
            if (metodoDePago != null)
            {
                _dbContext.MetodosDePago.Remove(metodoDePago);
                _dbContext.SaveChanges();
            }
        }
    }
}