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
    public class AfeccionController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/Afeccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Afeccion>>> GetAfecciones()
        {
            return await _context.Afecciones.ToListAsync();
        }

        // GET: api/Afeccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Afeccion>> GetAfeccion(int id)
        {
            var afeccion = await _context.Afecciones.FindAsync(id);

            if (afeccion == null)
            {
                return NotFound();
            }

            return afeccion;
        }

        // PUT: api/Afeccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAfeccion(int id, Afeccion afeccion)
        {
            if (id != afeccion.IDAfeccion)
            {
                return BadRequest();
            }

            _context.Entry(afeccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AfeccionExists(id))
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

        // POST: api/Afeccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Afeccion>> PostAfeccion(Afeccion afeccion)
        {
            _context.Afecciones.Add(afeccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAfeccion", new { id = afeccion.IDAfeccion }, afeccion);
        }

        // DELETE: api/Afeccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAfeccion(int id)
        {
            var afeccion = await _context.Afecciones.FindAsync(id);
            if (afeccion == null)
            {
                return NotFound();
            }

            _context.Afecciones.Remove(afeccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AfeccionExists(int id)
        {
            return _context.Afecciones.Any(e => e.IDAfeccion == id);
        }
    }
}
