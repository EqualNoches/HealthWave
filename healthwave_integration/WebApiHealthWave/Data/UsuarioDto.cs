using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class UsuarioDto
    {
        public string? UsuarioCodigo { get; set; }
        public string? DocumentoUsuario { get; set; }
        public string? UsuarioContra { get; set; }

        public string TipoDocumento { get; set; } = null!;
        public string? NumLicenciaMedica { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? Direccion { get; set; } = null!;
        public string Rol { get; set; } = null!;

        
    }
}

