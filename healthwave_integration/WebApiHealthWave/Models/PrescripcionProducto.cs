namespace WebApiHealthWave.Models
{
    public class PrescripcionProducto
    {
        public int IDProducto { get; set; }
        public int ConsultaCodigo { get; set; }
        public int Cantidad { get; set; }

        public Producto? Producto { get; set; }
        public Consulta? Consulta { get; set; }
    }
}
