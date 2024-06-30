using HospitalCore_core.Context;
using NLog;
using HospitalCore_core.Models;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Services;

public class ProductoService(HospitalCore dbContext): IProductoService
{
    private readonly LogManager<ProductoService> _logManager = new();


    public async Task<List<Producto?>> GetProductosAsync()
    {
        try
        {
            return await dbContext.Productos.ToListAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error al Obtener Productos de aseguradora", ex);
            throw;
        }
    }
    
    public async Task<Producto?> GetProductoId(int id)
    {
        try
        {
            return await dbContext.Productos.FindAsync(id);
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
            return await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error al crear Producto",ex);
            throw;
        }
    }

    public async Task<int> UpdateProducto(Producto producto)
    {
        try
        {
            dbContext.Entry(producto).State = EntityState.Modified;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!ProductoExist((uint)producto.Idproducto))
                {
                    return 0;
                }

                throw;
            }

            return 1;
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error al actualizar los productos",ex);
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
                return 0;
            }

            dbContext.Productos.Remove(producto);
            await dbContext.SaveChangesAsync();

            return 1;
        }
        catch (Exception ex)
        {
            _logManager.LogFatal("Error Eliminando el producto del registro",ex);
            throw;
        }
    }

    private bool ProductoExist(uint id)
    {
        return dbContext.Productos.Any(e => e.Idproducto == id);
    }
}
