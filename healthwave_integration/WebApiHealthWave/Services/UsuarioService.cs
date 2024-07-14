using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<UsuarioService> _logHandler = new();

        public UsuarioService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UsuarioDto>> GetUsuariosListAsync()
        {
            try
            {
                var usuarios = await _dbContext.Usuarios
                    .Include(u => u.PerfilUsuario)
                    .Select(u => new UsuarioDto
                    {
                        UsuarioCodigo = u.UsuarioCodigo,
                        DocumentoUsuario = u.DocumentoUsuario,
                        UsuarioContra = u.UsuarioContra,
                        TipoDocumento = u.PerfilUsuario.TipoDocumento.ToString(),
                        NumLicenciaMedica = u.PerfilUsuario.NumLicenciaMedica,
                        Nombre = u.PerfilUsuario.Nombre,
                        Apellido = u.PerfilUsuario.Apellido,
                        Genero = u.PerfilUsuario.Género.ToString(),
                        FechaNacimiento = u.PerfilUsuario.FechaNacimiento,
                        Telefono = u.PerfilUsuario.Teléfono,
                        Correo = u.PerfilUsuario.Correo,
                        Direccion = u.PerfilUsuario.Dirección,
                        Rol = u.PerfilUsuario.Rol.ToString()
                    })
                    .ToListAsync();

                _logHandler.LogInfo("Usuarios retrieved successfully.");
                return usuarios;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve usuarios.", ex);
                throw;
            }
        }

        public async Task<int> AddUsuarioAsync(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = Usuario.FromDto(usuarioDto);

                var perfilUsuario = new PerfilUsuario
                {
                    CodigoDocumento = usuarioDto.DocumentoUsuario,
                    TipoDocumento = char.Parse(usuarioDto.TipoDocumento),
                    NumLicenciaMedica = usuarioDto.NumLicenciaMedica,
                    Nombre = usuarioDto.Nombre,
                    Apellido = usuarioDto.Apellido,
                    Género = char.Parse(usuarioDto.Genero),
                    FechaNacimiento = usuarioDto.FechaNacimiento,
                    Teléfono = usuarioDto.Telefono,
                    Correo = usuarioDto.Correo,
                    Dirección = usuarioDto.Direccion,
                    Rol = char.Parse(usuarioDto.Rol)
                };

                usuario.PerfilUsuario = perfilUsuario;

                _dbContext.Usuarios.Add(usuario);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Usuario added successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to add usuario.", ex);
                throw;
            }
        }

        public async Task<int> UpdateUsuarioAsync(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = await _dbContext.Usuarios
                    .Include(u => u.PerfilUsuario)
                    .FirstOrDefaultAsync(u => u.UsuarioCodigo == usuarioDto.UsuarioCodigo || u.DocumentoUsuario == usuarioDto.DocumentoUsuario);

                if (usuario == null)
                {
                    _logHandler.LogInfo("Usuario not found.");
                    return 0;
                }

                usuario.UsuarioContra = usuarioDto.UsuarioContra;
                usuario.PerfilUsuario.TipoDocumento = char.Parse(usuarioDto.TipoDocumento);
                usuario.PerfilUsuario.NumLicenciaMedica = usuarioDto.NumLicenciaMedica;
                usuario.PerfilUsuario.Nombre = usuarioDto.Nombre;
                usuario.PerfilUsuario.Apellido = usuarioDto.Apellido;
                usuario.PerfilUsuario.Género = char.Parse(usuarioDto.Genero);
                usuario.PerfilUsuario.FechaNacimiento = usuarioDto.FechaNacimiento;
                usuario.PerfilUsuario.Teléfono = usuarioDto.Telefono;
                usuario.PerfilUsuario.Correo = usuarioDto.Correo;
                usuario.PerfilUsuario.Dirección = usuarioDto.Direccion;
                usuario.PerfilUsuario.Rol = char.Parse(usuarioDto.Rol);

                _dbContext.Usuarios.Update(usuario);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Usuario updated successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to update usuario.", ex);
                throw;
            }
        }

        public async Task<int> DeleteUsuarioAsync(string codigoOdocumento)
        {
            try
            {
                var usuario = await _dbContext.Usuarios
                    .FirstOrDefaultAsync(u => u.UsuarioCodigo == codigoOdocumento || u.DocumentoUsuario == codigoOdocumento);

                if (usuario == null)
                {
                    _logHandler.LogInfo("Usuario not found.");
                    return 0;
                }

                _dbContext.Usuarios.Remove(usuario);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Usuario deleted successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to delete usuario.", ex);
                throw;
            }
        }

        public async Task<int> ToggleUsuarioCuentaAsync(string codigoOdocumento)
        {
            try
            {
                var usuario = await _dbContext.Usuarios
                    .Include(u => u.PerfilUsuario)
                    .FirstOrDefaultAsync(u => u.UsuarioCodigo == codigoOdocumento || u.DocumentoUsuario == codigoOdocumento);

                if (usuario == null)
                {
                    _logHandler.LogInfo("Usuario not found.");
                    return 0;
                }

                // Aquí puedes implementar la lógica para activar/desactivar la cuenta de usuario

                _dbContext.Usuarios.Update(usuario);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Usuario account status toggled successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to toggle usuario account status.", ex);
                throw;
            }
        }

        public async Task<UsuarioDto?> GetUsuarioByIdAsync(string id)
        {
            try
            {
                var usuario = await _dbContext.Usuarios
                    .Include(u => u.PerfilUsuario)
                    .FirstOrDefaultAsync(u => u.UsuarioCodigo == id || u.DocumentoUsuario == id);

                if (usuario == null)
                {
                    _logHandler.LogInfo("Usuario not found.");
                    return null;
                }

                var usuarioDto = new UsuarioDto
                {
                    UsuarioCodigo = usuario.UsuarioCodigo,
                    DocumentoUsuario = usuario.DocumentoUsuario,
                    UsuarioContra = usuario.UsuarioContra,
                    TipoDocumento = usuario.PerfilUsuario.TipoDocumento.ToString(),
                    NumLicenciaMedica = usuario.PerfilUsuario.NumLicenciaMedica,
                    Nombre = usuario.PerfilUsuario.Nombre,
                    Apellido = usuario.PerfilUsuario.Apellido,
                    Genero = usuario.PerfilUsuario.Género.ToString(),
                    FechaNacimiento = usuario.PerfilUsuario.FechaNacimiento,
                    Telefono = usuario.PerfilUsuario.Teléfono,
                    Correo = usuario.PerfilUsuario.Correo,
                    Direccion = usuario.PerfilUsuario.Dirección,
                    Rol = usuario.PerfilUsuario.Rol.ToString()
                };

                _logHandler.LogInfo("Usuario retrieved successfully.");
                return usuarioDto;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Failed to retrieve usuario by ID.", ex);
                throw;
            }
        }
    }
}
