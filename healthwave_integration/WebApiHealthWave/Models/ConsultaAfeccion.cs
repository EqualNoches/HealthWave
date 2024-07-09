using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class ConsultaAfeccion
    {
        public int ConsultaCodigo { get; set; }
        public int IDAfeccion { get; set; }

        public Consulta? Consulta { get; set; }
        public Afeccion? Afeccion { get; set; }
    }
}
