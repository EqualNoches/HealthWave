using WebApiHealthWave.Data;

namespace WebApiHealthWave.Models
{
    public class Ingreso
    {
        public int IDIngreso { get; set; }
        public decimal CostoEstancia { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int? NumSala { get; set; }
        public string? CodigoPaciente { get; set; }
        public string? CodigoDocumentoMedico { get; set; }
        public int? ConsultaCodigo { get; set; }
        public int? IDAutorizacion { get; set; }

        public Sala? Sala { get; set; }
        public PerfilUsuario? Paciente { get; set; }
        public PerfilUsuario? Medico { get; set; }
        public Consulta? Consulta { get; set; }
        public Autorizacion? Autorizacion { get; set; }

        public ICollection<Factura>? Facturas { get; set; }

        public ICollection<IngresoAfeccion>? IngresoAfecciones { get; set; }


        public virtual ICollection<Afeccion> Afecciones { get; set; } = new List<Afeccion>();


    }
}
