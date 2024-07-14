using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;
using Microsoft.Extensions.Logging;

namespace WebApiHealthWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AutorizacionController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/AutorizacionControllerjson";

        public AutorizacionController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<AutorizacionController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Autorizacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autorizacion>>> GetAutorizaciones()
        {
            try
            {
                var autorizacionesEnCore = await Core_GetAutorizaciones();
                if (autorizacionesEnCore != null)
                {
                    return Ok(autorizacionesEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var autorizaciones = await _context.Autorizaciones.ToListAsync();
                if (autorizaciones != null)
                {
                    return Ok(autorizaciones);
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

        // GET: api/Autorizacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Autorizacion>> GetAutorizacion(int id)
        {
            try
            {
                var autorizacionEnCore = await Core_GetAutorizacion(id);
                if (autorizacionEnCore != null)
                {
                    return Ok(autorizacionEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var autorizacionLocal = await _context.Autorizaciones.FindAsync(id);
                if (autorizacionLocal == null)
                {
                    return NotFound();
                }
                return Ok(autorizacionLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Autorizacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutorizacion(int id, Autorizacion autorizacion)
        {
            if (id != autorizacion.IDAutorizacion)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateAutorizacion(id, autorizacion);
                if (result != null)
                {
                    _context.Entry(autorizacion).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(autorizacion).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorizacionExists(id))
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

        // POST: api/Autorizacion
        [HttpPost]
        public async Task<ActionResult<Autorizacion>> PostAutorizacion(Autorizacion autorizacion)
        {
            try
            {
                var result = await Core_CreateAutorizacion(autorizacion);
                if (result != null)
                {
                    _context.Autorizaciones.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetAutorizacion), new { id = result.IDAutorizacion }, result);
                }
                else
                {
                    return BadRequest("Error al crear la autorización en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Autorizaciones.Add(autorizacion);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAutorizacion), new { id = autorizacion.IDAutorizacion }, autorizacion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Autorizacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutorizacion(int id)
        {
            try
            {
                var result = await Core_DeleteAutorizacion(id);
                if (result)
                {
                    var autorizacionLocal = await _context.Autorizaciones.FindAsync(id);
                    if (autorizacionLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Autorizaciones.Remove(autorizacionLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var autorizacionLocal = await _context.Autorizaciones.FindAsync(id);
                if (autorizacionLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(autorizacionLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorizacionExists(id))
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
        private async Task<Autorizacion> Core_GetAutorizacion(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var autorizacion = await response.Content.ReadAsAsync<Autorizacion>();
                return autorizacion;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Autorizacion>> Core_GetAutorizaciones()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var autorizaciones = await response.Content.ReadFromJsonAsync<List<Autorizacion>>();
                return autorizaciones;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener autorizaciones del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Autorizacion> Core_CreateAutorizacion(Autorizacion autorizacion)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(autorizacion), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/Add", content);
            if (response.IsSuccessStatusCode)
            {
                var createdAutorizacion = await response.Content.ReadAsAsync<Autorizacion>();
                return createdAutorizacion;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Autorizacion> Core_UpdateAutorizacion(int id, Autorizacion autorizacion)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(autorizacion), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/Update", content);
            if (response.IsSuccessStatusCode)
            {
                return autorizacion;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteAutorizacion(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool AutorizacionExists(int id)
        {
            return _context.Autorizaciones.Any(e => e.IDAutorizacion == id);
        }
    }
}
