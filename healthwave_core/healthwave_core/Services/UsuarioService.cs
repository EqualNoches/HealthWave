using System.Collections.Generic;
using System.Linq;
using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Context;
using HospitalCore_core.Models;
using HospitalCore_core.Utilities;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace HospitalCore_core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HospitalCore _context;

        public UsuarioService(HospitalCore context)
        {
            _context = context;
        }

        public UsuarioDTO GetUsuario(string codigoODocumento)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.usuarioCodigo == codigoODocumento);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                UsuarioCodigo = usuario.usuarioCodigo,
                DocumentoUsuario = usuario.documentoUsuario,
                UsuarioContra = usuario.usuarioContra
            };
        }

        public IEnumerable<UsuarioDTO> GetAllUsuarios()
        {
            return _context.Usuarios.Select(u => new UsuarioDTO
            {
                UsuarioCodigo = u.usuarioCodigo,
                DocumentoUsuario = u.documentoUsuario,
                UsuarioContra = u.usuarioContra
            }).ToList();
        }

        public void AddUsuario(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario
            {
                usuarioCodigo = usuarioDto.UsuarioCodigo,
                documentoUsuario = usuarioDto.DocumentoUsuario,
                usuarioContra = usuarioDto.UsuarioContra
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void UpdateUsuario(UsuarioDTO usuarioDto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.usuarioCodigo == usuarioDto.UsuarioCodigo);
            if (usuario == null) return;

            usuario.documentoUsuario = usuarioDto.DocumentoUsuario;
            usuario.usuarioContra = usuarioDto.UsuarioContra;

            _context.SaveChanges();
        }

        public void DeleteUsuario(string codigoODocumento)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.usuarioCodigo == codigoODocumento);
            if (usuario == null) return;

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        public PerfilUsuarioDTO GetCuenta(string codigoODocumento)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.usuarioCodigo == codigoODocumento);
            if (usuario?.PerfilUsuario == null) return null;

            var perfil = usuario.PerfilUsuario;
            return new PerfilUsuarioDTO
            {
                CodigoDocumento = perfil.CodigoDocumento,
                TipoDocumento = perfil.TipoDocumento,
                NumLicenciaMedica = perfil.NumLicenciaMedica,
                Nombre = perfil.Nombre,
                Apellido = perfil.Apellido,
                Genero = perfil.Genero,
                FechaNacimiento = perfil.FechaNacimiento,
                Telefono = perfil.Telefono,
                Correo = perfil.Correo,
                Direccion = perfil.Direccion,
                Rol = perfil.Rol
            };
        }

        public void ToggleStateCuenta(string codigoODocumento)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.usuarioCodigo == codigoODocumento);
            if (usuario?.PerfilUsuario == null) return;

            var perfil = usuario.PerfilUsuario;
            // Implement logic to toggle state
            // For example, assuming there's an "IsActive" property
            // perfil.IsActive = !perfil.IsActive;

            _context.SaveChanges();
        }
    }
}
