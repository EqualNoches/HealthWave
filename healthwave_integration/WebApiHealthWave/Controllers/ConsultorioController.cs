using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ConsultorioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ConsultorioController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/ConsultorioControllerjson";

        public ConsultorioController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<ConsultorioController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Consultorio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consultorio>>> GetConsultorios()
        {
            try
            {
                var consultoriosEnCore = await Core_GetConsultorios();
                if (consultoriosEnCore != null)
                {
                    return Ok(consultoriosEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var consultorios = await _context.Consultorios.ToListAsync();
                if (consultorios != null)
                {
                    return Ok(consultorios);
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

        // GET: api/Consultorio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consultorio>> GetConsultorio(int id)
        {
            try
            {
                var consultorioEnCore = await Core_GetConsultorio(id);
                if (consultorioEnCore != null)
                {
                    return Ok(consultorioEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var consultorioLocal = await _context.Consultorios.FindAsync(id);
                if (consultorioLocal == null)
                {
                    return NotFound();
                }
                return Ok(consultorioLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Consultorio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultorio(int id, Consultorio consultorio)
        {
            if (id != consultorio.IDConsultorio)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateConsultorio(id, consultorio);
                if (result != null)
                {
                    _context.Entry(consultorio).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(consultorio).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultorioExists(id))
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

        // POST: api/Consultorio
        [HttpPost]
        public async Task<ActionResult<Consultorio>> PostConsultorio(Consultorio consultorio)
        {
            try
            {
                var result = await Core_CreateConsultorio(consultorio);
                if (result != null)
                {
                    _context.Consultorios.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetConsultorio), new { id = result.IDConsultorio }, result);
                }
                else
                {
                    return BadRequest("Error al crear el consultorio en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Consultorios.Add(consultorio);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetConsultorio), new { id = consultorio.IDConsultorio }, consultorio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Consultorio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultorio(int id)
        {
            try
            {
                var result = await Core_DeleteConsultorio(id);
                if (result)
                {
                    var consultorioLocal = await _context.Consultorios.FindAsync(id);
                    if (consultorioLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Consultorios.Remove(consultorioLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var consultorioLocal = await _context.Consultorios.FindAsync(id);
                if (consultorioLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(consultorioLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultorioExists(id))
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
        private async Task<Consultorio> Core_GetConsultorio(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var consultorio = await response.Content.ReadAsAsync<Consultorio>();
                return consultorio;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Consultorio>> Core_GetConsultorios()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var consultorios = await response.Content.ReadFromJsonAsync<List<Consultorio>>();
                return consultorios;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener consultorios del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Consultorio> Core_CreateConsultorio(Consultorio consultorio)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(consultorio), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/post", content);
            if (response.IsSuccessStatusCode)
            {
                var createdConsultorio = await response.Content.ReadAsAsync<Consultorio>();
                return createdConsultorio;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Consultorio> Core_UpdateConsultorio(int id, Consultorio consultorio)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(consultorio), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return consultorio;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteConsultorio(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool ConsultorioExists(int id)
        {
            return _context.Consultorios.Any(e => e.IDConsultorio == id);
        }
    }
}
