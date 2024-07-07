namespace WebApiHealthWave.Data
{
    public class AutorizacionDto
    {
        public int IDAutorizacion { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public decimal MontoAutorizado { get; set; }
        public int? IDAseguradora { get; set; }
    }
}
