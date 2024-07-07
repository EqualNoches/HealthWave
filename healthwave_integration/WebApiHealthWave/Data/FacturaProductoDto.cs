namespace WebApiHealthWave.Data
{
    public class FacturaProductoDto
    {
        public int FacturaCodigoProducto { get; set; }
        public int IDProducto { get; set; }
        public int IDAutorizacion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
