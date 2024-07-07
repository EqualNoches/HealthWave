using System;

namespace HospitalCore_core.DTO
{
    public class ConsultorioDTO
    {
        public int IDConsultorio { get; set; }
        public string Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}