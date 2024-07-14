using System.Collections.Generic;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Converter
{
    public interface IIntegrationEntity
    {
        int Id { get; set; }
        int IdEstado { get; set; }
    }
}
