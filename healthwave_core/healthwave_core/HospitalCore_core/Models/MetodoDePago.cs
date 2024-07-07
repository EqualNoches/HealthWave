using HospitalCore_core.DTO;

namespace HospitalCore_core.Models
{
    public partial class MetodoDePago
    {
        public int CodigoMetodoDePago { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

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