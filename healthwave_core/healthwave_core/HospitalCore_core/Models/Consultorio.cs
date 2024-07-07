using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class Consultorio
{
    public int IDConsultorio { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Consultum> ConsultaCodigo { get; set; } = new List<Consultum>();
}
