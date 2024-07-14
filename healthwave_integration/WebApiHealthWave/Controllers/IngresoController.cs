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
using System.Text;
using System.Text.Json;

namespace WebApiHealthWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IngresoController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/IngresoControllerJson";

        public IngresoController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<IngresoController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Ingreso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingreso>>> GetIngresos()
        {
            try
            {
                var ingresosEnCore = await Core_GetIngresos();
                if (ingresosEnCore != null)
                {
                    return Ok(ingresosEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var ingresos = await _context.Ingresos.ToListAsync();
                if (ingresos != null)
                {
                    return Ok(ingresos);
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

        // GET: api/Ingreso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingreso>> GetIngreso(int id)
        {
            try
            {
                var ingresoEnCore = await Core_GetIngreso(id);
                if (ingresoEnCore != null)
                {
                    return Ok(ingresoEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var ingresoLocal = await _context.Ingresos.FindAsync(id);
                if (ingresoLocal == null)
                {
                    return NotFound();
                }
                return Ok(ingresoLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Ingreso/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngreso(int id, Ingreso ingreso)
        {
            if (id != ingreso.IDIngreso)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateIngreso(id, ingreso);
                if (result != null)
                {
                    _context.Entry(ingreso).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(ingreso).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoExists(id))
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

        // POST: api/Ingreso
        [HttpPost]
        public async Task<ActionResult<Ingreso>> PostIngreso(Ingreso ingreso)
        {
            try
            {
                var result = await Core_CreateIngreso(ingreso);
                if (result != null)
                {
                    _context.Ingresos.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetIngreso), new { id = result.IDIngreso }, result);
                }
                else
                {
                    return BadRequest("Error al crear el ingreso en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Ingresos.Add(ingreso);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetIngreso), new { id = ingreso.IDIngreso }, ingreso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Ingreso/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngreso(int id)
        {
            try
            {
                var result = await Core_DeleteIngreso(id);
                if (result)
                {
                    var ingresoLocal = await _context.Ingresos.FindAsync(id);
                    if (ingresoLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Ingresos.Remove(ingresoLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var ingresoLocal = await _context.Ingresos.FindAsync(id);
                if (ingresoLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(ingresoLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoExists(id))
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
        private async Task<Ingreso> Core_GetIngreso(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var ingreso = await response.Content.ReadAsAsync<Ingreso>();
                return ingreso;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Ingreso>> Core_GetIngresos()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var ingresos = await response.Content.ReadFromJsonAsync<List<Ingreso>>();
                return ingresos;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener ingresos del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Ingreso> Core_CreateIngreso(Ingreso ingreso)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(ingreso), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/add", content);
            if (response.IsSuccessStatusCode)
            {
                var createdIngreso = await response.Content.ReadAsAsync<Ingreso>();
                return createdIngreso;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Ingreso> Core_UpdateIngreso(int id, Ingreso ingreso)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(ingreso), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return ingreso;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteIngreso(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool IngresoExists(int id)
        {
            return _context.Ingresos.Any(e => e.IDIngreso == id);
        }
    }
}
