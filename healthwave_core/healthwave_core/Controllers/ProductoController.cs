using System.Net;
using System.Net.NetworkInformation;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace HospitalCore_core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductoController(IProductoService productoService) : ControllerBase
{
    private readonly LogManager<ProductoController> _logManager = new();

    [HttpPost("post")]
    public async Task<ActionResult> PostProducto([FromQuery]ProductoDto producto)
    {
        try
        {
            var result = await productoService.AddProductoAsync(producto);
            if (result != 0)
                return Ok("Product added successfully.");

            return BadRequest();
        }
        catch (Exception ex)
        {
            _logManager.LogError("Ocurrio un errro agregando el producto.", ex);
            return StatusCode(500, "Ocurrio un error en el servidor tratando de agregar el producto.");
        }
    }

    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<Producto?>>> GetAllProductos()
    {
        try
        {
            return await productoService.GetProductosAsync();
        }
        catch (Exception x)
        {
            _logManager.LogFatal("Error al obtener los productos", x);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener productos");
        }
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<Producto?>> GetProductosId(uint id)
    {
        try
        {
            var producto = await productoService.GetProductoId((int)id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }
        catch (Exception ex)
        {
            _logManager.LogFatal($"Error al obtener el producto {id}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener producto {id}");
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<int>> UpdateProducto(Producto producto)
    {
        try
        {
            var answer = await productoService.UpdateProducto(producto);
            if (answer == 0)
            {
                return NotFound();
            }

            return answer;
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error al actualizar los productos",ex);
            throw;
        }
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult<int>> DeleteProducto(int id)
    {
        try
        {
            var answer = await productoService.DeleteProductoAsync(id);
            if (answer == 0)
            {
                return NotFound();
            }

            return Ok(answer);
        }
        catch (Exception ex)
        {
            _logManager.LogFatal($"Error al eliminar aseguradora con {id}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el producto en el servidor");
        }
    }
    
}