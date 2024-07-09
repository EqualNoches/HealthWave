using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Usuario
    {
        public string? UsuarioCodigo { get; set; } = null!;
        public string? DocumentoUsuario { get; set; } = null!;
        public string? UsuarioContra { get; set; } = null!; 

        public virtual PerfilUsuario PerfilUsuario { get; set; } = null!;

        public static Usuario FromDto(UsuarioDto usuarioDto)
        {
            return new Usuario
            {
                UsuarioCodigo = usuarioDto.UsuarioCodigo,
                UsuarioContra = usuarioDto.UsuarioContra
            };
        }
    }
}
