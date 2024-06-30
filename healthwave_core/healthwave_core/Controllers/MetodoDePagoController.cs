using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoDePagoController : ControllerBase
    {
        private readonly IMetodoDePagoService _metodoDePagoService;

        public MetodoDePagoController(IMetodoDePagoService metodoDePagoService)
        {
            _metodoDePagoService = metodoDePagoService;
        }

        [HttpPost("add")]
        public IActionResult AddMetodoDePago([FromBody] MetodoDePagoDto metodoDePagoDto)
        {
            var metodoDePago = new MetodoDePago
            {
                CodigoMetodoDePago = metodoDePagoDto.CodigoMetodoDePago,
                Nombre = metodoDePagoDto.Nombre
            };

            _metodoDePagoService.AddMetodoDePago(metodoDePago);
            return Ok("MetodoDePago added successfully.");
        }

        [HttpGet("get")]
        public ActionResult<List<MetodoDePagoDto>> GetAllMetodosDePago()
        {
            var metodosDePago = _metodoDePagoService.GetAllMetodosDePago()
                .Select(m => new MetodoDePagoDto
                {
                    CodigoMetodoDePago = m.CodigoMetodoDePago,
                    Nombre = m.Nombre
                }).ToList();

            return Ok(metodosDePago);
        }

        [HttpGet("get/{id}")]
        public ActionResult<MetodoDePagoDto> GetMetodoDePagoById(int id)
        {
            var metodoDePago = _metodoDePagoService.GetMetodoDePagoById(id);
            if (metodoDePago == null)
            {
                return NotFound("MetodoDePago not found.");
            }

            var metodoDePagoDto = new MetodoDePagoDto
            {
                CodigoMetodoDePago = metodoDePago.CodigoMetodoDePago,
                Nombre = metodoDePago.Nombre
            };

            return Ok(metodoDePagoDto);
        }

        [HttpPut("update")]
        public IActionResult UpdateMetodoDePago([FromBody] MetodoDePagoDto metodoDePagoDto)
        {
            var metodoDePago = _metodoDePagoService.GetMetodoDePagoById(metodoDePagoDto.CodigoMetodoDePago);
            if (metodoDePago == null)
            {
                return NotFound("MetodoDePago not found.");
            }

            metodoDePago.Nombre = metodoDePagoDto.Nombre;
            _metodoDePagoService.UpdateMetodoDePago(metodoDePago);

            return Ok("MetodoDePago updated successfully.");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteMetodoDePago(int id)
        {
            var metodoDePago = _metodoDePagoService.GetMetodoDePagoById(id);
            if (metodoDePago == null)
            {
                return NotFound("MetodoDePago not found.");
            }

            _metodoDePagoService.DeleteMetodoDePago(id);
            return Ok("MetodoDePago deleted successfully.");
        }
    }
}