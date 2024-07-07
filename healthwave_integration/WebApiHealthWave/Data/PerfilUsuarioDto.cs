namespace WebApiHealthWave.Data
{
    public class PerfilUsuarioDto
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
    }
}

