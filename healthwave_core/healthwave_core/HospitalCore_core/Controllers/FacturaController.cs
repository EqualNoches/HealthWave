using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Add")]
        public async Task<IActionResult> AddFactura(FacturaDto facturaDto)
        {
            var result = await _facturaService.AddFacturaAsync(facturaDto);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateFactura(FacturaDto facturaDto)
        {
            var result = await _facturaService.UpdateFacturaAsync(facturaDto);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpDelete("Delete/{facturaCodigo}")]
        public async Task<IActionResult> DeleteFactura(string facturaCodigo)
        {
            var result = await _facturaService.DeleteFacturaAsync(facturaCodigo);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetFacturas()
        {
            var result = await _facturaService.GetFacturasAsync();
            return Ok(result);
        }

        [HttpPost("AddServicio")]
        public async Task<IActionResult> AddFacturaServicio(FacturaServicioDto facturaServicioDto)
        {
            var result = await _facturaService.AddFacturaServicioAsync(facturaServicioDto);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpDelete("DeleteServicio/{facturaCodigo}/{servicioCodigo}")]
        public async Task<IActionResult> DeleteFacturaServicio(string facturaCodigo, string servicioCodigo)
        {
            var result = await _facturaService.DeleteFacturaServicioAsync(facturaCodigo, servicioCodigo);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpGet("GetServicios/{facturaCodigo}")]
        public async Task<IActionResult> GetFacturaServicios(string facturaCodigo)
        {
            var result = await _facturaService.GetFacturaServiciosAsync(facturaCodigo);
            return Ok(result);
        }

        [HttpPost("AddProducto")]
        public async Task<IActionResult> AddFacturaProducto(FacturaProductoDto facturaProductoDto)
        {
            var result = await _facturaService.AddFacturaProductoAsync(facturaProductoDto);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpDelete("DeleteProducto/{facturaCodigoProducto}/{idProducto}")]
        public async Task<IActionResult> DeleteFacturaProducto(string facturaCodigoProducto, int idProducto)
        {
            var result = await _facturaService.DeleteFacturaProductoAsync(facturaCodigoProducto, idProducto);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpGet("GetProductos/{facturaCodigoProducto}")]
        public async Task<IActionResult> GetFacturaProductos(string facturaCodigoProducto)
        {
            var result = await _facturaService.GetFacturaProductosAsync(facturaCodigoProducto);
            return Ok(result);
        }

        [HttpPost("AddMetodoPago")]
        public async Task<IActionResult> AddFacturaMetodoPago(string facturaCodigo, int codigoMetodoDePago)
        {
            var result = await _facturaService.AddFacturaMetodoPagoAsync(facturaCodigo, codigoMetodoDePago);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpDelete("DeleteMetodoPago/{facturaCodigo}/{codigoMetodoDePago}")]
        public async Task<IActionResult> DeleteFacturaMetodoPago(string facturaCodigo, int codigoMetodoDePago)
        {
            var result = await _facturaService.DeleteFacturaMetodoPagoAsync(facturaCodigo, codigoMetodoDePago);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpGet("GetMetodosPago/{facturaCodigo}")]
        public async Task<IActionResult> GetMetodoPagos(string facturaCodigo)
        {
            var result = await _facturaService.GetMetodoPagosAsync(facturaCodigo);
            return Ok(result);
        }
    }
}
