using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Aseguradora
    {
        public int IDAseguradora { get; set; }
        public string? Nombre { get; set; }
        public string? Dirección { get; set; }
        public string? Teléfono { get; set; }
        public string? Correo { get; set; }

        public virtual ICollection<Autorizacion> Autorizacions { get; set; } = new List<Autorizacion>();

        public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();


    }
}

