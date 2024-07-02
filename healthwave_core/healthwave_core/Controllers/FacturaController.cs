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

        [HttpPost]
        public async Task<IActionResult> CreateFactura([FromBody] FacturaDto facturaDto)
        {
            try
            {
                var result = await _facturaService.AddFacturaAsync(facturaDto);
                return CreatedAtAction(nameof(GetFacturaByCodigo), new { codigo = facturaDto.FacturaCodigo }, facturaDto);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFactura([FromBody] FacturaDto facturaDto)
        {
            try
            {
                var result = await _facturaService.UpdateFacturaAsync(facturaDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteFactura(string codigo)
        {
            try
            {
                var result = await _facturaService.DeleteFacturaAsync(codigo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFacturas()
        {
            try
            {
                var facturas = await _facturaService.GetFacturasAsync();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetFacturaByCodigo(string codigo)
        {
            try
            {
                var factura = await _facturaService.GetFacturasAsync();
                return Ok(factura);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddServicio")]
        public async Task<IActionResult> AddFacturaServicio([FromBody] FacturaServicioDto facturaServicioDto)
        {
            try
            {
                var result = await _facturaService.AddFacturaServicioAsync(facturaServicioDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteServicio/{codigo}/{servicioCodigo}")]
        public async Task<IActionResult> DeleteFacturaServicio(string codigo, string servicioCodigo)
        {
            try
            {
                var result = await _facturaService.DeleteFacturaServicioAsync(codigo, servicioCodigo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Servicios/{codigo}")]
        public async Task<IActionResult> GetFacturaServicios(string codigo)
        {
            try
            {
                var servicios = await _facturaService.GetFacturaServiciosAsync(codigo);
                return Ok(servicios);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddProducto")]
        public async Task<IActionResult> AddFacturaProducto([FromBody] FacturaProductoDto facturaProductoDto)
        {
            try
            {
                var result = await _facturaService.AddFacturaProductoAsync(facturaProductoDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteProducto/{codigo}/{productoId}")]
        public async Task<IActionResult> DeleteFacturaProducto(string codigo, int productoId)
        {
            try
            {
                var result = await _facturaService.DeleteFacturaProductoAsync(codigo, productoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Productos/{codigo}")]
        public async Task<IActionResult> GetFacturaProductos(string codigo)
        {
            try
            {
                var productos = await _facturaService.GetFacturaProductosAsync(codigo);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddMetodoPago")]
        public async Task<IActionResult> AddFacturaMetodoPago([FromBody] string facturaCodigo, int codigoMetodoDePago)
        {
            try
            {
                var result = await _facturaService.AddFacturaMetodoPagoAsync(facturaCodigo, codigoMetodoDePago);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteMetodoPago/{facturaCodigo}/{codigoMetodoDePago}")]
        public async Task<IActionResult> DeleteFacturaMetodoPago(string facturaCodigo, int codigoMetodoDePago)
        {
            try
            {
                var result = await _facturaService.DeleteFacturaMetodoPagoAsync(facturaCodigo, codigoMetodoDePago);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("MetodosPago/{codigo}")]
        public async Task<IActionResult> GetMetodoPagos(string codigo)
        {
            try
            {
                var metodos = await _facturaService.GetMetodoPagosAsync(codigo);
                return Ok(metodos);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }
    }
}
