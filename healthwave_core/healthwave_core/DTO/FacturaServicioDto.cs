using HospitalCore_core.Models;

namespace HospitalCore_core.DTO;

public class FacturaServicioDto
{
    public string FacturaCodigoServicio { get; set; } = null!;

    public int Idproducto { get; set; }

    public int? Idautorizacion { get; set; }

    public decimal? Costo { get; set; }

    public string? ServicioCodigo { get; set; }

    public static FacturaServicioDto FromModel(FacturaServicio facturaServicio)
    {
        return new FacturaServicioDto
        {
            FacturaCodigoServicio = facturaServicio.FacturaCodigoServicio,
            Idproducto = facturaServicio.Idproducto,
            Idautorizacion = facturaServicio.Idautorizacion,
            Costo = facturaServicio.Costo,
            ServicioCodigo = facturaServicio.ServicioCodigo
        };
    }
}