using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<FacturaController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/FacturaControllerJson";

        public FacturaController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<FacturaController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Factura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            try
            {
                var facturasEnCore = await Core_GetFacturas();
                if (facturasEnCore != null)
                {
                    return Ok(facturasEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var facturas = await _context.Facturas.ToListAsync();
                if (facturas != null)
                {
                    return Ok(facturas);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Factura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            try
            {
                var facturaEnCore = await Core_GetFactura(id);
                if (facturaEnCore != null)
                {
                    return Ok(facturaEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var facturaLocal = await _context.Facturas.FindAsync(id);
                if (facturaLocal == null)
                {
                    return NotFound();
                }
                return Ok(facturaLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Factura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            if (id != factura.FacturaCodigo)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateFactura(id, factura);
                if (result != null)
                {
                    _context.Entry(factura).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(factura).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            return NoContent();
        }

        // POST: api/Factura
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            try
            {
                var result = await Core_CreateFactura(factura);
                if (result != null)
                {
                    _context.Facturas.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetFactura), new { id = result.FacturaCodigo }, result);
                }
                else
                {
                    return BadRequest("Error al crear la factura en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetFactura), new { id = factura.FacturaCodigo }, factura);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Factura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                var result = await Core_DeleteFactura(id);
                if (result)
                {
                    var facturaLocal = await _context.Facturas.FindAsync(id);
                    if (facturaLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Facturas.Remove(facturaLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var facturaLocal = await _context.Facturas.FindAsync(id);
                if (facturaLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(facturaLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception exL)
                {
                    _logger.LogError($"Error en la aplicación: {exL.Message}");
                    return StatusCode(500, $"Error interno del servidor: {exL.Message}");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            return NoContent();
        }

        // Métodos privados para interactuar con el Core
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Factura> Core_GetFactura(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/Get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var factura = await response.Content.ReadAsAsync<Factura>();
                return factura;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Factura>> Core_GetFacturas()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/Get");
            if (response.IsSuccessStatusCode)
            {
                var facturas = await response.Content.ReadFromJsonAsync<List<Factura>>();
                return facturas;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener facturas del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Factura> Core_CreateFactura(Factura factura)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(factura), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/Add", content);
            if (response.IsSuccessStatusCode)
            {
                var createdFactura = await response.Content.ReadAsAsync<Factura>();
                return createdFactura;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Factura> Core_UpdateFactura(int id, Factura factura)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(factura), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/Update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return factura;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteFactura(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.FacturaCodigo == id);
        }
    }
}
