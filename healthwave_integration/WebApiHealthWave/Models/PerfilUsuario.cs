using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class PerfilUsuario
    {
        public string? CodigoDocumento { get; set; } = null!;
        public char? TipoDocumento { get; set; } = null!;
        public string? NumLicenciaMedica { get; set; }
        public string? Nombre { get; set; } = null!;
        public string? Apellido { get; set; } = null!;
        public char? Género { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string? Teléfono { get; set; }
        public string? Correo { get; set; }
        public string? Dirección { get; set; }
        public char Rol { get; set; } 
        public ICollection<CuentaCobrar>? CuentasCobrar { get; set; }
        public ICollection<Consulta>? Consultas { get; set; }
        public ICollection<Ingreso>? IngresosPaciente { get; set; } 
        public ICollection<Ingreso>? IngresosMedico { get; set; }
        public ICollection<Factura>? Facturas { get; set; }
        public ICollection<ReservaServicio>? ReservaServicios { get; set; }

        public ICollection<Usuario>? Usuarios { get; set; }

        public virtual Usuario? Usuario { get; set; }


        public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();

        public virtual ICollection<CuentaCobrar> CuentaCobrars { get; set; } = new List<CuentaCobrar>();



        public virtual ICollection<Servicio> ServicioCodigos { get; set; } = new List<Servicio>();

        public static PerfilUsuario FromDto(UsuarioDto usuarioDto)
        {
            return new PerfilUsuario
            {
                NumLicenciaMedica = usuarioDto.NumLicenciaMedica,
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                FechaNacimiento = usuarioDto.FechaNacimiento,
                Teléfono = usuarioDto.Telefono,
                Correo = usuarioDto.Correo,
                Dirección = usuarioDto.Direccion,
                
            };
        }
    }
}

