using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class ReservaServicio
    {
        public int ServicioCodigo { get; set; }
        public string? CodigoPaciente { get; set; }

        public Servicio? Servicio { get; set; }
        public PerfilUsuario? Paciente { get; set; }
    }
}

