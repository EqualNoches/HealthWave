using HospitalCore_core.DTO;

namespace HospitalCore_core.Models;

public class Usuario
{
    public string UsuarioCodigo { get; set; } = null!;

    public string DocumentoUsuario { get; set; } = null!;

    public string UsuarioContra { get; set; } = null!;

    public virtual PerfilUsuario PerfilUsuario { get; set; } = null!;

    public static Usuario FromDto(UsuarioDto usuarioDto)
    {
        return new Usuario
        {
            UsuarioCodigo = usuarioDto.UsuarioCodigo,
            DocumentoUsuario = usuarioDto.CodigoDocumento,
            UsuarioContra = usuarioDto.UsuarioContra
        };
    }
}
