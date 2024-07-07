using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models
{
    public partial class Ingreso
    {
        public int IDIngreso { get; set; }
        public decimal? CostoEstancia { get; set; }
        public DateOnly FechaIngreso { get; set; }
        public DateOnly? FechaAlta { get; set; }
        public int? NumSala { get; set; }
        public string? CodigoPaciente { get; set; }
        public string? CodigoDocumentoMedico { get; set; }
        public int? ConsultaCodigo { get; set; }
        public int? Idautorizacion { get; set; }
        
        public virtual PerfilUsuario? CodigoDocumentoMedicoNavigation { get; set; }
        public virtual PerfilUsuario? CodigoPacienteNavigation { get; set; }
        public virtual Consultum? ConsultaCodigoNavigation { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
        public virtual Autorizacion? IdautorizacionNavigation { get; set; }
        public virtual Sala? NumSalaNavigation { get; set; }
        
        public virtual ICollection<Afeccion> Afecciones { get; set; } = new List<Afeccion>();
    }
}