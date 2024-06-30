using HospitalCore_core.DTO;

namespace HospitalCore_core.Services.Interfaces;

public interface IServicioService
{
    Task<IEnumerable<ServicioDto>> GetAllServicios();
    Task<ServicioDto> GetServicioById(int id);
    Task<ServicioDto> CreateServicio(ServicioDto servicioDto);
    Task<ServicioDto> UpdateServicio(ServicioDto servicioDto);
    Task<bool> DeleteServicio(int id);
}