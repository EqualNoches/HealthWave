﻿using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class FacturaProducto
{
    public string FacturaCodigo { get; set; } = null!;

    public int Idproducto { get; set; }

    public int? Idautorizacion { get; set; }

    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual Factura FacturaCodigoNavigation { get; set; } = null!;

    public virtual Autorizacion? IdautorizacionNavigation { get; set; }

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}