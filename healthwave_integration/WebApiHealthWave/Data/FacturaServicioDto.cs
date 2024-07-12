using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class FacturaServicioDto
    {
        public int FacturaCodigoServicio { get; set; }
        public int IDProducto { get; set; }
        public int? IDAutorizacion { get; set; }
        public decimal Costo { get; set; }
        public string? ServicioCodigo { get; set; }

        public static FacturaServicioDto FromModel(FacturaServicio facturaServicio)
        {
            return new FacturaServicioDto
            {
                FacturaCodigoServicio = facturaServicio.FacturaCodigoServicio,
                IDProducto = facturaServicio.IDProducto,
                IDAutorizacion = facturaServicio.IDAutorizacion,
                Costo = facturaServicio.Costo,
                ServicioCodigo = facturaServicio.ServicioCodigo
            };
        }
    }
}
