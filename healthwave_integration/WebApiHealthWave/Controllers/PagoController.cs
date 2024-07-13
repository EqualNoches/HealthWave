using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HospitalCore_core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PagoController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/PagoControllerJson";

        public PagoController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<PagoController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            try
            {
                var pagosEnCore = await Core_GetPagos();
                if (pagosEnCore != null)
                {
                    return Ok(pagosEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var pagos = await _context.Pagos.ToListAsync();
                if (pagos != null)
                {
                    return Ok(pagos);
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

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            try
            {
                var pagoEnCore = await Core_GetPago(id);
                if (pagoEnCore != null)
                {
                    return Ok(pagoEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var pagoLocal = await _context.Pagos.FindAsync(id);
                if (pagoLocal == null)
                {
                    return NotFound();
                }
                return Ok(pagoLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Pago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.IDPago)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdatePago(id, pago);
                if (result != null)
                {
                    _context.Entry(pago).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(pago).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(id))
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

        // POST: api/Pago
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            try
            {
                var result = await Core_CreatePago(pago);
                if (result != null)
                {
                    _context.Pagos.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetPago), new { id = result.IDPago }, result);
                }
                else
                {
                    return BadRequest("Error al crear el pago en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Pagos.Add(pago);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPago), new { id = pago.IDPago }, pago);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            try
            {
                var result = await Core_DeletePago(id);
                if (result)
                {
                    var pagoLocal = await _context.Pagos.FindAsync(id);
                    if (pagoLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Pagos.Remove(pagoLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var pagoLocal = await _context.Pagos.FindAsync(id);
                if (pagoLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(pagoLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(id))
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
        private async Task<Pago> Core_GetPago(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var pago = await response.Content.ReadAsAsync<Pago>();
                return pago;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Pago>> Core_GetPagos()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var pagos = await response.Content.ReadFromJsonAsync<List<Pago>>();
                return pagos;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener pagos del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Pago> Core_CreatePago(Pago pago)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(pago), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/add", content);
            if (response.IsSuccessStatusCode)
            {
                var createdPago = await response.Content.ReadAsAsync<Pago>();
                return createdPago;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Pago> Core_UpdatePago(int id, Pago pago)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(pago), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return pago;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeletePago(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.IDPago == id);
        }
    }
}
