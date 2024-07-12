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
    public class FacturaServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturaServicioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FacturaServicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaServicio>>> GetFacturaServicios()
        {
            return await _context.FacturaServicios.ToListAsync();
        }

        // GET: api/FacturaServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaServicio>> GetFacturaServicio(int id)
        {
            var facturaServicio = await _context.FacturaServicios.FindAsync(id);

            if (facturaServicio == null)
            {
                return NotFound();
            }

            return facturaServicio;
        }

        // PUT: api/FacturaServicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturaServicio(int id, FacturaServicio facturaServicio)
        {
            if (id != facturaServicio.FacturaCodigoServicio)
            {
                return BadRequest();
            }

            _context.Entry(facturaServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaServicioExists(id))
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

        // POST: api/FacturaServicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FacturaServicio>> PostFacturaServicio(FacturaServicio facturaServicio)
        {
            _context.FacturaServicios.Add(facturaServicio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FacturaServicioExists(facturaServicio.FacturaCodigoServicio))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFacturaServicio", new { id = facturaServicio.FacturaCodigoServicio }, facturaServicio);
        }

        // DELETE: api/FacturaServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturaServicio(int id)
        {
            var facturaServicio = await _context.FacturaServicios.FindAsync(id);
            if (facturaServicio == null)
            {
                return NotFound();
            }

            _context.FacturaServicios.Remove(facturaServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaServicioExists(int id)
        {
            return _context.FacturaServicios.Any(e => e.FacturaCodigoServicio == id);
        }
    }
}
