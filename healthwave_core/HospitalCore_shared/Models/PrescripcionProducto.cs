using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class PrescripcionProducto
{
    public int Idproducto { get; set; }

    public int ConsultaCodigo { get; set; }

    public int? Cantidad { get; set; }

    public virtual Consultum ConsultaCodigoNavigation { get; set; } = null!;

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
