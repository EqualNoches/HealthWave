using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class ConsultaDto
    {
        public int ConsultaCodigo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Estado { get; set; }
        public decimal? Costo { get; set; }
        public string? Motivo { get; set; }
        public string? Descripcion { get; set; }
        public string? CodigoPaciente { get; set; }
        public int? IDConsultorio { get; set; }
        public int? IDAutorizacion { get; set; }
        public string? CodigoDocumentoMedico { get; set; }

        static public ConsultaDto FromModel(Consulta consulta) => new ConsultaDto
        {
            ConsultaCodigo = consulta.ConsultaCodigo,
            IDConsultorio = consulta.IDConsultorio,
            IDAutorizacion = consulta.IDAutorizacion,
            Descripcion = consulta.Descripcion,
            CodigoPaciente = consulta.CodigoPaciente,
            CodigoDocumentoMedico = consulta.CodigoDocumentoMedico,
            Fecha = consulta.Fecha,
            Motivo = consulta.Motivo,
            Estado = consulta.Estado,
            Costo = consulta.Costo
        };

    }
}
