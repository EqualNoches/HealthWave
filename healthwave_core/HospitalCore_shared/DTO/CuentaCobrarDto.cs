namespace HospitalCore_core.DTO
{
    public class CuentaCobrarDto
    {
        public int Idcuenta { get; set; }
        public decimal? Balance { get; set; }
        public string Estado { get; set; } = null!;
        public string? CodigoPaciente { get; set; }
    }
}