﻿using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Consultorio
    {
        public int IDConsultorio { get; set; }
        public string? Nombre { get; set; }
        public string? Dirección { get; set; }
        public string? Teléfono { get; set; }
    }
}
