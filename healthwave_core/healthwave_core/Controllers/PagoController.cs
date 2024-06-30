using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCore_core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PagoController(IPagoService pagoService) : ControllerBase
{
    private readonly LogManager<PagoController> _logManager = new();


    [HttpPost("post")]
    public async Task<ActionResult> PostPago(PagoDto pago)
    {
        try
        {
            await pagoService.CreatePago(Pago.FromDTO(pago));
            return Ok();
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error al crear pago en la pase de datos", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al insertar pago en la base de datos");
        }
    }

    //get
    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<Pago>>> GetPago()
    {
        try
        {
            var pago = await pagoService.GetPagos();
            return Ok(pago.Select(PagoDto.FromModel));
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error al obtener pagos", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener pagos del servidor");
        }
    }

    // Get by ID
    [HttpGet("get/{id}")]
    public async Task<ActionResult> GetPagosId(int id){
        try
        {
            var pago = await pagoService.GetPagoById(id);
            if (pago == null)
            {
                return BadRequest("Este registro de pago no se encuentra");
            }
            return Ok(pago);
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error al obtener el registro de pago solicitado", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener pagos con el id {id}");
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult> PutPago(PagoDto pago){
        try
        {
            await pagoService.UpdatePago(Pago.FromDTO(pago));
            return Ok();
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("No se pudo Modificar el pago", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener pagos con el id {pago.IdPago}");
        }
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> Delete(uint id)
    {

        try
        {
            var answer = await pagoService.DeletePago(id);
            if (answer == 0 )
            {
                NotFound(); 
                return BadRequest("No se pudo encontrar ese registro de factura");
            }
            return Ok(answer);
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("No se pudo eliminar el pago", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar registro de pagos con el id {id}");
        }
    }
}
