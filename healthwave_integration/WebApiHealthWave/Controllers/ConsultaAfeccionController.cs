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
    public class ConsultaAfeccionController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/ConsultaAfeccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultaAfeccion>>> GetConsultaAfecciones()
        {
            return await _context.ConsultaAfecciones.ToListAsync();
        }

        // GET: api/ConsultaAfeccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultaAfeccion>> GetConsultaAfeccion(int id)
        {
            var consultaAfeccion = await _context.ConsultaAfecciones.FindAsync(id);

            if (consultaAfeccion == null)
            {
                return NotFound();
            }

            return consultaAfeccion;
        }

        // PUT: api/ConsultaAfeccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultaAfeccion(int id, ConsultaAfeccion consultaAfeccion)
        {
            if (id != consultaAfeccion.ConsultaCodigo)
            {
                return BadRequest();
            }

            _context.Entry(consultaAfeccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaAfeccionExists(id))
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

        // POST: api/ConsultaAfeccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConsultaAfeccion>> PostConsultaAfeccion(ConsultaAfeccion consultaAfeccion)
        {
            _context.ConsultaAfecciones.Add(consultaAfeccion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConsultaAfeccionExists(consultaAfeccion.ConsultaCodigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetConsultaAfeccion", new { id = consultaAfeccion.ConsultaCodigo }, consultaAfeccion);
        }

        // DELETE: api/ConsultaAfeccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultaAfeccion(int id)
        {
            var consultaAfeccion = await _context.ConsultaAfecciones.FindAsync(id);
            if (consultaAfeccion == null)
            {
                return NotFound();
            }

            _context.ConsultaAfecciones.Remove(consultaAfeccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultaAfeccionExists(int id)
        {
            return _context.ConsultaAfecciones.Any(e => e.ConsultaCodigo == id);
        }
    }
}
