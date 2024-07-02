using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{usuariocodigoOCodigoDocumento}")]
        public async Task<IActionResult> GetUsuarioById(string usuariocodigoOCodigoDocumento)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(usuariocodigoOCodigoDocumento);

                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                // Handle exception (log it, return error response, etc.)
                return StatusCode(500, $"Error retrieving user: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuariosList()
        {
            var usuarios = await _usuarioService.GetUsuariosListAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddUsuario(UsuarioDto usuarioDto)
        {
            var result = await _usuarioService.AddUsuarioAsync(usuarioDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateUsuario(UsuarioDto usuarioDto)
        {
            var result = await _usuarioService.UpdateUsuarioAsync(usuarioDto);
            return Ok(result);
        }

        [HttpDelete("{codigoOCodigoDocumento}")]
        public async Task<ActionResult<int>> DeleteUsuario(string codigoOCodigoDocumento)
        {
            var result = await _usuarioService.DeleteUsuarioAsync(codigoOCodigoDocumento);
            if (result == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("toggle-cuenta/{usuariocodigoOCodigoDocumento}")]
        public async Task<IActionResult> ToggleCuenta(string usuariocodigoOCodigoDocumento)
        {
            try
            {
                var result = await _usuarioService.ToggleUsuarioCuentaAsync(usuariocodigoOCodigoDocumento);

                if (result == 0)
                {
                    return NotFound("Cuenta no encontrada.");
                }

                return Ok("Estado de la cuenta actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                // Handle exception (log it, return error response, etc.)
                return StatusCode(500, $"Error toggling account: {ex.Message}");
            }
        }

        [HttpGet("GetCuentaByUsuarioCodigoOrDocumento/{codigoOCodigoDocumento}")]
        public async Task<ActionResult<CuentaCobrarDto>> GetCuentaByUsuarioCodigoOrDocumento(string codigoOCodigoDocumento)
        {
            var cuenta = await _usuarioService.GetCuentaByUsuarioCodigoOrDocumentoAsync(codigoOCodigoDocumento);
            if (cuenta == null)
            {
                return NotFound();
            }

            return Ok(cuenta);
        }
    }
}
