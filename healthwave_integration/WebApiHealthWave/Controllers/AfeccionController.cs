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
    public class AfeccionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AfeccionController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/AfeccionControllerjson";

        public AfeccionController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<AfeccionController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Afeccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Afeccion>>> GetAfecciones()
        {
            try
            {
                var afeccionesEnCore = await Core_GetAfecciones();
                if (afeccionesEnCore != null)
                {
                    return Ok(afeccionesEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var afecciones = await _context.Afecciones.ToListAsync();
                if (afecciones != null)
                {
                    return Ok(afecciones);
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

        // GET: api/Afeccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Afeccion>> GetAfeccion(int id)
        {
            try
            {
                var afeccionEnCore = await Core_GetAfeccion(id);
                if (afeccionEnCore != null)
                {
                    return Ok(afeccionEnCore);
                }
                else
                {
                    return NotFound();
                }
            }

            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var afeccionLocal = await _context.Afecciones.FindAsync(id);
                if (afeccionLocal == null)
                {
                    return NotFound();
                }
                return Ok(afeccionLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Afeccion/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAfeccion(int id, Afeccion afeccion)
        {
            if (id != afeccion.IDAfeccion)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateAfeccion(id, afeccion);
                if (result != null)
                {
                    afeccion.Descripción = "Actualizado"; // Actualizar el estado según tu lógica
                    _context.Entry(afeccion).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                afeccion.Descripción = "Error"; // Actualizar el estado según tu lógica
                _context.Entry(afeccion).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfeccionExists(id))
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

        // POST: api/Afeccion

        [HttpPost]
        public async Task<ActionResult<Afeccion>> PostAfeccion(Afeccion afeccion)
        {
            try
            {
                var result = await Core_CreateAfeccion(afeccion);
                if (result != null)
                {
                    result.Descripción = "Creado"; // Actualizar el estado según tu lógica
                    _context.Afecciones.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetAfeccion), new { id = result.IDAfeccion }, result);
                }
                else
                {
                    return BadRequest("Error al crear la afección en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                afeccion.Descripción = "Error"; // Actualizar el estado según tu lógica
                _context.Afecciones.Add(afeccion);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAfeccion), new { id = afeccion.IDAfeccion }, afeccion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Afeccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAfeccion(int id)
        {
            try
            {
                var result = await Core_DeleteAfeccion(id);
                if (result)
                {
                    var afeccionLocal = await _context.Afecciones.FindAsync(id);
                    if (afeccionLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Afecciones.Remove(afeccionLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var afeccionLocal = await _context.Afecciones.FindAsync(id);
                if (afeccionLocal == null)
                {
                    return NotFound();
                }
                afeccionLocal.Descripción = "Eliminado"; // Actualizar el estado según tu lógica
                _context.Entry(afeccionLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfeccionExists(id))
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
        private async Task<Afeccion> Core_GetAfeccion(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var afeccion = await response.Content.ReadAsAsync<Afeccion>();
                return afeccion;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Afeccion>> Core_GetAfecciones()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}");
            if (response.IsSuccessStatusCode)
            {
                var afecciones = await response.Content.ReadFromJsonAsync<List<Afeccion>>();
                return afecciones;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener afecciones del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Afeccion> Core_CreateAfeccion(Afeccion afeccion)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(_coreBaseUrl, afeccion);
            if (response.IsSuccessStatusCode)
            {
                var createdAfeccion = await response.Content.ReadAsAsync<Afeccion>();
                return createdAfeccion;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Afeccion> Core_UpdateAfeccion(int id, Afeccion afeccion)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{_coreBaseUrl}/{id}", afeccion);
            if (response.IsSuccessStatusCode)
            {
                return afeccion;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteAfeccion(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool AfeccionExists(int id)
        {
            return _context.Afecciones.Any(e => e.IDAfeccion == id);
        }
    }
}
