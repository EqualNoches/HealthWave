using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core.Services
{
    public class AfeccionService : IAfeccionService
    {
        private readonly HospitalCore _context;

        public AfeccionService(HospitalCore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AfeccionDto>> GetAfecciones()
        {
            return await _context.Afeccions
                .Select(a => new AfeccionDto
                {
                    IdAfeccion = a.IdAfeccion,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion
                }).ToListAsync();
        }

        public async Task<AfeccionDto?> GetAfeccionById(int id)
        {
            var afeccion = await _context.Afeccions.FindAsync(id);

            if (afeccion == null)
                return null;

            return new AfeccionDto
            {
                IdAfeccion = afeccion.IdAfeccion,
                Nombre = afeccion.Nombre,
                Descripcion = afeccion.Descripcion
            };
        }

        public async Task<AfeccionDto> CreateAfeccion(AfeccionDto afeccionDto)
        {
            var afeccion = new Afeccion
            {
                Nombre = afeccionDto.Nombre,
                Descripcion = afeccionDto.Descripcion
            };

            _context.Afeccions.Add(afeccion);
            await _context.SaveChangesAsync();

            afeccionDto.IdAfeccion = afeccion.IdAfeccion;
            return afeccionDto;
        }

        public async Task<AfeccionDto?> UpdateAfeccion(int id, AfeccionDto afeccionDto)
        {
            var afeccion = await _context.Afeccions.FindAsync(id);

            if (afeccion == null)
                return null;

            afeccion.Nombre = afeccionDto.Nombre;
            afeccion.Descripcion = afeccionDto.Descripcion;

            await _context.SaveChangesAsync();
            return afeccionDto;
        }

        public async Task<bool> DeleteAfeccion(int id)
        {
            var afeccion = await _context.Afeccions.FindAsync(id);

            if (afeccion == null)
                return false;

            _context.Afeccions.Remove(afeccion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
