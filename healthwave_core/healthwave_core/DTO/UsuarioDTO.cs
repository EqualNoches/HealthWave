using HospitalCore_core.Models;

namespace HospitalCore_core.DTO
{
    public class UsuarioDTO
    {
        public string UsuarioCodigo { get; set; }
        public string DocumentoUsuario { get; set; }
        public string UsuarioContra { get; set; }
    }

    public class PerfilUsuarioDTO
    {
        public string CodigoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string? NumLicenciaMedica { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string Rol { get; set; }
    }
}