public class FacturaServicioDto
{
    public string FacturaCodigoServicio { get; set; }
    public int IDProducto { get; set; }
    public int? IDAutorizacion { get; set; }
    public decimal? Costo { get; set; }
    public string ServicioCodigo { get; set; }
}
