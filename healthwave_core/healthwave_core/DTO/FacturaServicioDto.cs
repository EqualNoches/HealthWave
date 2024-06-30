namespace HospitalCore_core.DTO;

public class FacturaServicioDto
{
    public string FacturaCodigo { get; set; } = null!;
    public int Idproducto { get; set; }
    public int? Idautorizacion { get; set; }
    public decimal? Costo { get; set; }
    public string? ServicioCodigo { get; set; }
}