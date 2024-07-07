using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class Autorizacion
{
    public int Idautorizacion { get; set; }

    public DateOnly FechaAutorizacion { get; set; }

    public decimal? MontoAutorizado { get; set; }

    public int? Idaseguradora { get; set; }

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();

    public virtual ICollection<FacturaProducto> FacturaProductos { get; set; } = new List<FacturaProducto>();

    public virtual ICollection<FacturaServicio> FacturaServicios { get; set; } = new List<FacturaServicio>();

    public virtual Aseguradora? IdaseguradoraNavigation { get; set; }

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
}
