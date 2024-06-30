using HospitalCore_core.DTO;
using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioDTO GetUsuario(string codigoODocumento);
        IEnumerable<UsuarioDTO> GetAllUsuarios();
        void AddUsuario(UsuarioDTO usuarioDto);
        void UpdateUsuario(UsuarioDTO usuarioDto);
        void DeleteUsuario(string codigoODocumento);
        PerfilUsuarioDTO GetCuenta(string codigoODocumento);
        void ToggleStateCuenta(string codigoODocumento);
    }
}