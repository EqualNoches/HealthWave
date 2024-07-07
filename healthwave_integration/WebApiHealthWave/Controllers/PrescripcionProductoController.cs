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
    public class PrescripcionProductoController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/PrescripcionProducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescripcionProducto>>> GetPrescripcionProductos()
        {
            return await _context.PrescripcionProductos.ToListAsync();
        }

        // GET: api/PrescripcionProducto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescripcionProducto>> GetPrescripcionProducto(int id)
        {
            var prescripcionProducto = await _context.PrescripcionProductos.FindAsync(id);

            if (prescripcionProducto == null)
            {
                return NotFound();
            }

            return prescripcionProducto;
        }

        // PUT: api/PrescripcionProducto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescripcionProducto(int id, PrescripcionProducto prescripcionProducto)
        {
            if (id != prescripcionProducto.IDProducto)
            {
                return BadRequest();
            }

            _context.Entry(prescripcionProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescripcionProductoExists(id))
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

        // POST: api/PrescripcionProducto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrescripcionProducto>> PostPrescripcionProducto(PrescripcionProducto prescripcionProducto)
        {
            _context.PrescripcionProductos.Add(prescripcionProducto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PrescripcionProductoExists(prescripcionProducto.IDProducto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPrescripcionProducto", new { id = prescripcionProducto.IDProducto }, prescripcionProducto);
        }

        // DELETE: api/PrescripcionProducto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescripcionProducto(int id)
        {
            var prescripcionProducto = await _context.PrescripcionProductos.FindAsync(id);
            if (prescripcionProducto == null)
            {
                return NotFound();
            }

            _context.PrescripcionProductos.Remove(prescripcionProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrescripcionProductoExists(int id)
        {
            return _context.PrescripcionProductos.Any(e => e.IDProducto == id);
        }
    }
}
