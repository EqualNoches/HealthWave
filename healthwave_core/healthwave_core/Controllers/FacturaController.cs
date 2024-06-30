using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalCore_core.Models; 

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFacturaAsync(FacturaDto factura)
        {
            var result = await _facturaService.AddFacturaAsync(factura);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFacturaAsync(FacturaDto factura)
        {
            var result = await _facturaService.UpdateFacturaAsync(factura);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{facturaCodigo}")]
        public async Task<IActionResult> DeleteFacturaAsync(string facturaCodigo)
        {
            var result = await _facturaService.DeleteFacturaAsync(facturaCodigo);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDto>>> GetFacturasAsync()
        {
            var facturas = await _facturaService.GetFacturasAsync();
            return Ok(facturas);
        }

        [HttpPost("servicio")]
        public async Task<IActionResult> AddFacturaServicioAsync(FacturaServicioDto facturaServicio)
        {
            var result = await _facturaService.AddFacturaServicioAsync(facturaServicio);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("servicio/{facturaCodigo}/{servicioCodigo}")]
        public async Task<IActionResult> DeleteFacturaServicioAsync(string facturaCodigo, string servicioCodigo)
        {
            var result = await _facturaService.DeleteFacturaServicioAsync(facturaCodigo, servicioCodigo);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpGet("servicio/{facturaCodigo}")]
        public async Task<ActionResult<IEnumerable<FacturaServicioDto>>> GetFacturaServiciosAsync(string facturaCodigo)
        {
            var facturaServicios = await _facturaService.GetFacturaServiciosAsync(facturaCodigo);
            return Ok(facturaServicios);
        }

        [HttpPost("producto")]
        public async Task<IActionResult> AddFacturaProductoAsync(FacturaProductoDto facturaProducto)
        {
            var result = await _facturaService.AddFacturaProductoAsync(facturaProducto);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("producto/{facturaCodigo}/{idProducto}")]
        public async Task<IActionResult> DeleteFacturaProductoAsync(string facturaCodigo, int idProducto)
        {
            var result = await _facturaService.DeleteFacturaProductoAsync(facturaCodigo, idProducto);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpGet("producto/{facturaCodigo}")]
        public async Task<ActionResult<IEnumerable<FacturaProductoDto>>> GetFacturaProductosAsync(string facturaCodigo)
        {
            var facturaProductos = await _facturaService.GetFacturaProductosAsync(facturaCodigo);
            return Ok(facturaProductos);
        }

        [HttpPost("metodopago/{facturaCodigo}/{idMetodoPago}")]
        public async Task<IActionResult> AddFacturaMetodoPagoAsync(string facturaCodigo, int idMetodoPago)
        {
            var result = await _facturaService.AddFacturaMetodoPagoAsync(facturaCodigo, idMetodoPago);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("metodopago/{facturaCodigo}/{idMetodoPago}")]
        public async Task<IActionResult> DeleteFacturaMetodoPagoAsync(string facturaCodigo, int idMetodoPago)
        {
            var result = await _facturaService.DeleteFacturaMetodoPagoAsync(facturaCodigo, idMetodoPago);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpGet("metodopago/{facturaCodigo}")]
        public async Task<ActionResult<IEnumerable<MetodoDePago>>> GetMetodoPagosAsync(string facturaCodigo)
        {
            var metodoPagos = await _facturaService.GetMetodoPagosAsync(facturaCodigo);
            return Ok(metodoPagos);
        }
    }
}
