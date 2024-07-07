namespace WebApiHealthWave.Models
{
    public class Afeccion
    {
        public int IDAfeccion { get; set; }
        public string? Nombre { get; set; }
        public string? Descripción { get; set; }

        public ICollection<ConsultaAfeccion>? ConsultaAfecciones { get; set; }
        public ICollection<IngresoAfeccion>? IngresoAfecciones { get; set; } 

    }
}
