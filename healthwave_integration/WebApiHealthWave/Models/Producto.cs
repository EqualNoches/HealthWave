namespace WebApiHealthWave.Models
{
    public class Producto
    {
        public int IDProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripción { get; set; }
        public decimal Precio { get; set; }

        public ICollection<FacturaServicio>? FacturaServicios { get; set; }
        public ICollection<PrescripcionProducto>? PrescripcionProductos { get; set; } 

        public ICollection<FacturaProducto>? FacturaProductos { get; set; } 

    }
}
