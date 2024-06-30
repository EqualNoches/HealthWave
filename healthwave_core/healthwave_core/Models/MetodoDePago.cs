namespace HospitalCore_core.Models
{
    public partial class MetodoDePago
    {
        public int CodigoMetodoDePago { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}