using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces;

public interface IPagoService
{
    Task<IEnumerable<Pago?>> GetPagos();
    Task<Pago?> GetPagoById(int idPago);
    Task<int> CreatePago(Pago? pago);
    Task<int> UpdatePago(Pago pago);
    Task<int> DeletePago(uint idPago);
}