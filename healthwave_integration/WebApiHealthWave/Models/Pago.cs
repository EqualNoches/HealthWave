using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Pago
    {
        public int IDPago { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime Fecha { get; set; }
        public int IDCuenta { get; set; }

        public CuentaCobrar? Cuenta { get; set; }

        public static Pago? FromDTO(PagoDto dto)
        {
            return new Pago()
            {
                IDPago = dto.IDPago,
                IDCuenta = dto.IDCuenta,
                MontoPagado = dto.MontoPagado,
                Fecha = dto.Fecha,

            };
        }
    }
}
