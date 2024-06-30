using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class FacturaServicio
{
    public string FacturaCodigo { get; set; } = null!;

    public int Idproducto { get; set; }

    public int? Idautorizacion { get; set; }

    public decimal? Costo { get; set; }

    public string? ServicioCodigo { get; set; }

    public virtual Factura FacturaCodigoNavigation { get; set; } = null!;

    public virtual Autorizacion? IdautorizacionNavigation { get; set; }

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}