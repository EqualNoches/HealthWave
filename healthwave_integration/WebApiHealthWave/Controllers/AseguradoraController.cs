using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;
using WebApiHealthWave.Utilities;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace WebApiHealthWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoraController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AseguradoraController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/AseguradoraControllerjson";

        public AseguradoraController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<AseguradoraController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Aseguradora
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aseguradora>>> GetAseguradoras()
        {
            try
            {
                var aseguradorasEnCore = await Core_GetAseguradoras();
                if (aseguradorasEnCore != null)
                {
                    return Ok(aseguradorasEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var aseguradoras = await _context.Aseguradoras.ToListAsync();
                if (aseguradoras != null)
                {
                    return Ok(aseguradoras);
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

        // GET: api/Aseguradora/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aseguradora>> GetAseguradora(int id)
        {
            try
            {
                var aseguradoraEnCore = await Core_GetAseguradora(id);
                if (aseguradoraEnCore != null)
                {
                    return Ok(aseguradoraEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var aseguradoraLocal = await _context.Aseguradoras.FindAsync(id);
                if (aseguradoraLocal == null)
                {
                    return NotFound();
                }
                return Ok(aseguradoraLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Aseguradora/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAseguradora(int id, Aseguradora aseguradora)
        {
            if (id != aseguradora.IDAseguradora)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateAseguradora(id, aseguradora);
                if (result != null)
                {
                    _context.Entry(aseguradora).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(aseguradora).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AseguradoraExists(id))
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

        // POST: api/Aseguradora
        [HttpPost]
        public async Task<ActionResult<Aseguradora>> PostAseguradora(Aseguradora aseguradora)
        {
            try
            {
                var result = await Core_CreateAseguradora(aseguradora);
                if (result != null)
                {
                    _context.Aseguradoras.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetAseguradora), new { id = result.IDAseguradora }, result);
                }
                else
                {
                    return BadRequest("Error al crear la aseguradora en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Aseguradoras.Add(aseguradora);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAseguradora), new { id = aseguradora.IDAseguradora }, aseguradora);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Aseguradora/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAseguradora(int id)
        {
            try
            {
                var result = await Core_DeleteAseguradora(id);
                if (result)
                {
                    var aseguradoraLocal = await _context.Aseguradoras.FindAsync(id);
                    if (aseguradoraLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Aseguradoras.Remove(aseguradoraLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var aseguradoraLocal = await _context.Aseguradoras.FindAsync(id);
                if (aseguradoraLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(aseguradoraLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AseguradoraExists(id))
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
        private async Task<Aseguradora> Core_GetAseguradora(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var aseguradora = await response.Content.ReadAsAsync<Aseguradora>();
                return aseguradora;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Aseguradora>> Core_GetAseguradoras()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}");
            if (response.IsSuccessStatusCode)
            {
                var aseguradoras = await response.Content.ReadFromJsonAsync<List<Aseguradora>>();
                return aseguradoras;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener aseguradoras del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Aseguradora> Core_CreateAseguradora(Aseguradora aseguradora)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(_coreBaseUrl, aseguradora);
            if (response.IsSuccessStatusCode)
            {
                var createdAseguradora = await response.Content.ReadAsAsync<Aseguradora>();
                return createdAseguradora;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Aseguradora> Core_UpdateAseguradora(int id, Aseguradora aseguradora)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{_coreBaseUrl}/{id}", aseguradora);
            if (response.IsSuccessStatusCode)
            {
                return aseguradora;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteAseguradora(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool AseguradoraExists(int id)
        {
            return _context.Aseguradoras.Any(e => e.IDAseguradora == id);
        }
    }
}
