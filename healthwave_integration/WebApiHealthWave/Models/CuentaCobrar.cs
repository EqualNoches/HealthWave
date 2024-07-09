using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class CuentaCobrar
    {
        public int IDCuenta { get; set; }
        public decimal Balance { get; set; }
        public char Estado { get; set; }
        public string? CodigoPaciente { get; set; }
        public PerfilUsuario? PerfilUsuario { get; set; }

        public ICollection<Factura>? Facturas { get; set; }
        public ICollection<Pago>? Pagos { get; set; }


    }
}
