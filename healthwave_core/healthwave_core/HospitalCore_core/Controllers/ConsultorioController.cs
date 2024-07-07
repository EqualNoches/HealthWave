using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultorioController : ControllerBase
    {
        private readonly IConsultorioService _consultorioService;

        public ConsultorioController(IConsultorioService consultorioService)
        {
            _consultorioService = consultorioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultorioDTO>>> GetConsultorios()
        {
            var consultorios = await _consultorioService.GetConsultoriosAsync();
            return Ok(consultorios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultorioDTO>> GetConsultorio(int id)
        {
            var consultorio = await _consultorioService.GetConsultorioByIdAsync(id);
            if (consultorio == null)
            {
                return NotFound();
            }
            return Ok(consultorio);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateConsultorio(ConsultorioDTO consultorioDTO)
        {
            var createdId = await _consultorioService.CreateConsultorioAsync(consultorioDTO);
            return CreatedAtAction(nameof(GetConsultorio), new { id = createdId }, createdId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsultorio(int id, ConsultorioDTO consultorioDTO)
        {
            if (id != consultorioDTO.IDConsultorio)
            {
                return BadRequest();
            }

            await _consultorioService.UpdateConsultorioAsync(consultorioDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultorio(int id)
        {
            var deletedId = await _consultorioService.DeleteConsultorioAsync(id);
            if (deletedId == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
