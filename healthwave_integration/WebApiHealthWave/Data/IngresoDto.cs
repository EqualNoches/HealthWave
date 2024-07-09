using WebApiHealthWave.Models;

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

        public static IngresoDto FromModel(Ingreso model)
        {
            return new IngresoDto
            {
                IDIngreso = model.IDIngreso,
                CostoEstancia = model.CostoEstancia,
                FechaIngreso = model.FechaIngreso,
                FechaAlta = model.FechaAlta,
                NumSala = model.NumSala,
                CodigoPaciente = model.CodigoPaciente,
                CodigoDocumentoMedico = model.CodigoDocumentoMedico,
                ConsultaCodigo = model.ConsultaCodigo,
                IDAutorizacion = model.IDAutorizacion,
            };
        }
    }
}
