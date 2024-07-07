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
    public class AseguradoraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AseguradoraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Aseguradora
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aseguradora>>> GetAseguradoras()
        {
            return await _context.Aseguradoras.ToListAsync();
        }

        // GET: api/Aseguradora/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aseguradora>> GetAseguradora(int id)
        {
            var aseguradora = await _context.Aseguradoras.FindAsync(id);

            if (aseguradora == null)
            {
                return NotFound();
            }

            return aseguradora;
        }

        // PUT: api/Aseguradora/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAseguradora(int id, Aseguradora aseguradora)
        {
            if (id != aseguradora.IDAseguradora)
            {
                return BadRequest();
            }

            _context.Entry(aseguradora).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AseguradoraExists(id))
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

        // POST: api/Aseguradora
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aseguradora>> PostAseguradora(Aseguradora aseguradora)
        {
            _context.Aseguradoras.Add(aseguradora);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAseguradora", new { id = aseguradora.IDAseguradora }, aseguradora);
        }

        // DELETE: api/Aseguradora/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAseguradora(int id)
        {
            var aseguradora = await _context.Aseguradoras.FindAsync(id);
            if (aseguradora == null)
            {
                return NotFound();
            }

            _context.Aseguradoras.Remove(aseguradora);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AseguradoraExists(int id)
        {
            return _context.Aseguradoras.Any(e => e.IDAseguradora == id);
        }
    }
}
