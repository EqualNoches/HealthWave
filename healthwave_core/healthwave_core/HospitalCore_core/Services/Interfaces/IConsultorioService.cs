using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Services.Interfaces
{
    public interface IConsultorioService
    {
        Task<IEnumerable<ConsultorioDTO>> GetConsultoriosAsync();
        Task<ConsultorioDTO> GetConsultorioByIdAsync(int idConsultorio);
        Task<int> CreateConsultorioAsync(ConsultorioDTO consultorioDTO);
        Task<int> UpdateConsultorioAsync(ConsultorioDTO consultorioDTO);
        Task<int> DeleteConsultorioAsync(int idConsultorio);
    }
}