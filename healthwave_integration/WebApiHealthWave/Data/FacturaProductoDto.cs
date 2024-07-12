using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class FacturaProductoDto
    {
        public int FacturaCodigoProducto { get; set; }
        public int IDProducto { get; set; }
        public int IDAutorizacion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public static FacturaProductoDto FromModel(FacturaProducto facturaProducto)
        {
            return new FacturaProductoDto
            {
                FacturaCodigoProducto = facturaProducto.FacturaCodigoProducto,
                IDProducto = facturaProducto.IDProducto,
                IDAutorizacion = facturaProducto.IDAutorizacion,
                Precio = facturaProducto.Precio,
                Cantidad = facturaProducto.Cantidad
            };
        }
    }
}
