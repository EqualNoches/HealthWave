namespace WebApiHealthWave.Models
{
    public class ConsultaServicio
    {
        public int ConsultaCodigo { get; set; }
        public int ServicioCodigo { get; set; }

        public Consulta? Consulta { get; set; }
        public Servicio? Servicio { get; set; }
    }
}


