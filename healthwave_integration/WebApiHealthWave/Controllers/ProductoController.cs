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
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductoController> _logger;
        private readonly string _coreBaseUrl = "https://localhost:7181/api/ProductoControllerJson"; // URL de tu API de core

        public ProductoController(AppDbContext context, IHttpClientFactory httpClientFactory, ILogger<ProductoController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                var productosEnCore = await Core_GetProductos();
                if (productosEnCore != null)
                {
                    return Ok(productosEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var productos = await _context.Productos.ToListAsync();
                if (productos != null)
                {
                    return Ok(productos);
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

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            try
            {
                var productoEnCore = await Core_GetProducto(id);
                if (productoEnCore != null)
                {
                    return Ok(productoEnCore);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var productoLocal = await _context.Productos.FindAsync(id);
                if (productoLocal == null)
                {
                    return NotFound();
                }
                return Ok(productoLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.IDProducto)
            {
                return BadRequest();
            }

            try
            {
                var result = await Core_UpdateProducto(id, producto);
                if (result != null)
                {
                    _context.Entry(producto).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Entry(producto).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(id))
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

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                var result = await Core_CreateProducto(producto);
                if (result != null)
                {
                    _context.Productos.Add(result);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetProducto), new { id = result.IDProducto }, result);
                }
                else
                {
                    return BadRequest("Error al crear el producto en el Core.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProducto), new { id = producto.IDProducto }, producto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la aplicación: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var result = await Core_DeleteProducto(id);
                if (result)
                {
                    var productoLocal = await _context.Productos.FindAsync(id);
                    if (productoLocal == null)
                    {
                        return NotFound();
                    }
                    _context.Productos.Remove(productoLocal);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error al intentar conectar con el Core: {ex.Message}");
                var productoLocal = await _context.Productos.FindAsync(id);
                if (productoLocal == null)
                {
                    return NotFound();
                }
                _context.Entry(productoLocal).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(id))
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
        private async Task<Producto> Core_GetProducto(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var producto = await response.Content.ReadAsAsync<Producto>();
                return producto;
            }
            else
            {
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<List<Producto>> Core_GetProductos()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_coreBaseUrl}/get");
            if (response.IsSuccessStatusCode)
            {
                var productos = await response.Content.ReadFromJsonAsync<List<Producto>>();
                return productos;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al intentar obtener productos del Core: {content}");
                return null;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Producto> Core_CreateProducto(Producto producto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_coreBaseUrl}/post", content);
            if (response.IsSuccessStatusCode)
            {
                var createdProducto = await response.Content.ReadAsAsync<Producto>();
                return createdProducto;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Producto> Core_UpdateProducto(int id, Producto producto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_coreBaseUrl}/update/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return producto;
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Core_DeleteProducto(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_coreBaseUrl}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IDProducto == id);
        }
    }
}
