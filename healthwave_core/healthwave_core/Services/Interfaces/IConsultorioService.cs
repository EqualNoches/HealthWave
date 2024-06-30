using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces;

public interface IConsultorioService
{
    Task<IEnumerable<Consultorio>> GetConsultoriosAsync();
    Task<Consultorio> GetConsultorioByIdAsync(int idConsultorio);
    Task<int> CreateConsultorioAsync(Consultorio consultorio);
    Task<int> UpdateConsultorioAsync(Consultorio consultorio);
    Task<int> DeleteConsultorioAsync(int idConsultorio);
}