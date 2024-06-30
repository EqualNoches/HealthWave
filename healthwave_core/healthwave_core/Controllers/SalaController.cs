using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCore_core.Controllers;

[Route("api/[controller]")]
public class SalaController(ISalaService salaService) : ControllerBase
{
    private readonly LogManager<SalaController> _logManager = new();

    [HttpPost("post")]
    public async Task<ActionResult<int>> PostSala(SalaDto sala)
    {
        try
        {
            var newSala = await salaService.CreateSalaAsync(sala);
            if (newSala == 0)
            {
                return Conflict();
            }

            return Ok(newSala);
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error al tratar de crear sala", ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error tratando de introducir la data al servidor");
        }
    }

    [HttpGet("get")]
    public async Task<ActionResult<int>> GetSala()
    {
        var salas = await salaService.GetSalasAsync();
        return Ok(salas);
    }

    [HttpPut("update")]
    public async Task<ActionResult> updateSalas(int numSala)
    {
        try
        {
            var salas = await salaService.UpdateSalaEstadoAsync(numSala);
            if (salas == 0)
            {
                return NotFound("Sala no encontrada en el registro");
            }
            return Ok("Estado de sala fua actualizado correctamente");
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("No se pudo actualizar la base de datos", ex);
            return StatusCode(StatusCodes. Status500InternalServerError, "No pudo integrarse registro a la base de datos");
        }
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<int>> DeleteSala(int numSala)
    {
        try
        {
            var result = await salaService.DeleteSalaAsync(numSala);
        if (result == 0)
        {
            BadRequest("La sala no pudo se encontrada");
            return 0;
        }
        return Ok("El registro se borró correctamente");
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("No se pudo eliminar registro de la base de datos", ex);
            return StatusCode(StatusCodes. Status500InternalServerError, "No se pudo eliminar registro de la base de datos");
        }
        
    }
}
