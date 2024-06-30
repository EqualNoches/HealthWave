using HospitalCore_core.Models;

namespace HospitalCore_core.DTO;


public class FacturaDto
{
    public string FacturaCodigo { get; set; } = null!;
    public decimal? MontoTotal { get; set; }
    public decimal? MontoSubtotal { get; set; }
    public DateOnly Fecha { get; set; }
    public string? Rnc { get; set; }
    public int? CodigoMetodoDePago { get; set; }
    public string? CodigoPaciente { get; set; }
    public int? Idingreso { get; set; }
    public int? Idcuenta { get; set; }
    public int? ConsultaCodigo { get; set; }
    public List<FacturaProductoDto> FacturaProductos { get; set; } = new List<FacturaProductoDto>();
    public List<FacturaServicioDto> FacturaServicios { get; set; } = new List<FacturaServicioDto>();
}