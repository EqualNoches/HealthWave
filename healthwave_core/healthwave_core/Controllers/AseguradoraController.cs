using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HospitalCore_core.Models;
using HospitalCore_core.Utilities;

namespace HospitalCore_core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AseguradoraController(IAseguradoraService aseguradoraService) : ControllerBase
{
    private readonly LogManager<AseguradoraController> _logHandler = new();
    

    [HttpPost("post")]
    public async Task<ActionResult<int>> PostAseguradora(Aseguradora aseguradora)
    {
        try
        {
            if (aseguradora.Idaseguradora != 0)
            {
                return BadRequest("Id aseguradora should not be set by the user.");
            }

            var newAseguradora = await aseguradoraService.CreateAseguradora(aseguradora);
            if (newAseguradora == 0)
            {
                return Conflict();
            }

            return Ok(newAseguradora);
        }
        catch (Exception ex)
        {
            _logHandler.LogFatal("Error al crear la aseguradora",ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la aseguradora");
        }
    }

    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<Aseguradora>>> GetAseguradoras()
    {
        try
        {
            return await aseguradoraService.GetAllAseguradoras();

        } catch (Exception ex)
        {
            _logHandler.LogFatal("Error al obtener las aseguradoras", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las aseguradoras");
        }
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<Aseguradora>> GetAseguradoraById(uint id)
    {
        try
        {
            var aseguradora = await aseguradoraService.GetAseguradoraById((int)id);
            if (aseguradora == null)
            {
                return NotFound();
            }
            return Ok(aseguradora);
        }
        catch (Exception ex)
        {
            _logHandler.LogFatal($"Error al obtener la aseguradora con ID {id}",ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la aseguradora");
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<int>> UpdateAseguradora(Aseguradora aseguradora)
    {
        try
        {
            var result = await aseguradoraService.UpdateAseguradora(aseguradora);
            if (result == 0)
            {
                return NotFound();
            }
            return result;
        }
        catch (Exception ex)
        {
            _logHandler.LogFatal("Error al actualizar aseguradora",ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la aseguradora");
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<int>> DeleteAseguradora(uint id)
    {
        try
        {
            var result = await aseguradoraService.DeleteAseguradora((int)id);
            if (result == 0)
            {
                return NotFound();
            }
            return result;
        }
        catch (Exception ex)
        {
            _logHandler.LogFatal( $"Error al eliminar la aseguradora con ID {id}",ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la aseguradora");
        }
    }
}
