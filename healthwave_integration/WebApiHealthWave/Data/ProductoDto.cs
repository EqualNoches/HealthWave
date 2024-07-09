using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class ProductoDto
    {
        public int IDProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripción { get; set; }
        public decimal Precio { get; set; }

        public static ProductoDto? FromModel(Producto model)
        {
            return new ProductoDto
            {
                IDProducto = model.IDProducto,
                Nombre = model.Nombre,
                Descripción = model.Descripción,
                Precio = model.Precio
            };
        }
    }
}
