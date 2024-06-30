using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("get/{codigoODocumento}")]
        public IActionResult GetUsuario(string codigoODocumento)
        {
            var usuario = _usuarioService.GetUsuario(codigoODocumento);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpGet("list")]
        public IActionResult GetAllUsuarios()
        {
            var usuarios = _usuarioService.GetAllUsuarios();
            return Ok(usuarios);
        }

        [HttpPost("add")]
        public IActionResult AddUsuario([FromBody] UsuarioDTO usuarioDto)
        {
            _usuarioService.AddUsuario(usuarioDto);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult UpdateUsuario([FromBody] UsuarioDTO usuarioDto)
        {
            _usuarioService.UpdateUsuario(usuarioDto);
            return Ok();
        }

        [HttpDelete("delete/{codigoODocumento}")]
        public IActionResult DeleteUsuario(string codigoODocumento)
        {
            _usuarioService.DeleteUsuario(codigoODocumento);
            return Ok();
        }

        [HttpGet("cuenta/{codigoODocumento}")]
        public IActionResult GetCuenta(string codigoODocumento)
        {
            var cuenta = _usuarioService.GetCuenta(codigoODocumento);
            if (cuenta == null) return NotFound();

            return Ok(cuenta);
        }

        [HttpPut("toggle-state-cuenta/{codigoODocumento}")]
        public IActionResult ToggleStateCuenta(string codigoODocumento)
        {
            _usuarioService.ToggleStateCuenta(codigoODocumento);
            return Ok();
        }
    }
}
