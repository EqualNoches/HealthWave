using WebApiHealthWave.Data;

namespace WebApiHealthWave.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<List<UsuarioDto>> GetUsuariosListAsync();
        public Task<int> AddUsuarioAsync(UsuarioDto usuarioDto);
        public Task<int> UpdateUsuarioAsync(UsuarioDto usuarioDto);
        public Task<int> DeleteUsuarioAsync(string codigoOdocumento);
        public Task<int> ToggleUsuarioCuentaAsync(string codigoOdocumento);
        public Task<UsuarioDto?> GetUsuarioByIdAsync(string id);
    }
}
