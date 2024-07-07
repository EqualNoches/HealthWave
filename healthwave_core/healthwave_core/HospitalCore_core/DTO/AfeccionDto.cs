
using HospitalCore_core.Models;

namespace HospitalCore_core.DTO
{
    public class AfeccionDto
    {
        public int IdAfeccion { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }


        public static AfeccionDto FromModel(Afeccion afeccion)
        {
            return new AfeccionDto
            {
                IdAfeccion = afeccion.IdAfeccion,
                Nombre = afeccion.Nombre
            };
        }
    }
}