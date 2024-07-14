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
    public class ServicioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ServicioController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:44372/api/ServicioControllerJson";

        public ServicioController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<ServicioController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicios()
        {
            try
            {
                var serviciosEnCore = await Core_GetServicios();
                if (serviciosEnCore != null)
                {
                    return Ok(serviciosEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var servicios = await _context.Servicios.ToListAsync();
                if (servicios != null)
                {
                    return Ok(servicios);
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

        // GET: api/Servicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> GetServicio(int id)
        {
            try
            {
                var servicioEnCore = await Core_GetServicio(id);
                if (servicioEnCore != null)
                {
                    return Ok(servicioEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var servicioLocal = await _context.Servicios.FindAsync(id);
                if (servicioLocal == null)
                {
                    return NotFound();
                }
                return Ok(servicioLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Servicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, Servicio servicio)
        {
            if (id != servicio.ServicioCodigo)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateServicio(id, servicio);
                if (result != null)
                {
                    _context.Entry(servicio).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(servicio).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(id))
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

        // POST: api/Servicio
        [HttpPost]
        public async Task<ActionResult<Servicio>> PostServicio(Servicio servicio)
        {
            try
            {
                var result = await Core_CreateServicio(servicio);
                if (result != null)
                {
                    _context.Servicios.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetServicio), new { id = result.ServicioCodigo }, result);
                }
                else
                {
                    return BadRequest("Error al crear el servicio en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Servicios.Add(servicio);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetServicio), new { id = servicio.ServicioCodigo }, servicio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Servicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            try
            {
                var result = await Core_DeleteServicio(id);
                if (result)
                {
                    var servicioLocal = await _context.Servicios.FindAsync(id);
                    if (servicioLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Servicios.Remove(servicioLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var servicioLocal = await _context.Servicios.FindAsync(id);
                if (servicioLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(servicioLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(id))
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
        private async Task<Servicio> Core_GetServicio(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var servicio = await response.Content.ReadAsAsync<Servicio>();
                return servicio;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Servicio>> Core_GetServicios()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var servicios = await response.Content.ReadFromJsonAsync<List<Servicio>>();
                return servicios;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener servicios del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Servicio> Core_CreateServicio(Servicio servicio)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(servicio), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/post", content);
            if (response.IsSuccessStatusCode)
            {
                var createdServicio = await response.Content.ReadAsAsync<Servicio>();
                return createdServicio;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Servicio> Core_UpdateServicio(int id, Servicio servicio)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(servicio), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/put/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return servicio;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteServicio(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.ServicioCodigo == id);
        }
    }
}
