
using HospitalCore_core.DTO;
using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces;
public interface IAfeccionService
    {
        Task<IEnumerable<AfeccionDto>> GetAfecciones();
        Task<AfeccionDto?> GetAfeccionById(int id);
        Task<AfeccionDto> CreateAfeccion(AfeccionDto afeccionDto);
        Task<AfeccionDto?> UpdateAfeccion(int id, AfeccionDto afeccionDto);
        Task<bool> DeleteAfeccion(int id);
    }