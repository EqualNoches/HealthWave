namespace WebApiHealthWave.Data
{
    public class ServicioDto
    {
        public int ServicioCodigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripción { get; set; }
        public int? TipoServicio { get; set; }
        public decimal Costo { get; set; }
        public int? IDAseguradora { get; set; }
    }
}
