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
    public class IngresoAfeccionController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/IngresoAfeccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngresoAfeccion>>> GetIngresoAfecciones()
        {
            return await _context.IngresoAfecciones.ToListAsync();
        }

        // GET: api/IngresoAfeccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngresoAfeccion>> GetIngresoAfeccion(int id)
        {
            var ingresoAfeccion = await _context.IngresoAfecciones.FindAsync(id);

            if (ingresoAfeccion == null)
            {
                return NotFound();
            }

            return ingresoAfeccion;
        }

        // PUT: api/IngresoAfeccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngresoAfeccion(int id, IngresoAfeccion ingresoAfeccion)
        {
            if (id != ingresoAfeccion.IDAfeccion)
            {
                return BadRequest();
            }

            _context.Entry(ingresoAfeccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngresoAfeccionExists(id))
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

        // POST: api/IngresoAfeccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngresoAfeccion>> PostIngresoAfeccion(IngresoAfeccion ingresoAfeccion)
        {
            _context.IngresoAfecciones.Add(ingresoAfeccion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IngresoAfeccionExists(ingresoAfeccion.IDAfeccion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIngresoAfeccion", new { id = ingresoAfeccion.IDAfeccion }, ingresoAfeccion);
        }

        // DELETE: api/IngresoAfeccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngresoAfeccion(int id)
        {
            var ingresoAfeccion = await _context.IngresoAfecciones.FindAsync(id);
            if (ingresoAfeccion == null)
            {
                return NotFound();
            }

            _context.IngresoAfecciones.Remove(ingresoAfeccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngresoAfeccionExists(int id)
        {
            return _context.IngresoAfecciones.Any(e => e.IDAfeccion == id);
        }
    }
}
