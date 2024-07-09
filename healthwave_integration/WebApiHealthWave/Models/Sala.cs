using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Sala
    {
        public int NumSala { get; set; }
        public string? Estado { get; set; }

        public ICollection<Ingreso>? Ingresos { get; set; }

    }
}

