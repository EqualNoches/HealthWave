using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class MetodoDePago
    {
        public int CodigoMetodoDePago { get; set; }
        public string? Nombre { get; set; }

        public ICollection<Factura>? Facturas { get; set; }

        public static MetodoDePago FromDto(MetodoDePagoDto metodoDePagoDto)
        {
            return new MetodoDePago
            {
                CodigoMetodoDePago = metodoDePagoDto.CodigoMetodoDePago,
                Nombre = metodoDePagoDto.Nombre,
            };
        }

    }
}
