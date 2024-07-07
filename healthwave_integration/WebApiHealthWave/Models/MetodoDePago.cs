namespace WebApiHealthWave.Models
{
    public class MetodoDePago
    {
        public int CodigoMetodoDePago { get; set; }
        public string? Nombre { get; set; }

        public ICollection<Factura>? Facturas { get; set; } 

    }
}
