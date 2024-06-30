using HospitalCore_core.Models;

namespace HospitalCore_core.DTO
{
    public class IngresoDto
    {
        public int IDIngreso { get; set; }
        public decimal? CostoEstancia { get; set; }
        public DateOnly FechaIngreso { get; set; }
        public DateOnly? FechaAlta { get; set; }
        public int? NumSala { get; set; }
        public string? CodigoPaciente { get; set; }
        public string? CodigoDocumentoMedico { get; set; }
        public int? ConsultaCodigo { get; set; }
        public int? Idautorizacion { get; set; }
        
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
                Idautorizacion = model.Idautorizacion
            };
        }
    }
}