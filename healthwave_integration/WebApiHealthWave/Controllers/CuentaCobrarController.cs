using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaCobrarController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/CuentaCobrar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaCobrar>>> GetCuentasCobrar()
        {
            return await _context.CuentasCobrar.ToListAsync();
        }

        // GET: api/CuentaCobrar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaCobrar>> GetCuentaCobrar(int id)
        {
            var cuentaCobrar = await _context.CuentasCobrar.FindAsync(id);

            if (cuentaCobrar == null)
            {
                return NotFound();
            }

            return cuentaCobrar;
        }

        // PUT: api/CuentaCobrar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentaCobrar(int id, CuentaCobrar cuentaCobrar)
        {
            if (id != cuentaCobrar.IDCuenta)
            {
                return BadRequest();
            }

            _context.Entry(cuentaCobrar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaCobrarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CuentaCobrar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CuentaCobrar>> PostCuentaCobrar(CuentaCobrar cuentaCobrar)
        {
            _context.CuentasCobrar.Add(cuentaCobrar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuentaCobrar", new { id = cuentaCobrar.IDCuenta }, cuentaCobrar);
        }

        // DELETE: api/CuentaCobrar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentaCobrar(int id)
        {
            var cuentaCobrar = await _context.CuentasCobrar.FindAsync(id);
            if (cuentaCobrar == null)
            {
                return NotFound();
            }

            _context.CuentasCobrar.Remove(cuentaCobrar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentaCobrarExists(int id)
        {
            return _context.CuentasCobrar.Any(e => e.IDCuenta == id);
        }
    }
}
