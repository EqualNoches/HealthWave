using HospitalCore_core.Models;

namespace HospitalCore_core.DTO
{
    public class MetodoDePagoDto
    {
        public int CodigoMetodoDePago { get; set; }
        public string Nombre { get; set; } = null!;

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