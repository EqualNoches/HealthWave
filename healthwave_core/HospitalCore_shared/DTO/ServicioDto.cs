using HospitalCore_core.Models;

namespace HospitalCore_core.DTO;

public class ServicioDto
{
    public int ServicioCodigo { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public int? IDTipoServicio { get; set; }
    public decimal? Costo { get; set; }
    public int? IDAseguradora { get; set; }
}