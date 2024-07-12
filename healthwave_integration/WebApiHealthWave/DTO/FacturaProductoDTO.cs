public class FacturaProductoDto
{
    public string FacturaCodigoProducto { get; set; }
    public int IDProducto { get; set; }
    public int? IDAutorizacion { get; set; }
    public decimal? Precio { get; set; }
    public int? Cantidad { get; set; }
}
