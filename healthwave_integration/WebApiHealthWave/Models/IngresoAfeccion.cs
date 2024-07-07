namespace WebApiHealthWave.Models
{
    public class IngresoAfeccion
    {
        public int IDAfeccion { get; set; }
        public int IDIngreso { get; set; }

        public Afeccion? Afeccion { get; set; }
        public Ingreso? Ingreso { get; set; }
    }
}

