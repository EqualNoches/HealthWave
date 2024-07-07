namespace HospitalCore_core.Models;

public partial class Servicio
{
    public int ServicioCodigo { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int? IDTipoServicio { get; set; }
    public decimal? Costo { get; set; }
    public int? IDAseguradora { get; set; }
        
    public virtual Aseguradora? Aseguradora { get; set; }
    public virtual TipoServicio? TipoServicio { get; set; }
    public virtual ICollection<PerfilUsuario> CodigoPacientes { get; set; } = new List<PerfilUsuario>();
    public virtual ICollection<Consultum> ConsultaCodigos { get; set; } = new List<Consultum>();
}