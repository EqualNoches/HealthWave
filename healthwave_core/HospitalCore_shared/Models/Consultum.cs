using HospitalCore_core.DTO;
using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class Consultum
{
    public int ConsultaCodigo { get; set; }

    public DateOnly Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public decimal? Costo { get; set; }

    public string? Motivo { get; set; }

    public string? Descripcion { get; set; }

    public string? CodigoPaciente { get; set; }

    public int? Idconsultorio { get; set; }

    public int? Idautorizacion { get; set; }

    public string? CodigoDocumentoMedico { get; set; }

    public virtual PerfilUsuario? CodigoPacienteNavigation { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Autorizacion? IdautorizacionNavigation { get; set; }

    public virtual Consultorio? IdconsultorioNavigation { get; set; }

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

    public virtual ICollection<PrescripcionProducto> PrescripcionProductos { get; set; } = new List<PrescripcionProducto>();

    public virtual ICollection<Afeccion> Idafeccions { get; set; } = new List<Afeccion>();

    public virtual ICollection<Servicio> ServicioCodigos { get; set; } = new List<Servicio>();
    public static Consultum FromDto(ConsultaDto consultaDto)
    {
        return new Consultum
        {
            ConsultaCodigo = consultaDto.ConsultaCodigo,
            CodigoPaciente = consultaDto.DocumentoPaciente,
            CodigoDocumentoMedico = consultaDto.DocumentoMedico,
            Idconsultorio = (int)consultaDto.IdConsultorio,
            Idautorizacion = (int)consultaDto.IdAutorizacion,
            Fecha = consultaDto.Fecha,
            Motivo = consultaDto.Motivo,
            Descripcion = consultaDto.Comentarios,
            Estado = consultaDto.Estado,
            Costo = consultaDto.costo
        };
    }
}
