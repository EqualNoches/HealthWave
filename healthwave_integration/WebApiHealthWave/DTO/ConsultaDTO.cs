public class ConsultaDto
{
    public int ConsultaCodigo { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; }
    public decimal? Costo { get; set; }
    public string Motivo { get; set; }
    public string Descripcion { get; set; }
    public string CodigoPaciente { get; set; }
    public int? IDConsultorio { get; set; }
    public int? IDAutorizacion { get; set; }
    public string CodigoDocumentoMedico { get; set; }
}
