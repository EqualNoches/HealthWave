using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController(IConsultaService consultaService) : ControllerBase
    {
        private readonly LogManager<ConsultaController> _logManager = new();

        [HttpPost("add")]
        public async Task<ActionResult<int>> CrearConsulta([FromBody ] ConsultaDto consulta)
        {
            try
            {
                var result = await consultaService.AddConsultaAsync(consulta);
                return Ok(result);

            }               
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al crear consulta", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear consulta");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<int>> ActualizarConsulta([FromBody] ConsultaDto consultaDto)
        {
            try
            {
                var result = await consultaService.UpdateConsultaAsync(consultaDto);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al actualizar consulta", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar consulta");
            }
        }

        [HttpDelete("delete/{consultaCodigo}")]
        public async Task<ActionResult<int>> EliminarConsulta(int consultaCodigo)
        {
            try
            {
                var result = await consultaService.RemoveConsultaAsync(consultaCodigo);
                return Ok(result);
            }
            catch (Exception ex)
            {

                _logManager.LogFatal("Error al eliminar consulta", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar consulta");
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<ConsultaDto>?>> ListarConsultas()
        {
            try
            {
                var result = await consultaService.GetConsultasAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al listar consultas", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al listar consultas");
            }
        }

        [HttpPost("addServicio/{consultaCodigo}/{servicioCodigo}")]
        public async Task<ActionResult<int>> RelacionarServicio(int consultaCodigo, int servicioCodigo)
        {
            try
            {
                var result = await consultaService.AddConsultaServicioAsync(consultaCodigo, servicioCodigo);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al relacionar servicio", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al relacionar servicio");
            }
        }

        [HttpDelete("deleteServicio/{consultaCodigo}/{servicioCodigo}")]
        public async Task<ActionResult<int>> DesrelacionarServicio(int consultaCodigo, int servicioCodigo)
        {
            try
            {
                var result = await consultaService.RemoveConsultaServicioAsync(consultaCodigo, servicioCodigo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al desrelacionar servicio", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al desrelacionar servicio");
            }
        }

        [HttpGet("getServicios/{consultaCodigo}/")]
        public async Task<ActionResult<List<ServicioDto>>> ListarServicios(int consultaCodigo)
        {
            try
            {
                var result = await consultaService.GetConsultaServiciosAsync(consultaCodigo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al listar servicios", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al listar servicios");
            }
        }
        [HttpPost("addProducto/{consultaCodigo}/{idProducto}/{cantidad}")]
        public async Task<ActionResult<int>> RelacionarProducto(int consultaCodigo, int idProducto, int cantidad)
        {
            try
            {
                var result = await consultaService.AddConsultaProductoAsync(consultaCodigo, idProducto, cantidad);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al relacionar producto", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al relacionar producto");
            }
        }

        [HttpDelete("deleteProducto/{consultaCodigo}/{idProducto}")]
        public async Task<ActionResult<int>> DesrelacionarProducto(int consultaCodigo, int idProducto)
        {
            try
            {
                var result = await consultaService.RemoveConsultaProductoAsync(consultaCodigo, idProducto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logManager.LogError("Error al desrelacionar producto", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al desrelacionar producto");
            }
        }

        [HttpGet("getProductos/{consultaCodigo}")]
        public async Task<ActionResult<List<ProductoDto>?>> ListarProductos(int consultaCodigo)
        {
            try
            {
                var result = await consultaService.GetConsultaProductosAsync(consultaCodigo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logManager.LogFatal("Error al listar productos", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al listar productos");
            }
        }
    }
}
