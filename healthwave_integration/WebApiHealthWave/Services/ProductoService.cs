using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<ProductoService> _logHandler = new();

        public ProductoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductoDto>> GetProductosAsync()
        {
            try
            {
                var productos = await _dbContext.Productos
                    .Select(p => ProductoDto.FromModel(p))
                    .ToListAsync();

                _logHandler.LogInfo("Productos retrieved successfully.");
                return productos;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve productos.", ex);
                throw;
            }
        }

        public async Task<int> AddProductoAsync(ProductoDto productoDto)
        {
            try
            {
                var producto = Producto.FromDto(productoDto);
                _dbContext.Productos.Add(producto);
                await _dbContext.SaveChangesAsync();

                _logHandler.LogInfo("Producto added successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to add producto.", ex);
                throw;
            }
        }

        public async Task<int> UpdateProductoAsync(ProductoDto productoDto)
        {
            try
            {
                var producto = await _dbContext.Productos.FindAsync(productoDto.IDProducto);
                if (producto == null)
                {
                    _logHandler.LogInfo("Producto not found.");
                    return 0;
                }

                producto.Nombre = productoDto.Nombre;
                producto.Descripción = productoDto.Descripción;
                producto.Precio = productoDto.Precio;

                _dbContext.Productos.Update(producto);
                await _dbContext.SaveChangesAsync();

                _logHandler.LogInfo("Producto updated successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to update producto.", ex);
                throw;
            }
        }

        public async Task<int> DeleteProductoAsync(uint idProducto)
        {
            try
            {
                var producto = await _dbContext.Productos.FindAsync(idProducto);
                if (producto == null)
                {
                    _logHandler.LogInfo("Producto not found.");
                    return 0;
                }

                _dbContext.Productos.Remove(producto);
                await _dbContext.SaveChangesAsync();

                _logHandler.LogInfo("Producto deleted successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to delete producto.", ex);
                throw;
            }
        }

        public async Task<int> AddProductoProveedorAsync(uint idProducto, uint rncProveedor)
        {
            try
            {
                var producto = await _dbContext.Productos.Include(p => p.FacturaProductos).FirstOrDefaultAsync(p => p.IDProducto == idProducto);
                var proveedor = await _dbContext.FacturaProductos.FindAsync(rncProveedor);
                if (producto != null && proveedor != null)
                {
                    producto.FacturaProductos.Add(proveedor);
                    await _dbContext.SaveChangesAsync();

                    _logHandler.LogInfo("Proveedor added to producto successfully.");
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to add proveedor to producto.", ex);
                throw;
            }
        }

        public async Task<int> DeleteProductoProveedorAsync(uint idProducto, uint rncProveedor)
        {
            try
            {
                var producto = await _dbContext.Productos.Include(p => p.FacturaProductos).FirstOrDefaultAsync(p => p.IDProducto == idProducto);
                var proveedor = producto?.FacturaProductos.FirstOrDefault(p => p.IDProducto == rncProveedor);
                if (producto != null && proveedor != null)
                {
                    producto.FacturaProductos.Remove(proveedor);
                    await _dbContext.SaveChangesAsync();

                    _logHandler.LogInfo("Proveedor removed from producto successfully.");
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to remove proveedor from producto.", ex);
                throw;
            }
        }

        public async Task<List<uint>> GetProductoProveedoresAsync(uint idProducto)
        {
            try
            {
                var producto = await _dbContext.Productos.Include(p => p.FacturaProductos).FirstOrDefaultAsync(p => p.IDProducto == idProducto);
                var proveedores = producto?.FacturaProductos.Select(p => (uint)p.IDProducto).ToList() ?? new List<uint>();

                _logHandler.LogInfo("Proveedores for producto retrieved successfully.");
                return proveedores;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve proveedores for producto.", ex);
                throw;
            }
        }
    }
}
