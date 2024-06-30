using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ServiciosController : ControllerBase
{
    private readonly IServicioService _servicioService;

    public ServiciosController(IServicioService servicioService)
    {
        _servicioService = servicioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServicioDto>>> GetServicios()
    {
        var servicios = await _servicioService.GetAllServicios();
        return Ok(servicios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServicioDto>> GetServicio(int id)
    {
        var servicio = await _servicioService.GetServicioById(id);

        if (servicio == null)
        {
            return NotFound();
        }

        return Ok(servicio);
    }

    [HttpPost]
    public async Task<ActionResult<ServicioDto>> PostServicio(ServicioDto servicioDto)
    {
        var createdServicio = await _servicioService.CreateServicio(servicioDto);
        return CreatedAtAction(nameof(GetServicio), new { id = createdServicio.ServicioCodigo }, createdServicio);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutServicio(int id, ServicioDto servicioDto)
    {
        if (id != servicioDto.ServicioCodigo)
        {
            return BadRequest();
        }

        var updatedServicio = await _servicioService.UpdateServicio(servicioDto);

        if (updatedServicio == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServicio(int id)
    {
        var deleted = await _servicioService.DeleteServicio(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}