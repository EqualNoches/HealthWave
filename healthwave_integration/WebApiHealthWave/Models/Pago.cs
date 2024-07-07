namespace WebApiHealthWave.Models
{
    public class Pago
    {
        public int IDPago { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime Fecha { get; set; }
        public int IDCuenta { get; set; }

        public CuentaCobrar? Cuenta { get; set; }
    }
}
