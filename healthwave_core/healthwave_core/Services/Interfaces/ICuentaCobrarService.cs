using HospitalCore_core.DTO;

namespace HospitalCore_core.Services.Interfaces
{
    public interface ICuentaCobrarService
    {
        Task<IEnumerable<CuentaCobrarDto>> GetCuentasCobrar();
        Task<CuentaCobrarDto?> GetCuentaCobrarById(int id);
        Task<CuentaCobrarDto> CreateCuentaCobrar(CuentaCobrarDto cuentaCobrarDto);
        Task<CuentaCobrarDto?> UpdateCuentaCobrar(int id, CuentaCobrarDto cuentaCobrarDto);
        Task<bool> DeleteCuentaCobrar(int id);
    }
}