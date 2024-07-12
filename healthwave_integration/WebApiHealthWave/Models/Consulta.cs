using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Consulta
    {
        public int ConsultaCodigo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Estado { get; set; }
        public decimal? Costo { get; set; }
        public string? Motivo { get; set; }
        public string? Descripcion { get; set; }
        public string? CodigoPaciente { get; set; }
        public int? IDConsultorio { get; set; }
        public int? IDAutorizacion { get; set; }
        public string? CodigoDocumentoMedico { get; set; }

        public PerfilUsuario? Paciente { get; set; }
        public Consultorio? Consultorio { get; set; }
        public Autorizacion? Autorizacion { get; set; }

        public ICollection<Ingreso>? Ingresos { get; set; }

        public ICollection<Factura>? Facturas { get; set; }
        public ICollection<PrescripcionProducto>? PrescripcionProductos { get; set; } 
        public ICollection<ConsultaAfeccion>? ConsultaAfecciones { get; set; }
        public ICollection<ConsultaServicio>? ConsultaServicios { get; set; }

        public virtual ICollection<Afeccion> Idafeccions { get; set; } = new List<Afeccion>();

        public static Consulta FromDto(ConsultaDto consultaDto)
        {
            return new Consulta
            {
                ConsultaCodigo = consultaDto.ConsultaCodigo,
                IDConsultorio = (int?)consultaDto.IDConsultorio,
                IDAutorizacion = (int?)consultaDto.IDAutorizacion,
                Fecha = consultaDto.Fecha,
                Motivo = consultaDto.Motivo,
                Descripcion = consultaDto.Descripcion,
                Estado = consultaDto.Estado,
                Costo = consultaDto.Costo,
            };
        }


    }
}
