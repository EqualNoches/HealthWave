using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class Sala
{
    public int NumSala { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
}
