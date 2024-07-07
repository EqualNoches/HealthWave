using HospitalCore_core.Context;
using HospitalCore_core.Models;
using Microsoft.EntityFrameworkCore;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.DTO;
using HospitalCore_core.Utilities;



namespace HospitalCore_core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HospitalCore _dbContext;
        private readonly LogManager<UsuarioService> _logManager = new();

        public UsuarioService(HospitalCore dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UsuarioDto?> GetUsuarioByIdAsync(string usuariocodigoOCodigoDocumento)
        {
            try
            {
                var usuario = await _dbContext.Usuarios
                    .Where(e => e.UsuarioCodigo == usuariocodigoOCodigoDocumento || e.DocumentoUsuario == usuariocodigoOCodigoDocumento)
                    .Include(e => e.PerfilUsuario)
                    .FirstOrDefaultAsync();

                if (usuario == null) return null;

                _logManager.LogInfo($"Usuario {usuariocodigoOCodigoDocumento} obtenido exitosamente.");
                return UsuarioDto.FromModel(usuario);
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al obtener usuario {usuariocodigoOCodigoDocumento}.", ex);
                throw new Exception("Error retrieving user by ID.", ex);
            }
        }

        public async Task<List<UsuarioDto>> GetUsuariosListAsync()
        {
            try
            {
                var usuarios = (await _dbContext.Usuarios
                    .Include(e => e.PerfilUsuario)
                    .ToListAsync())
                    .Select(e => UsuarioDto.FromModel(e))
                    .ToList();

                _logManager.LogInfo("Lista de usuarios obtenida exitosamente.");
                return usuarios;
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al listar usuarios.", ex);
                throw new Exception("Error retrieving users list.", ex);
            }
        }

        public async Task<int> AddUsuarioAsync(UsuarioDto usuarioDto)
        {
            try
            {
                var perfilUsuario = PerfilUsuario.FromDto(usuarioDto);
                var usuario = Usuario.FromDto(usuarioDto);

                _dbContext.PerfilUsuarios.Add(perfilUsuario);
                _dbContext.Usuarios.Add(usuario);

                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Usuario {usuario.UsuarioCodigo} agregado exitosamente.");

                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al agregar usuario {usuarioDto.UsuarioCodigo}.", ex);
                throw new Exception("Error adding user.", ex);
            }
        }

        public async Task<int> UpdateUsuarioAsync(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = Usuario.FromDto(usuarioDto);
                var perfilUsuario = PerfilUsuario.FromDto(usuarioDto);

                _dbContext.PerfilUsuarios.Update(perfilUsuario);
                usuario.PerfilUsuario = perfilUsuario;
                _dbContext.Usuarios.Update(usuario);

                await _dbContext.SaveChangesAsync();
                _logManager.LogInfo($"Usuario {usuario.UsuarioCodigo} actualizado exitosamente.");

                return 1;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al actualizar usuario {usuarioDto.UsuarioCodigo}.", ex);
                throw new Exception("Error updating user.", ex);
            }
        }

        public async Task<int> ToggleUsuarioCuentaAsync(string usuariocodigoOCodigoDocumento)
        {
            try
            {
                bool isUsuarioCodigo = await _dbContext.Usuarios.AnyAsync(u => u.UsuarioCodigo == usuariocodigoOCodigoDocumento);

                CuentaCobrar? cuentaCobrarToUpdate;

                if (isUsuarioCodigo)
                {
                    var codigoDocumento = await _dbContext.Usuarios
                        .Where(u => u.UsuarioCodigo == usuariocodigoOCodigoDocumento)
                        .Select(u => u.DocumentoUsuario)
                        .FirstOrDefaultAsync();

                    cuentaCobrarToUpdate = await _dbContext.CuentaCobrars
                        .Include(c => c.CodigoPacienteNavigation)
                        .FirstOrDefaultAsync(c => c.CodigoPacienteNavigation.CodigoDocumento == codigoDocumento);
                }
                else
                {
                    cuentaCobrarToUpdate = await _dbContext.CuentaCobrars
                        .Include(c => c.CodigoPacienteNavigation)
                        .FirstOrDefaultAsync(c => c.CodigoPacienteNavigation.CodigoDocumento == usuariocodigoOCodigoDocumento);
                }

                if (cuentaCobrarToUpdate != null)
                {
                    cuentaCobrarToUpdate.Estado = cuentaCobrarToUpdate.Estado == "A" ? "D" : "A";
                    _dbContext.CuentaCobrars.Update(cuentaCobrarToUpdate);
                    await _dbContext.SaveChangesAsync();

                    _logManager.LogInfo($"Estado de cuenta del usuario {usuariocodigoOCodigoDocumento} cambiado exitosamente.");
                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al cambiar estado de cuenta del usuario {usuariocodigoOCodigoDocumento}.", ex);
                throw new Exception("Error toggling user account.", ex);
            }
        }

        public async Task<int> DeleteUsuarioAsync(string usuariocodigoOCodigoDocumento)
        {
            try
            {
                var user = await _dbContext.Usuarios
                    .Include(u => u.PerfilUsuario)
                    .FirstOrDefaultAsync(u => u.UsuarioCodigo == usuariocodigoOCodigoDocumento || u.DocumentoUsuario == usuariocodigoOCodigoDocumento);

                if (user != null)
                {
                    _dbContext.Usuarios.Remove(user);
                    int result = await _dbContext.SaveChangesAsync();
                    _logManager.LogInfo($"Usuario {usuariocodigoOCodigoDocumento} eliminado exitosamente.");
                    return result;
                }

                return 0;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al eliminar usuario {usuariocodigoOCodigoDocumento}.", ex);
                throw new Exception("Error deleting user.", ex);
            }
        }

        public async Task<CuentaCobrarDto?> GetCuentaByUsuarioCodigoOrDocumentoAsync(string usuariocodigoOCodigoDocumento)
        {
            try
            {
                bool isUsuarioCodigo = await _dbContext.Usuarios.AnyAsync(u => u.UsuarioCodigo == usuariocodigoOCodigoDocumento);

                CuentaCobrar? cuenta;

                if (isUsuarioCodigo)
                {
                    var codigoDocumento = await _dbContext.Usuarios
                        .Where(u => u.UsuarioCodigo == usuariocodigoOCodigoDocumento)
                        .Select(u => u.DocumentoUsuario)
                        .FirstOrDefaultAsync();

                    cuenta = await _dbContext.CuentaCobrars
                        .Include(c => c.CodigoPacienteNavigation)
                        .FirstOrDefaultAsync(c => c.CodigoPacienteNavigation.CodigoDocumento == codigoDocumento);
                }
                else
                {
                    cuenta = await _dbContext.CuentaCobrars
                        .Include(c => c.CodigoPacienteNavigation)
                        .FirstOrDefaultAsync(c => c.CodigoPacienteNavigation.CodigoDocumento == usuariocodigoOCodigoDocumento);
                }

                if (cuenta != null)
                {
                    _logManager.LogInfo($"Cuenta obtenida para el usuario {usuariocodigoOCodigoDocumento} exitosamente.");
                    return CuentaCobrarDto.FromModel(cuenta);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error al obtener cuenta para el usuario {usuariocodigoOCodigoDocumento}.", ex);
                throw new Exception("Error retrieving account by user code or document.", ex);
            }
        }
    }
}