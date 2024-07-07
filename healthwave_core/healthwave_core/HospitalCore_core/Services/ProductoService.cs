using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Services
{
    public class ProductoService(HospitalCore dbContext) : IProductoService
    {
        private readonly LogManager<ProductoService> _logManager = new();

        public async Task<List<Producto?>> GetProductosAsync()
        {
            try
            {
                var productos = await dbContext.Productos.ToListAsync();
                _logManager.LogInfo("Productos obtenidos exitosamente");
                return productos;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al obtener productos de aseguradora", ex);
                throw;
            }
        }

        public async Task<Producto?> GetProductoId(int id)
        {
            try
            {
                var producto = await dbContext.Productos.FindAsync(id);
                _logManager.LogInfo($"Producto {id} obtenido exitosamente");
                return producto;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal($"Error al obtener producto {id}", ex);
                throw;
            }
        }

        public async Task<int> AddProductoAsync(ProductoDto producto)
        {
            try
            {
                Producto result = Producto.FromDto(producto);
                dbContext.Productos.Add(result);
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo("Producto creado exitosamente");
                return result.Idproducto;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al crear producto", ex);
                throw;
            }
        }

        public async Task<int> UpdateProducto(Producto producto)
        {
            try
            {
                dbContext.Entry(producto).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Producto {producto.Idproducto} actualizado exitosamente");
                return 1;
            }
            catch (DbUpdateException ex)
            {
                if (!ProductoExist((uint)producto.Idproducto))
                {
                    _logManager.LogInfo($"El producto {producto.Idproducto} no existe");
                    return 0;
                }
                _logManager.LogFatal("Error al actualizar el producto", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al actualizar los productos", ex);
                throw;
            }
        }

        public async Task<int> DeleteProductoAsync(int id)
        {
            try
            {
                var producto = await dbContext.Productos.FindAsync(id);
                if (producto == null)
                {
                    _logManager.LogInfo($"El producto {id} no existe");
                    return 0;
                }

                dbContext.Productos.Remove(producto);
                await dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Producto {id} eliminado exitosamente");
                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error eliminando el producto del registro", ex);
                throw;
            }
        }

        private bool ProductoExist(uint id)
        {
            return dbContext.Productos.Any(e => e.Idproducto == id);
        }
    }
}
