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
    public class CuentaCobrarController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CuentaCobrarController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/CuentaCobrarControllerjson";

        public CuentaCobrarController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<CuentaCobrarController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/CuentaCobrar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaCobrar>>> GetCuentasCobrar()
        {
            try
            {
                var cuentasEnCore = await Core_GetCuentasCobrar();
                if (cuentasEnCore != null)
                {
                    return Ok(cuentasEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var cuentas = await _context.CuentasCobrar.ToListAsync();
                if (cuentas != null)
                {
                    return Ok(cuentas);
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

        // GET: api/CuentaCobrar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaCobrar>> GetCuentaCobrar(int id)
        {
            try
            {
                var cuentaEnCore = await Core_GetCuentaCobrar(id);
                if (cuentaEnCore != null)
                {
                    return Ok(cuentaEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var cuentaLocal = await _context.CuentasCobrar.FindAsync(id);
                if (cuentaLocal == null)
                {
                    return NotFound();
                }
                return Ok(cuentaLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/CuentaCobrar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentaCobrar(int id, CuentaCobrar cuentaCobrar)
        {
            if (id != cuentaCobrar.IDCuenta)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateCuentaCobrar(id, cuentaCobrar);
                if (result != null)
                {
                    _context.Entry(cuentaCobrar).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(cuentaCobrar).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentaCobrarExists(id))
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

        // POST: api/CuentaCobrar
        [HttpPost]
        public async Task<ActionResult<CuentaCobrar>> PostCuentaCobrar(CuentaCobrar cuentaCobrar)
        {
            try
            {
                var result = await Core_CreateCuentaCobrar(cuentaCobrar);
                if (result != null)
                {
                    _context.CuentasCobrar.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetCuentaCobrar), new { id = result.IDCuenta }, result);
                }
                else
                {
                    return BadRequest("Error al crear la cuenta por cobrar en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.CuentasCobrar.Add(cuentaCobrar);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCuentaCobrar), new { id = cuentaCobrar.IDCuenta }, cuentaCobrar);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/CuentaCobrar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentaCobrar(int id)
        {
            try
            {
                var result = await Core_DeleteCuentaCobrar(id);
                if (result)
                {
                    var cuentaLocal = await _context.CuentasCobrar.FindAsync(id);
                    if (cuentaLocal == null)
                    {
                        return NotFound();
                    }
                    _context.CuentasCobrar.Remove(cuentaLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var cuentaLocal = await _context.CuentasCobrar.FindAsync(id);
                if (cuentaLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(cuentaLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentaCobrarExists(id))
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
        private async Task<CuentaCobrar> Core_GetCuentaCobrar(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var cuenta = await response.Content.ReadAsAsync<CuentaCobrar>();
                return cuenta;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<CuentaCobrar>> Core_GetCuentasCobrar()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var cuentas = await response.Content.ReadFromJsonAsync<List<CuentaCobrar>>();
                return cuentas;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener cuentas por cobrar del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<CuentaCobrar> Core_CreateCuentaCobrar(CuentaCobrar cuentaCobrar)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(cuentaCobrar), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/post", content);
            if (response.IsSuccessStatusCode)
            {
                var createdCuenta = await response.Content.ReadAsAsync<CuentaCobrar>();
                return createdCuenta;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<CuentaCobrar> Core_UpdateCuentaCobrar(int id, CuentaCobrar cuentaCobrar)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(cuentaCobrar), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return cuentaCobrar;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteCuentaCobrar(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool CuentaCobrarExists(int id)
        {
            return _context.CuentasCobrar.Any(e => e.IDCuenta == id);
        }
    }
}
