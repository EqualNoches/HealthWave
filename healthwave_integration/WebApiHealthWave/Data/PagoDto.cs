using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class PagoDto
    {
        public int IDPago { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime Fecha { get; set; }
        public int IDCuenta { get; set; }


        public static PagoDto FromModel(Pago pago)
        {
            return new PagoDto()
            {
                IDPago = pago.IDPago,
                IDCuenta = pago.IDCuenta,
                MontoPagado = pago.MontoPagado,
                Fecha = pago.Fecha

            };
        }
    }

}
