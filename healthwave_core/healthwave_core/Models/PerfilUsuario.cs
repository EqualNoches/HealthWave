

namespace HospitalCore_core.Models
{
    public partial class PerfilUsuario
    {
        public string CodigoDocumento { get; set; } = null!;
        public string TipoDocumento { get; set; } = null!;
        public string? NumLicenciaMedica { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string Rol { get; set; } = null!;
    

        public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
        public virtual ICollection<CuentaCobrar> CuentaCobrars { get; set; } = new List<CuentaCobrar>();
        public virtual Usuario? Usuarios { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
        public virtual ICollection<Ingreso> IngresoCodigoDocumentoMedicoNavigations { get; set; } = new List<Ingreso>();
        public virtual ICollection<Ingreso> IngresoCodigoPacienteNavigations { get; set; } = new List<Ingreso>();
        public virtual ICollection<Servicio> ServicioCodigos { get; set; } = new List<Servicio>();
    }
}

