using WebApiHealthWave.Data;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface ICuentaCobrarService
    {
        Task<int> AddCuentaCobrarAsync(CuentaCobrarDto cuentaCobrarDto);
        Task<List<CuentaCobrarDto>> GetCuentasCobrarAsync();
        Task<int> UpdateCuentaCobrarAsync(CuentaCobrarDto cuentaCobrarDto);
        Task<int> DeleteCuentaCobrarAsync(int idCuenta);
        Task<CuentaCobrarDto?> GetCuentaCobrarByIdAsync(int idCuenta);
    }
}

