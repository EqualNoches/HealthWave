using HospitalCore_core.Models;
using Microsoft.AspNetCore.Mvc;
using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfeccionController : ControllerBase
    {
        private readonly IAfeccionService _afeccionService;

        public AfeccionController(IAfeccionService afeccionService)
        {
            _afeccionService = afeccionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AfeccionDto>>> GetAfecciones()
        {
            var afecciones = await _afeccionService.GetAfecciones();
            return Ok(afecciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AfeccionDto>> GetAfeccion(int id)
        {
            var afeccion = await _afeccionService.GetAfeccionById(id);

            if (afeccion == null)
                return NotFound();

            return Ok(afeccion);
        }

        [HttpPost]
        public async Task<ActionResult<AfeccionDto>> CreateAfeccion(AfeccionDto afeccionDto)
        {
            var createdAfeccion = await _afeccionService.CreateAfeccion(afeccionDto);
            return CreatedAtAction(nameof(GetAfeccion), new { id = createdAfeccion.IdAfeccion }, createdAfeccion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AfeccionDto>> UpdateAfeccion(int id, AfeccionDto afeccionDto)
        {
            var updatedAfeccion = await _afeccionService.UpdateAfeccion(id, afeccionDto);

            if (updatedAfeccion == null)
                return NotFound();

            return Ok(updatedAfeccion);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAfeccion(int id)
        {
            var deleted = await _afeccionService.DeleteAfeccion(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}