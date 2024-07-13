using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace WebApiHealthWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoDePagoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MetodoDePagoController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/MetodoDePagoControllerjson";

        public MetodoDePagoController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<MetodoDePagoController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/MetodoDePago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodoDePago>>> GetMetodosDePago()
        {
            try
            {
                var metodosEnCore = await Core_GetMetodosDePago();
                if (metodosEnCore != null)
                {
                    return Ok(metodosEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var metodosLocal = await _context.MetodosDePago.ToListAsync();
                if (metodosLocal != null)
                {
                    return Ok(metodosLocal);
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

        // GET: api/MetodoDePago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodoDePago>> GetMetodoDePago(int id)
        {
            try
            {
                var metodoEnCore = await Core_GetMetodoDePago(id);
                if (metodoEnCore != null)
                {
                    return Ok(metodoEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var metodoLocal = await _context.MetodosDePago.FindAsync(id);
                if (metodoLocal == null)
                {
                    return NotFound();
                }
                return Ok(metodoLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/MetodoDePago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodoDePago(int id, MetodoDePago metodoDePago)
        {
            if (id != metodoDePago.CodigoMetodoDePago)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateMetodoDePago(id, metodoDePago);
                if (result != null)
                {
                    _context.Entry(metodoDePago).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(metodoDePago).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodoDePagoExists(id))
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

        // POST: api/MetodoDePago
        [HttpPost]
        public async Task<ActionResult<MetodoDePago>> PostMetodoDePago(MetodoDePago metodoDePago)
        {
            try
            {
                var result = await Core_CreateMetodoDePago(metodoDePago);
                if (result != null)
                {
                    _context.MetodosDePago.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetMetodoDePago), new { id = result.CodigoMetodoDePago }, result);
                }
                else
                {
                    return BadRequest("Error al crear el metodo de pago en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.MetodosDePago.Add(metodoDePago);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMetodoDePago), new { id = metodoDePago.CodigoMetodoDePago }, metodoDePago);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/MetodoDePago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodoDePago(int id)
        {
            try
            {
                var result = await Core_DeleteMetodoDePago(id);
                if (result)
                {
                    var metodoLocal = await _context.MetodosDePago.FindAsync(id);
                    if (metodoLocal == null)
                    {
                        return NotFound();
                    }
                    _context.MetodosDePago.Remove(metodoLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var metodoLocal = await _context.MetodosDePago.FindAsync(id);
                if (metodoLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(metodoLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodoDePagoExists(id))
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
        private async Task<MetodoDePago> Core_GetMetodoDePago(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var metodoDePago = await response.Content.ReadAsAsync<MetodoDePago>();
                return metodoDePago;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<MetodoDePago>> Core_GetMetodosDePago()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var metodosDePago = await response.Content.ReadFromJsonAsync<List<MetodoDePago>>();
                return metodosDePago;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener métodos de pago del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<MetodoDePago> Core_CreateMetodoDePago(MetodoDePago metodoDePago)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(metodoDePago), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/add", content);
            if (response.IsSuccessStatusCode)
            {
                var createdMetodoDePago = await response.Content.ReadAsAsync<MetodoDePago>();
                return createdMetodoDePago;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<MetodoDePago> Core_UpdateMetodoDePago(int id, MetodoDePago metodoDePago)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(metodoDePago), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update", content);
            if (response.IsSuccessStatusCode)
            {
                return metodoDePago;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteMetodoDePago(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool MetodoDePagoExists(int id)
        {
            return _context.MetodosDePago.Any(e => e.CodigoMetodoDePago == id);
        }
    }
}
