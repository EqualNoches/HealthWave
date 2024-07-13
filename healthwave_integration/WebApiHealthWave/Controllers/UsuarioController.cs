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
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UsuarioController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/UsuarioControllerjson"; // URL de tu API de integración

        public UsuarioController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<UsuarioController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            try
            {
                var usuariosEnCore = await Core_GetUsuarios();
                if (usuariosEnCore != null)
                {
                    return Ok(usuariosEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var usuarios = await _context.Usuarios.ToListAsync();
                if (usuarios != null)
                {
                    return Ok(usuarios);
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

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            try
            {
                var usuarioEnCore = await Core_GetUsuario(id);
                if (usuarioEnCore != null)
                {
                    return Ok(usuarioEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var usuarioLocal = await _context.Usuarios.FindAsync(id);
                if (usuarioLocal == null)
                {
                    return NotFound();
                }
                return Ok(usuarioLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(string id, Usuario usuario)
        {
            if (id != usuario.UsuarioCodigo)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateUsuario(id, usuario);
                if (result != null)
                {
                    _context.Entry(usuario).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(usuario).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(id))
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

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            try
            {
                var result = await Core_CreateUsuario(usuario);
                if (result != null)
                {
                    _context.Usuarios.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetUsuario), new { id = result.UsuarioCodigo }, result);
                }
                else
                {
                    return BadRequest("Error al crear el usuario en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.UsuarioCodigo }, usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            try
            {
                var result = await Core_DeleteUsuario(id);
                if (result)
                {
                    var usuarioLocal = await _context.Usuarios.FindAsync(id);
                    if (usuarioLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Usuarios.Remove(usuarioLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var usuarioLocal = await _context.Usuarios.FindAsync(id);
                if (usuarioLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(usuarioLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(id))
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
        private async Task<Usuario> Core_GetUsuario(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var usuario = await response.Content.ReadAsAsync<Usuario>();
                return usuario;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Usuario>> Core_GetUsuarios()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var usuarios = await response.Content.ReadFromJsonAsync<List<Usuario>>();
                return usuarios;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener usuarios del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Usuario> Core_CreateUsuario(Usuario usuario)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/add", content);
            if (response.IsSuccessStatusCode)
            {
                var createdUsuario = await response.Content.ReadAsAsync<Usuario>();
                return createdUsuario;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Usuario> Core_UpdateUsuario(string id, Usuario usuario)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return usuario;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteUsuario(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuarios.Any(e => e.UsuarioCodigo == id);
        }
    }
}
