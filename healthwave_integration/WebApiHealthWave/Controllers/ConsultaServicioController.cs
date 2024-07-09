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
    public class ConsultaServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsultaServicioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ConsultaServicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultaServicio>>> GetConsultaServicios()
        {
            return await _context.ConsultaServicios.ToListAsync();
        }

        // GET: api/ConsultaServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultaServicio>> GetConsultaServicio(int id)
        {
            var consultaServicio = await _context.ConsultaServicios.FindAsync(id);

            if (consultaServicio == null)
            {
                return NotFound();
            }

            return consultaServicio;
        }

        // PUT: api/ConsultaServicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultaServicio(int id, ConsultaServicio consultaServicio)
        {
            if (id != consultaServicio.ConsultaCodigo)
            {
                return BadRequest();
            }

            _context.Entry(consultaServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaServicioExists(id))
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

        // POST: api/ConsultaServicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConsultaServicio>> PostConsultaServicio(ConsultaServicio consultaServicio)
        {
            _context.ConsultaServicios.Add(consultaServicio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConsultaServicioExists(consultaServicio.ConsultaCodigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetConsultaServicio", new { id = consultaServicio.ConsultaCodigo }, consultaServicio);
        }

        // DELETE: api/ConsultaServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultaServicio(int id)
        {
            var consultaServicio = await _context.ConsultaServicios.FindAsync(id);
            if (consultaServicio == null)
            {
                return NotFound();
            }

            _context.ConsultaServicios.Remove(consultaServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultaServicioExists(int id)
        {
            return _context.ConsultaServicios.Any(e => e.ConsultaCodigo == id);
        }
    }
}
