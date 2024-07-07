namespace WebApiHealthWave.Models
{
    public class FacturaServicio
    {
        public int FacturaCodigoServicio { get; set; }
        public int IDProducto { get; set; }
        public int? IDAutorizacion { get; set; }
        public decimal Costo { get; set; }
        public string? ServicioCodigo { get; set; }

        public Factura? Factura { get; set; }
        public Producto? Producto { get; set; }
        public Autorizacion? Autorizacion { get; set; }
    }
}
