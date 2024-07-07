namespace HospitalCore_core.Models
{
    public class Afeccion
    {
        public int IdAfeccion { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<Consultum> ConsultaCodigo { get; set; } = new List<Consultum>();
        public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
    }
}