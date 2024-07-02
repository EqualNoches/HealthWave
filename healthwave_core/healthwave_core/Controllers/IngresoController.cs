
using HospitalCore_core.Models;
using Microsoft.AspNetCore.Mvc;
using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoController : ControllerBase
    {
        private readonly IIngresoService _ingresoService;

        public IngresoController(IIngresoService ingresoService)
        {
            _ingresoService = ingresoService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddIngreso([FromBody] IngresoDto ingresoDto)
        {
            try
            {
                var result = await _ingresoService.AddIngresoAsync(ingresoDto);
                if (result == 1)
                    return Ok("Ingreso added successfully.");
                return BadRequest("Failed to add ingreso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateIngreso([FromBody] IngresoDto ingresoDto)
        {
            try
            {
                var result = await _ingresoService.UpdateIngresoAsync(ingresoDto);
                if (result == 1)
                    return Ok("Ingreso updated successfully.");
                return NotFound("Ingreso not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteIngreso(int id)
        {
            try
            {
                var result = await _ingresoService.DeleteIngresoAsync(id);
                if (result == 1)
                    return Ok("Ingreso deleted successfully.");
                return NotFound("Ingreso not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<IngresoDto>>> GetIngresos()
        {
            try
            {
                var ingresos = await _ingresoService.GetIngresosAsync();
                return Ok(ingresos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<IngresoDto>> GetIngresoById(int id)
        {
            try
            {
                var ingreso = await _ingresoService.GetIngresoByIdAsync(id);
                if (ingreso == null)
                    return NotFound("Ingreso not found.");
                return Ok(ingreso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("ADD{idIngreso}/afecciones/{idAfeccion}/add")]
        public async Task<IActionResult> AddIngresoAfeccion(int idIngreso, int idAfeccion)
        {
            try
            {
                var result = await _ingresoService.AddIngresoAfeccionAsync(idIngreso, idAfeccion);
                if (result == 1)
                    return Ok("Afección added to ingreso successfully.");
                return BadRequest("Failed to add afección to ingreso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DELETE {idIngreso}/afecciones/{idAfeccion}/remove")]
        public async Task<IActionResult> RemoveIngresoAfeccion(int idIngreso, int idAfeccion)
        {
            try
            {
                var result = await _ingresoService.RemoveIngresoAfeccionAsync(idIngreso, idAfeccion);
                if (result == 1)
                    return Ok("Afección removed from ingreso successfully.");
                return BadRequest("Failed to remove afección from ingreso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GET{idIngreso}/afecciones")]
        public async Task<ActionResult<List<AfeccionDto>>> GetIngresoAfecciones(int idIngreso)
        {
            var afecciones = await _ingresoService.GetIngresoAfeccionesAsync(idIngreso);

            if (afecciones == null || afecciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(afecciones.Select(a => AfeccionDto.FromModel(a)).ToList());
        }
    }
}
