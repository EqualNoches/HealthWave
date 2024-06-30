using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces;

public interface IAseguradoraService
{
    Task<int> CreateAseguradora(Aseguradora aseguradora);
    Task<List<Aseguradora>> GetAllAseguradoras();
    Task<Aseguradora?> GetAseguradoraById(int id);
    Task<int> UpdateAseguradora(Aseguradora aseguradora);
    Task<int> DeleteAseguradora(int id);
}