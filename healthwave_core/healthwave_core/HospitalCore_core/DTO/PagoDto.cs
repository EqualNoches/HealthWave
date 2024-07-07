using HospitalCore_core.Models;

namespace HospitalCore_core.DTO;

public class PagoDto
{
    public int IdPago { get; set; }
    
    public int? IdCuenta { get; set; }
    
    public decimal? MontoPagado { get; set; }
    
    public DateOnly FechaPago { get; set; }

    public static PagoDto FromModel(Pago? pago)
    {
        return new PagoDto()
        {
            IdPago = pago.Idpago,
            IdCuenta = pago.Idcuenta,
            MontoPagado = pago.MontoPagado,
            FechaPago = pago.Fecha
            
        };
    }
}