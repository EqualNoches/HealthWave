using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class AfeccionDto
    {
        public int IDAfeccion { get; set; }
        public string? Nombre { get; set; }
        public string? Descripción { get; set; }

        public static AfeccionDto FromModel(Afeccion afeccion)
        {
            return new AfeccionDto
            {
                IDAfeccion = afeccion.IDAfeccion,
                Nombre = afeccion.Nombre
            };
        }

    }
}
