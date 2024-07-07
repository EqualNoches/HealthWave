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
    public class ReservaServicioController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/ReservaServicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaServicio>>> GetReservaServicios()
        {
            return await _context.ReservaServicios.ToListAsync();
        }

        // GET: api/ReservaServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaServicio>> GetReservaServicio(int id)
        {
            var reservaServicio = await _context.ReservaServicios.FindAsync(id);

            if (reservaServicio == null)
            {
                return NotFound();
            }

            return reservaServicio;
        }

        // PUT: api/ReservaServicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservaServicio(int id, ReservaServicio reservaServicio)
        {
            if (id != reservaServicio.ServicioCodigo)
            {
                return BadRequest();
            }

            _context.Entry(reservaServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaServicioExists(id))
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

        // POST: api/ReservaServicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservaServicio>> PostReservaServicio(ReservaServicio reservaServicio)
        {
            _context.ReservaServicios.Add(reservaServicio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReservaServicioExists(reservaServicio.ServicioCodigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReservaServicio", new { id = reservaServicio.ServicioCodigo }, reservaServicio);
        }

        // DELETE: api/ReservaServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservaServicio(int id)
        {
            var reservaServicio = await _context.ReservaServicios.FindAsync(id);
            if (reservaServicio == null)
            {
                return NotFound();
            }

            _context.ReservaServicios.Remove(reservaServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaServicioExists(int id)
        {
            return _context.ReservaServicios.Any(e => e.ServicioCodigo == id);
        }
    }
}
