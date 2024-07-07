using HospitalCore_core.DTO;
using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public virtual ICollection<FacturaProducto> FacturaProductos { get; set; } = new List<FacturaProducto>();

    public virtual ICollection<FacturaServicio> FacturaServicios { get; set; } = new List<FacturaServicio>();

    public virtual ICollection<PrescripcionProducto> PrescripcionProductos { get; set; } = new List<PrescripcionProducto>();

    public static Producto FromDto(ProductoDto dto)
    {
        return new Producto
        {
            Idproducto = dto.IdProducto,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Costo
        };
    }
}
