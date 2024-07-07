using System.Collections.Generic;
using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces
{
    public interface IMetodoDePagoService
    {
        IEnumerable<MetodoDePago> GetAllMetodosDePago();
        MetodoDePago GetMetodoDePagoById(int id);
        void AddMetodoDePago(MetodoDePago metodoDePago);
        void UpdateMetodoDePago(MetodoDePago metodoDePago);
        void DeleteMetodoDePago(int id);
    }
}