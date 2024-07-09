using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class MetodoDePagoDto
    {
        public int CodigoMetodoDePago { get; set; }
        public string? Nombre { get; set; }

        public static MetodoDePagoDto FromModel(MetodoDePago metodoDePago)
        {
            return new MetodoDePagoDto
            {
                CodigoMetodoDePago = metodoDePago.CodigoMetodoDePago,
                Nombre = metodoDePago.Nombre
            };
        }
    }
}
