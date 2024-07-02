using HospitalCore_core.Models;

namespace HospitalCore_core.DTO;

public class UsuarioDto
{
    public string UsuarioCodigo { get; set; } = null!;
    public string CodigoDocumento { get; set; } = null!;
    public string UsuarioContra { get; set; } = null!;
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

    static public UsuarioDto FromModel(Usuario usuario)
    {
        return new UsuarioDto
        {
            UsuarioCodigo = usuario.UsuarioCodigo,
            CodigoDocumento = usuario.DocumentoUsuario,
            UsuarioContra = usuario.UsuarioContra,
            TipoDocumento = usuario.PerfilUsuario.TipoDocumento,
            NumLicenciaMedica = usuario.PerfilUsuario.NumLicenciaMedica,
            Nombre = usuario.PerfilUsuario.Nombre,
            Apellido = usuario.PerfilUsuario.Apellido,
            Genero = usuario.PerfilUsuario.Genero,
            FechaNacimiento = usuario.PerfilUsuario.FechaNacimiento,
            Telefono = usuario.PerfilUsuario.Telefono,
            Correo = usuario.PerfilUsuario.Correo,
            Direccion = usuario.PerfilUsuario.Direccion,
            Rol = usuario.PerfilUsuario.Rol
        };
    }
}