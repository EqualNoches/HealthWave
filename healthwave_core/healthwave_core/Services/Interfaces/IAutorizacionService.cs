using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalCore_core.Models;

namespace HospitalCore_core.Services.Interfaces
{
    public interface IAutorizacionService
    {
        Task<Autorizacion?> GetAutorizacionById(int id);
        Task<int> AddAutorizacion(Autorizacion autorizacion, int? idIngreso, string? consultaCodigo,
            string? facturaCodigo, string? servicioCodigo, int? idProducto);
        Task<int> UpdateAutorizacionAsync(Autorizacion autorizacion);
        Task<int> DeleteAutorizacionAsync(int id);
        Task<List<Autorizacion>> GetAllAutorizaciones();
    }
}