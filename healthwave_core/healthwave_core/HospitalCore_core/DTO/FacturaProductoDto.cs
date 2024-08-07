using HospitalCore_core.Models;

namespace HospitalCore_core.DTO;

public class FacturaProductoDto
{
    public string FacturaCodigoProducto { get; set; } = null!;

    public int Idproducto { get; set; }

    public int? Idautorizacion { get; set; }

    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public static FacturaProductoDto FromModel(FacturaProducto facturaProducto)
    {
        return new FacturaProductoDto
        {
            FacturaCodigoProducto = facturaProducto.FacturaCodigoProducto,
            Idproducto = facturaProducto.Idproducto,
            Idautorizacion = facturaProducto.Idautorizacion,
            Precio = facturaProducto.Precio,
            Cantidad = facturaProducto.Cantidad
        };
    }
}