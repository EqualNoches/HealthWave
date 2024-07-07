namespace HospitalCore_core.DTO
{
    public class AutorizacionDTO
    {
        public int Idautorizacion { get; set; }
        public DateOnly FechaAutorizacion { get; set; }
        public decimal? MontoAutorizado { get; set; }
        public int? Idaseguradora { get; set; }
    }
}