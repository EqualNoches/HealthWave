namespace WebApiHealthWave.Models
{
    public class FacturaProducto
    {
        public int FacturaCodigoProducto { get; set; }
        public int IDProducto { get; set; }
        public int IDAutorizacion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public Factura? Factura { get; set; }
        public Producto? Producto { get; set; }
        public Autorizacion? Autorizacion { get; set; }
    }
}
