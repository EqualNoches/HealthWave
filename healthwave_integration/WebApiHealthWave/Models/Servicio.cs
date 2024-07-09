using WebApiHealthWave.Data;

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

        public Aseguradora? Aseguradora { get; set; }
        public ICollection<ReservaServicio>? ReservaServicios { get; set; }
        public ICollection<ConsultaServicio>? ConsultaServicios { get; set; }
        public virtual ICollection<PerfilUsuario> CodigoPacientes { get; set; } = new List<PerfilUsuario>();

        public TipoServicio? TipoServicioNavigation { get; set; }
        public Aseguradora? AseguradoraNavigation { get; set; }


        public virtual ICollection<Consulta> ConsultaCodigos { get; set; } = new List<Consulta>();


    }
}

