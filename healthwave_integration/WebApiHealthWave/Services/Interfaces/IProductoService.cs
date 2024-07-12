using WebApiHealthWave.Data;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> GetProductosAsync();
        Task<int> AddProductoAsync(ProductoDto producto);
        Task<int> UpdateProductoAsync(ProductoDto producto);
        Task<int> DeleteProductoAsync(uint idProducto);
        Task<int> AddProductoProveedorAsync(uint idProducto, uint rncProveedor);
        Task<int> DeleteProductoProveedorAsync(uint idProducto, uint rncProveedor);
        Task<List<uint>> GetProductoProveedoresAsync(uint idProducto);
    }
}
