using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SalaController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/SalaControllerJson";

        public SalaController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<SalaController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Sala
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
        {
            try
            {
                var salasEnCore = await Core_GetSalas();
                if (salasEnCore != null)
                {
                    return Ok(salasEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var salas = await _context.Salas.ToListAsync();
                if (salas != null)
                {
                    return Ok(salas);
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

        // GET: api/Sala/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
            try
            {
                var salaEnCore = await Core_GetSala(id);
                if (salaEnCore != null)
                {
                    return Ok(salaEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var salaLocal = await _context.Salas.FindAsync(id);
                if (salaLocal == null)
                {
                    return NotFound();
                }
                return Ok(salaLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Sala/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(int id, Sala sala)
        {
            if (id != sala.NumSala)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateSala(id, sala);
                if (result != null)
                {
                    _context.Entry(sala).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(sala).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(id))
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

        // POST: api/Sala
        [HttpPost]
        public async Task<ActionResult<Sala>> PostSala(Sala sala)
        {
            try
            {
                var result = await Core_CreateSala(sala);
                if (result != null)
                {
                    _context.Salas.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetSala), new { id = result.NumSala }, result);
                }
                else
                {
                    return BadRequest("Error al crear la sala en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Salas.Add(sala);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetSala), new { id = sala.NumSala }, sala);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Sala/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            try
            {
                var result = await Core_DeleteSala(id);
                if (result)
                {
                    var salaLocal = await _context.Salas.FindAsync(id);
                    if (salaLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Salas.Remove(salaLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var salaLocal = await _context.Salas.FindAsync(id);
                if (salaLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(salaLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(id))
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
        private async Task<Sala> Core_GetSala(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var sala = await response.Content.ReadAsAsync<Sala>();
                return sala;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Sala>> Core_GetSalas()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var salas = await response.Content.ReadFromJsonAsync<List<Sala>>();
                return salas;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener salas del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Sala> Core_CreateSala(Sala sala)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(sala), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/post", content);
            if (response.IsSuccessStatusCode)
            {
                var createdSala = await response.Content.ReadAsAsync<Sala>();
                return createdSala;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Sala> Core_UpdateSala(int id, Sala sala)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(sala), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return sala;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteSala(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool SalaExists(int id)
        {
            return _context.Salas.Any(e => e.NumSala == id);
        }
    }
}
