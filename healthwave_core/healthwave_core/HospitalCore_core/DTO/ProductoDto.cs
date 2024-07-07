using HospitalCore_core.Models;

namespace HospitalCore_core.DTO
{
    public class ProductoDto
    {

        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal? Costo { get; set; }

        public static ProductoDto? FromModel(Producto model)
        {
            return new ProductoDto
            {
                IdProducto = model.Idproducto,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Costo = model.Precio
            };
        }
    }
}
