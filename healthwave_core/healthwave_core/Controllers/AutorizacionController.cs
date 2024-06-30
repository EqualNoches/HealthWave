using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HospitalCore_core.Utilities;
using HospitalCore_core.Models;

namespace HospitalCore_core.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacionController(IAutorizacionService autorizacionService) : ControllerBase
    {
        private readonly LogManager<AutorizacionController> _logManager = new();

        [HttpPost("Post")]
        public async Task<IActionResult> PostAturización([FromQuery] Autorizacion autorizacion, int? idIngreso, string? consultaCodigo, string? facturaCodigo, string? servicioCodigo, int? idProducto){
            try
            {
                if (autorizacion.Idautorizacion != 0)
                {
                    return BadRequest("El id de la autorización no puede ser incluido en el codigo");
                }

                var newAutorizacion =
                    await autorizacionService.AddAutorizacion(autorizacion, idIngreso,consultaCodigo,facturaCodigo,servicioCodigo, idProducto);
                if (newAutorizacion == 0)
                {
                    return Conflict();
                }

                return Ok(newAutorizacion);
            }
            catch (Exception exception)
            {
                _logManager.LogFatal("Error al crear autorización",exception);
                throw;
            }
        }
        
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Autorizacion>>> GetAutorizaciones()
        {
            try
            {
                return await autorizacionService.GetAllAutorizaciones();
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error obteniendo las autorizaciones", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las autorizaciones");
            }
        }

        [HttpGet("get/ID")]
        public async Task<ActionResult<Autorizacion>> GetAutorizacionById(uint id)
        {
            try
            {

                var autorizacion = await autorizacionService.GetAutorizacionById((int)id);
                if (autorizacion == null)
                {
                    return NotFound();
                }

                return Ok(autorizacion);
            }
            catch (Exception e)
            {
                _logManager.LogFatal($"Error al obtener Autorizacion especificada", e);
                 return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la autorización");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<int>> UpdateAutorizacion(Autorizacion autorizacion)
        {
            try
            {
                var result = await autorizacionService.UpdateAutorizacionAsync(autorizacion);
                if (result == 0)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception e)
            {
                _logManager.LogFatal("Error al actualizar Autorización", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la autorización");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<int>> DeleteAutorizacion(uint id)
        {
            try
            {
                var result = await autorizacionService.DeleteAutorizacionAsync((int)id);
                if (result == 0)
                    
                {
                    
                    
                    return NotFound();
                    
                    
                }

                return result;

            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error al tratar de eliminar la autorización{id}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la autorización");
            }
        }
    }
}