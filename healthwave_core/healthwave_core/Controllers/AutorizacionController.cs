using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCore_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorizacionController : ControllerBase
    {
        private readonly IAutorizacionService _autorizacionService;

        public AutorizacionController(IAutorizacionService autorizacionService)
        {
            _autorizacionService = autorizacionService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAutorizacion(AutorizacionDTO autorizacionDto, int? idIngreso, string? consultaCodigo, string? facturaCodigo, string? servicioCodigo, int? idProducto)
        {
            var result = await _autorizacionService.AddAutorizacion(autorizacionDto, idIngreso, consultaCodigo, facturaCodigo, servicioCodigo, idProducto);
            if (result == 0)
            {
                return BadRequest("Autorizacion no pudo ser creada");
            }
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAutorizacion(int id)
        {
            var result = await _autorizacionService.DeleteAutorizacionAsync(id);
            if (result == 0)
            {
                return NotFound("Autorizacion no encontrada");
            }
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<AutorizacionDTO>>> GetAllAutorizaciones()
        {
            var autorizaciones = await _autorizacionService.GetAllAutorizaciones();
            return Ok(autorizaciones);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AutorizacionDTO?>> GetAutorizacionById(int id)
        {
            var autorizacion = await _autorizacionService.GetAutorizacionById(id);
            if (autorizacion == null)
            {
                return NotFound("Autorizacion no encontrada");
            }
            return Ok(autorizacion);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAutorizacion(AutorizacionDTO autorizacionDto)
        {
            var result = await _autorizacionService.UpdateAutorizacionAsync(autorizacionDto);
            if (result == 0)
            {
                return NotFound("Autorizacion no encontrada");
            }
            return Ok(result);
        }
    }
}
