using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class Aseguradora
{
    public int Idaseguradora { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Dirección { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Autorizacion> Autorizacions { get; set; } = new List<Autorizacion>();

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
