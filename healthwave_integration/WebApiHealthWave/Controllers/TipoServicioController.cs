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
    public class TipoServicioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TipoServicioController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/TipoServicioControllerJson";

        public TipoServicioController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<TipoServicioController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/TipoServicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServicio>>> GetTipoServicios()
        {
            try
            {
                var tipoServiciosEnCore = await Core_GetTipoServicios();
                if (tipoServiciosEnCore != null)
                {
                    return Ok(tipoServiciosEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var tipoServicios = await _context.TipoServicios.ToListAsync();
                if (tipoServicios != null)
                {
                    return Ok(tipoServicios);
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

        // GET: api/TipoServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoServicio>> GetTipoServicio(int id)
        {
            try
            {
                var tipoServicioEnCore = await Core_GetTipoServicio(id);
                if (tipoServicioEnCore != null)
                {
                    return Ok(tipoServicioEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var tipoServicioLocal = await _context.TipoServicios.FindAsync(id);
                if (tipoServicioLocal == null)
                {
                    return NotFound();
                }
                return Ok(tipoServicioLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/TipoServicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoServicio(int id, TipoServicio tipoServicio)
        {
            if (id != tipoServicio.TipoServicioId)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateTipoServicio(id, tipoServicio);
                if (result != null)
                {
                    _context.Entry(tipoServicio).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(tipoServicio).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoServicioExists(id))
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

        // POST: api/TipoServicio
        [HttpPost]
        public async Task<ActionResult<TipoServicio>> PostTipoServicio(TipoServicio tipoServicio)
        {
            try
            {
                var result = await Core_CreateTipoServicio(tipoServicio);
                if (result != null)
                {
                    _context.TipoServicios.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetTipoServicio), new { id = result.TipoServicioId }, result);
                }
                else
                {
                    return BadRequest("Error al crear el tipo de servicio en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.TipoServicios.Add(tipoServicio);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTipoServicio), new { id = tipoServicio.TipoServicioId }, tipoServicio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/TipoServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoServicio(int id)
        {
            try
            {
                var result = await Core_DeleteTipoServicio(id);
                if (result)
                {
                    var tipoServicioLocal = await _context.TipoServicios.FindAsync(id);
                    if (tipoServicioLocal == null)
                    {
                        return NotFound();
                    }
                    _context.TipoServicios.Remove(tipoServicioLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var tipoServicioLocal = await _context.TipoServicios.FindAsync(id);
                if (tipoServicioLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(tipoServicioLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoServicioExists(id))
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
        private async Task<TipoServicio> Core_GetTipoServicio(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var tipoServicio = await response.Content.ReadAsAsync<TipoServicio>();
                return tipoServicio;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<TipoServicio>> Core_GetTipoServicios()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var tipoServicios = await response.Content.ReadFromJsonAsync<List<TipoServicio>>();
                return tipoServicios;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener tipos de servicio del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<TipoServicio> Core_CreateTipoServicio(TipoServicio tipoServicio)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(tipoServicio), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/post", content);
            if (response.IsSuccessStatusCode)
            {
                var createdTipoServicio = await response.Content.ReadAsAsync<TipoServicio>();
                return createdTipoServicio;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<TipoServicio> Core_UpdateTipoServicio(int id, TipoServicio tipoServicio)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(tipoServicio), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return tipoServicio;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteTipoServicio(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool TipoServicioExists(int id)
        {
            return _context.TipoServicios.Any(e => e.TipoServicioId == id);
        }
    }
}
