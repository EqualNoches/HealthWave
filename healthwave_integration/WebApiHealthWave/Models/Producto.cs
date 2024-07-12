using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Producto
    {
        public int IDProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripción { get; set; }
        public decimal Precio { get; set; }

        public virtual ICollection<FacturaServicio> FacturaServicios { get; set; } = new List<FacturaServicio>();
        public virtual ICollection<PrescripcionProducto> PrescripcionProductos { get; set; } = new List<PrescripcionProducto>();

        public virtual ICollection<FacturaProducto> FacturaProductos { get; set; } = new List<FacturaProducto>();

        public static Producto FromDto(ProductoDto dto)
        {
            return new Producto
            {
                IDProducto = dto.IDProducto,
                Nombre = dto.Nombre,
                Descripción = dto.Descripción,
                Precio = dto.Precio
            };
        }
    }
}
