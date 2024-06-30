using HospitalCore_core.Context;
using HospitalCore_core.DTO;
using HospitalCore_core.Models;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core.Services;

public class ServicioService : IServicioService
{
    private readonly HospitalCore _context;

    public ServicioService(HospitalCore context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServicioDto>> GetAllServicios()
    {
        return await _context.Servicios
            .Select(s => new ServicioDto
            {
                ServicioCodigo = s.ServicioCodigo,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                IDTipoServicio = s.IDTipoServicio,
                Costo = s.Costo,
                IDAseguradora = s.IDAseguradora
            }).ToListAsync();
    }

    public async Task<ServicioDto> GetServicioById(int id)
    {
        var servicio = await _context.Servicios.FindAsync(id);

        if (servicio == null)
        {
            return null;
        }

        return new ServicioDto
        {
            ServicioCodigo = servicio.ServicioCodigo,
            Nombre = servicio.Nombre,
            Descripcion = servicio.Descripcion,
            IDTipoServicio = servicio.IDTipoServicio,
            Costo = servicio.Costo,
            IDAseguradora = servicio.IDAseguradora
        };
    }

    public async Task<ServicioDto> CreateServicio(ServicioDto servicioDto)
    {
        var servicio = new Servicio
        {
            ServicioCodigo = servicioDto.ServicioCodigo,
            Nombre = servicioDto.Nombre,
            Descripcion = servicioDto.Descripcion,
            IDTipoServicio = servicioDto.IDTipoServicio,
            Costo = servicioDto.Costo,
            IDAseguradora = servicioDto.IDAseguradora
        };

        _context.Servicios.Add(servicio);
        await _context.SaveChangesAsync();

        servicioDto.ServicioCodigo = servicio.ServicioCodigo;
        return servicioDto;
    }

    public async Task<ServicioDto> UpdateServicio(ServicioDto servicioDto)
    {
        var servicio = await _context.Servicios.FindAsync(servicioDto.ServicioCodigo);

        if (servicio == null)
        {
            return null;
        }

        servicio.Nombre = servicioDto.Nombre;
        servicio.Descripcion = servicioDto.Descripcion;
        servicio.IDTipoServicio = servicioDto.IDTipoServicio;
        servicio.Costo = servicioDto.Costo;
        servicio.IDAseguradora = servicioDto.IDAseguradora;

        await _context.SaveChangesAsync();

        return servicioDto;
    }

    public async Task<bool> DeleteServicio(int id)
    {
        var servicio = await _context.Servicios.FindAsync(id);

        if (servicio == null)
        {
            return false;
        }

        _context.Servicios.Remove(servicio);
        await _context.SaveChangesAsync();

        return true;
    }
}
