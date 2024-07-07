namespace WebApiHealthWave.Data
{
    public class IngresoDto
    {
        public int IDIngreso { get; set; }
        public decimal CostoEstancia { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int? NumSala { get; set; }
        public string? CodigoPaciente { get; set; }
        public string? CodigoDocumentoMedico { get; set; }
        public int? ConsultaCodigo { get; set; }
        public int? IDAutorizacion { get; set; }
    }
}
