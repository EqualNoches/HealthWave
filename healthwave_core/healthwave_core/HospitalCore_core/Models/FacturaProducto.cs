using System;
using System.Collections.Generic;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Models;

public partial class FacturaProducto
{
    public string FacturaCodigoProducto { get; set; } = null!;

    public int Idproducto { get; set; }

    public int? Idautorizacion { get; set; }

    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual Factura FacturaCodigoNavigation { get; set; } = null!;

    public virtual Autorizacion? IdautorizacionNavigation { get; set; }

    public virtual Producto IdproductoNavigation { get; set; } = null!;

    public static FacturaProducto FromDto(FacturaProductoDto facturaProductoDto)
    {
        return new FacturaProducto
        {
            FacturaCodigoProducto = facturaProductoDto.FacturaCodigoProducto,
            Idproducto = facturaProductoDto.Idproducto,
            Idautorizacion = facturaProductoDto.Idautorizacion,
            Precio = facturaProductoDto.Precio,
            Cantidad = facturaProductoDto.Cantidad
        };
    }
}