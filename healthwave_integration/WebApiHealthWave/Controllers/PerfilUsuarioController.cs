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
    public class PerfilUsuarioController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/PerfilUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilUsuario>>> GetPerfilUsuarios()
        {
            return await _context.PerfilUsuarios.ToListAsync();
        }

        // GET: api/PerfilUsuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilUsuario>> GetPerfilUsuario(string id)
        {
            var perfilUsuario = await _context.PerfilUsuarios.FindAsync(id);

            if (perfilUsuario == null)
            {
                return NotFound();
            }

            return perfilUsuario;
        }

        // PUT: api/PerfilUsuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfilUsuario(string id, PerfilUsuario perfilUsuario)
        {
            if (id != perfilUsuario.CodigoDocumento)
            {
                return BadRequest();
            }

            _context.Entry(perfilUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilUsuarioExists(id))
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

        // POST: api/PerfilUsuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PerfilUsuario>> PostPerfilUsuario(PerfilUsuario perfilUsuario)
        {
            _context.PerfilUsuarios.Add(perfilUsuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (perfilUsuario.CodigoDocumento != null && PerfilUsuarioExists(perfilUsuario.CodigoDocumento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPerfilUsuario", new { id = perfilUsuario.CodigoDocumento }, perfilUsuario);
        }

        // DELETE: api/PerfilUsuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfilUsuario(string id)
        {
            var perfilUsuario = await _context.PerfilUsuarios.FindAsync(id);
            if (perfilUsuario == null)
            {
                return NotFound();
            }

            _context.PerfilUsuarios.Remove(perfilUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfilUsuarioExists(string? id)
        {
            return id != null && _context.PerfilUsuarios.Any(e => e.CodigoDocumento == id);
        }
    }
}
