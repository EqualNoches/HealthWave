using HospitalCore_core.Models;

namespace HospitalCore_core.DTO;



public class FacturaDto
{
    public string FacturaCodigo { get; set; }
    
    public decimal MontoTotal { get; set; } = 0.00m;
    
    public decimal MontoSubtotal { get; set; } = 0.00m;
    
    public DateTime Fecha { get; set; }
    
    public string Rnc { get; set; }
    public int? CodigoMetodoDePago { get; set; }
    
    public string CodigoPaciente { get; set; }

    public int? Idingreso { get; set; }

    public int? Idcuenta { get; set; }

    public int? ConsultaCodigo { get; set; }


    public static FacturaDto FromModel(Factura factura)
    {
        return new FacturaDto
        {
            FacturaCodigo = factura.FacturaCodigo,
            MontoTotal = factura.MontoTotal,
            MontoSubtotal = factura.MontoSubtotal,
            Fecha = factura.Fecha,
            Rnc = factura.Rnc,
            CodigoMetodoDePago = factura.CodigoMetodoDePago,
            CodigoPaciente = factura.CodigoPaciente,
            Idingreso = factura.Idingreso,
            Idcuenta = factura.Idcuenta,
            ConsultaCodigo = factura.ConsultaCodigo
        };
    }
}