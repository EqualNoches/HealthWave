using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class FacturaDto
    {
        public int FacturaCodigo { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoSubtotal { get; set; }
        public DateTime Fecha { get; set; }
        public string? RNC { get; set; }
        public int CodigoMetodoDePago { get; set; }
        public string? CodigoPaciente { get; set; }
        public int? IDIngreso { get; set; }
        public int? IDCuenta { get; set; }
        public int? ConsultaCodigo { get; set; }

        public static FacturaDto FromModel(Factura factura)
        {
            return new FacturaDto
            {
                FacturaCodigo = factura.FacturaCodigo,
                MontoTotal = factura.MontoTotal,
                MontoSubtotal = factura.MontoSubtotal,
                Fecha = factura.Fecha,
                RNC = factura.RNC,
                CodigoMetodoDePago = factura.CodigoMetodoDePago,
                CodigoPaciente = factura.CodigoPaciente,
                IDIngreso = factura.IDIngreso,
                IDCuenta = factura.IDCuenta,
                ConsultaCodigo = factura.ConsultaCodigo
            };
        }
    }
}
