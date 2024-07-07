namespace WebApiHealthWave.Models
{
    public class Servicio
    {
        public int ServicioCodigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripción { get; set; }
        public int? TipoServicio { get; set; }
        public decimal Costo { get; set; }
        public int? IDAseguradora { get; set; }

        public TipoServicio? TipoServicioNavigation { get; set; }
        public Aseguradora? Aseguradora { get; set; }
        public ICollection<ReservaServicio>? ReservaServicios { get; set; }
        public ICollection<ConsultaServicio>? ConsultaServicios { get; set; }


    }
}

