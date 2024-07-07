using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicioController : ControllerBase
    {
        private readonly ITipoServicioService _tipoServicioService;

        public TipoServicioController(ITipoServicioService tipoServicioService)
        {
            _tipoServicioService = tipoServicioService;
        }

        [HttpPost("post")]
        public async Task<IActionResult> AddTipoServicio([FromBody] TipoServicioDto tipoServicioDto)
        {
            if (tipoServicioDto == null)
            {
                return BadRequest("Tipo de servicio es nulo.");
            }

            var result = await _tipoServicioService.AddTipoServicioAsync(tipoServicioDto);
            if (result == 1)
            {
                return Ok("Tipo de servicio creado con éxito.");
            }

            return StatusCode(500, "Ocurrió un error al crear el tipo de servicio.");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetTipoServicios()
        {
            var tipoServicios = await _tipoServicioService.GetTipoServiciosAsync();
            return Ok(tipoServicios);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTipoServicio([FromBody] TipoServicioDto tipoServicioDto)
        {
            if (tipoServicioDto == null)
            {
                return BadRequest("Tipo de servicio es nulo.");
            }

            var result = await _tipoServicioService.UpdateTipoServicioAsync(tipoServicioDto);
            if (result == 1)
            {
                return Ok("Tipo de servicio actualizado con éxito.");
            }

            if (result == 0)
            {
                return NotFound("Tipo de servicio no encontrado.");
            }

            return StatusCode(500, "Ocurrió un error al actualizar el tipo de servicio.");
        }

        [HttpDelete("delete{id}")]
        public async Task<IActionResult> DeleteTipoServicio(uint id)
        {
            var result = await _tipoServicioService.DeleteTipoServicioAsync(id);
            if (result == 1)
            {
                return Ok("Tipo de servicio eliminado con éxito.");
            }

            if (result == 0)
            {
                return NotFound("Tipo de servicio no encontrado.");
            }

            return StatusCode(500, "Ocurrió un error al eliminar el tipo de servicio.");
        }
    }
}
