using HospitalCore_core.DTO;

namespace HospitalCore_core.Models;

public partial class FacturaServicio
{
    public string FacturaCodigoServicio { get; set; } = null!;

    public int Idproducto { get; set; }

    public int? Idautorizacion { get; set; }

    public decimal? Costo { get; set; }

    public string? ServicioCodigo { get; set; }

    public virtual Factura FacturaCodigoNavigation { get; set; } = null!;

    public virtual Autorizacion? IdautorizacionNavigation { get; set; }

    public virtual Producto IdproductoNavigation { get; set; } = null!;
    
    public static FacturaServicio FromDto(FacturaServicioDto facturaServicioDto)
    {
        return new FacturaServicio
        {
            FacturaCodigoServicio = facturaServicioDto.FacturaCodigoServicio,
            Idproducto = facturaServicioDto.Idproducto,
            Idautorizacion = facturaServicioDto.Idautorizacion,
            Costo = facturaServicioDto.Costo,
            ServicioCodigo = facturaServicioDto.ServicioCodigo
        };
    }
}