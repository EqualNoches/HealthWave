namespace HospitalCore_core.DTO;

public class FacturaProductoDto
{
    public string FacturaCodigo { get; set; } = null!;
    public int Idproducto { get; set; }
    public int? Idautorizacion { get; set; }
    public decimal? Precio { get; set; }
    public int? Cantidad { get; set; }
}