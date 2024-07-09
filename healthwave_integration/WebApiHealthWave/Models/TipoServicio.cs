using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class TipoServicio
    {
        public int TipoServicioId { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

    }
}

