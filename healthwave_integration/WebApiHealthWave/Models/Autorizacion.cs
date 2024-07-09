using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Autorizacion
    {
        public int IDAutorizacion { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public decimal MontoAutorizado { get; set; }
        public int? IDAseguradora { get; set; }

        public Aseguradora? Aseguradora { get; set; }

        public ICollection<Ingreso>? Ingresos { get; set; }
        public ICollection<FacturaServicio>? FacturaServicios { get; set; } 

        public ICollection<FacturaProducto>? FacturaProductos { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();




    }
}


