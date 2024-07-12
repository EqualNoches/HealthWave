using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajaComplete
{
    public class Transaccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string Procedimiento { get; set; }
        public float Costo { get; set; }
        public bool Pago { get; set; } = false;
        public string Metodo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaPago { get; set; }
        public string Aseguradora { get; set; } = "";
        public string InsertadoPor { get; set; }
        public float DescuentoAseguradora { get; set; } = 0;

    }
}
