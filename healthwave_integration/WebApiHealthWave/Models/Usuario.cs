namespace WebApiHealthWave.Models
{
    public class Usuario
    {
        public string? UsuarioCodigo { get; set; }
        public string? DocumentoUsuario { get; set; }
        public string? UsuarioContra { get; set; }

        // Navigation property
        public PerfilUsuario? PerfilUsuario { get; set; }
    }
}
