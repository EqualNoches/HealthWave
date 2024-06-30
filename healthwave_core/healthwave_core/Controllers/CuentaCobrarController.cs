using HospitalCore_core.Models;
using Microsoft.AspNetCore.Mvc;
using HospitalCore_core.DTO;
using HospitalCore_core.Services.Interfaces;

namespace HospitalCore_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaCobrarController : ControllerBase
    {
        private readonly ICuentaCobrarService _cuentaCobrarService;

        public CuentaCobrarController(ICuentaCobrarService cuentaCobrarService)
        {
            _cuentaCobrarService = cuentaCobrarService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaCobrarDto>>> GetCuentasCobrar()
        {
            var cuentas = await _cuentaCobrarService.GetCuentasCobrar();
            return Ok(cuentas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaCobrarDto>> GetCuentaCobrar(int id)
        {
            var cuenta = await _cuentaCobrarService.GetCuentaCobrarById(id);

            if (cuenta == null)
                return NotFound();

            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<ActionResult<CuentaCobrarDto>> CreateCuentaCobrar(CuentaCobrarDto cuentaCobrarDto)
        {
            var createdCuenta = await _cuentaCobrarService.CreateCuentaCobrar(cuentaCobrarDto);
            return CreatedAtAction(nameof(GetCuentaCobrar), new { id = createdCuenta.Idcuenta }, createdCuenta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CuentaCobrarDto>> UpdateCuentaCobrar(int id, CuentaCobrarDto cuentaCobrarDto)
        {
            var updatedCuenta = await _cuentaCobrarService.UpdateCuentaCobrar(id, cuentaCobrarDto);

            if (updatedCuenta == null)
                return NotFound();

            return Ok(updatedCuenta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCuentaCobrar(int id)
        {
            var deleted = await _cuentaCobrarService.DeleteCuentaCobrar(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
