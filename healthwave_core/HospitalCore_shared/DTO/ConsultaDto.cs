using HospitalCore_core.Models;

namespace HospitalCore_core.DTO
{
    public class ConsultaDto
    {
        public int ConsultaCodigo { get; set; }

        public string DocumentoPaciente { get; set; }

        public string DocumentoMedico { get; set; }

        public int? IdConsultorio { get; set; }

        public int? IdAutorizacion { get; set; }

        public DateOnly Fecha { get; set; }

        public string Motivo { get; set; }

        public string? Comentarios { get; set; }

        public string Estado { get; set; }

        public decimal? costo { get; set; }

        static public ConsultaDto FromModel(Consultum consultum) => new ConsultaDto
        {
            ConsultaCodigo = consultum.ConsultaCodigo,
            DocumentoPaciente = consultum.CodigoPaciente,
            DocumentoMedico = consultum.CodigoDocumentoMedico,
            IdConsultorio = consultum.Idconsultorio,
            IdAutorizacion = consultum.Idautorizacion,
            Fecha = consultum.Fecha,
            Motivo = consultum.Motivo,
            Comentarios = consultum.Descripcion,
            Estado = consultum.Estado,
            costo = consultum.Costo
        };
    }
}
