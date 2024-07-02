using HospitalCore_core.DTO;


namespace HospitalCore_core.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<List<UsuarioDto>> GetUsuariosListAsync();
        public Task<int> AddUsuarioAsync(UsuarioDto usuarioDto);
        public Task<int> UpdateUsuarioAsync(UsuarioDto usuarioDto);
        public Task<int> DeleteUsuarioAsync(string usuariocodigoOCodigoDocumento);
        public Task<int> ToggleUsuarioCuentaAsync(string usuariocodigoOCodigoDocumento);
        public Task<UsuarioDto?> GetUsuarioByIdAsync(string usuariocodigoOCodigoDocumento);
        public Task<CuentaCobrarDto?> GetCuentaByUsuarioCodigoOrDocumentoAsync(string usuariocodigoOCodigoDocumento);
    }

    
    

}

