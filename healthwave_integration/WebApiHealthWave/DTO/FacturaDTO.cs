public class FacturaDto
{
    public string FacturaCodigo { get; set; }
    public decimal? MontoTotal { get; set; }
    public decimal? MontoSubtotal { get; set; }
    public DateTime Fecha { get; set; }
    public string RNC { get; set; }
    public int? CodigoMetodoDePago { get; set; }
    public string CodigoPaciente { get; set; }
    public int? IDIngreso { get; set; }
    public int? IDCuenta { get; set; }
    public int? ConsultaCodigo { get; set; }
}
