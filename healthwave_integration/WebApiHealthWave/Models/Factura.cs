namespace WebApiHealthWave.Models
{
    public class Factura
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

        public MetodoDePago? MetodoDePago { get; set; }
        public PerfilUsuario? Paciente { get; set; }
        public Ingreso? Ingreso { get; set; }
        public CuentaCobrar? Cuenta { get; set; }
        public Consulta? Consulta { get; set; }
        public ICollection<FacturaServicio>? FacturaServicios { get; set; }
        public ICollection<FacturaProducto>? FacturaProductos { get; set; } 

    }
}
