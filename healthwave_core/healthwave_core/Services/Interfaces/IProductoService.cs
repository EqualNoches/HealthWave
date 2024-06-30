using HospitalCore_core.DTO;
using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces;

public interface IProductoService
{
    Task<List<Producto?>> GetProductosAsync();

    Task<int> AddProductoAsync(ProductoDto producto);
    Task<Producto?> GetProductoId(int id);
    Task<int> UpdateProducto(Producto producto);
    Task<int> DeleteProductoAsync(int id);
}