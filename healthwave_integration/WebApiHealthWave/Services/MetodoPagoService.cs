using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using WebApiHealthWave.Data;
using WebApiHealthWave.Models;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Utilities;

namespace WebApiHealthWave.Services
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly AppDbContext _dbContext;
        private readonly LogManager<MetodoPagoService> _logHandler = new();

        public MetodoPagoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddMetodoPagoAsync(MetodoDePagoDto metodoPagoDto)
        {
            try
            {
                var newMetodoPago = MetodoDePago.FromDto(metodoPagoDto);
                _dbContext.MetodosDePago.Add(newMetodoPago);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Metodo de Pago agregado exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al agregar Metodo de Pago.", ex);
                throw;
            }
        }

        public async Task<List<MetodoDePagoDto>> GetMetodosPagoAsync()
        {
            try
            {
                var metodosPago = await _dbContext.MetodosDePago.ToListAsync();
                _logHandler.LogInfo("Metodos de Pago obtenidos exitosamente.");
                return metodosPago.Select(MetodoDePagoDto.FromModel).ToList();
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al obtener los Metodos de Pago.", ex);
                throw;
            }
        }

        public async Task<int> UpdateMetodoPagoAsync(MetodoDePagoDto metodoPagoDto)
        {
            try
            {
                var existingMetodoPago = await _dbContext.MetodosDePago.FindAsync(metodoPagoDto.CodigoMetodoDePago);
                if (existingMetodoPago == null)
                {
                    _logHandler.LogInfo("Metodo de Pago no encontrado.");
                    return 0;
                }

                existingMetodoPago.Nombre = metodoPagoDto.Nombre;
                _dbContext.MetodosDePago.Update(existingMetodoPago);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Metodo de Pago actualizado exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al actualizar Metodo de Pago.", ex);
                throw;
            }
        }

        public async Task<int> DeleteMetodoPagoAsync(uint idMetodoPago)
        {
            try
            {
                var metodoPago = await _dbContext.MetodosDePago.FindAsync(idMetodoPago);
                if (metodoPago == null)
                {
                    _logHandler.LogInfo("Metodo de Pago no encontrado.");
                    return 0;
                }

                _dbContext.MetodosDePago.Remove(metodoPago);
                await _dbContext.SaveChangesAsync();
                _logHandler.LogInfo("Metodo de Pago eliminado exitosamente.");
                return 1;
            }
            catch (Exception ex)
            {
                _logHandler.LogError("Error al eliminar Metodo de Pago.", ex);
                throw;
            }
        }
    }
}
