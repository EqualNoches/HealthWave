namespace WebApiHealthWave.Models
{
    public class PerfilUsuario
    {
        public string? CodigoDocumento { get; set; }
        public char? TipoDocumento { get; set; }
        public string? NumLicenciaMedica { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public char? Género { get; set; }
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

        public ICollection<Usuario>? Usuarios { get; set; }  // Agregar esta línea


    }
}

