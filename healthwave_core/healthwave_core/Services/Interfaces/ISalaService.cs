using HospitalCore_core.DTO;
using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces;

public interface ISalaService
{
    Task<int> CreateSalaAsync(SalaDto salaDto);
    Task<List<SalaDto>> GetSalasAsync();
    Task<int> UpdateSalaEstadoAsync(int numSala);
    Task<int> DeleteSalaAsync(int numSala);
}