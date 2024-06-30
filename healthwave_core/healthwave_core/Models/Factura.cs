using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class Factura
{
    public string FacturaCodigo { get; set; } = null!;

    public decimal? MontoTotal { get; set; }

    public decimal? MontoSubtotal { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Rnc { get; set; }

    public int? CodigoMetodoDePago { get; set; }

    public string? CodigoPaciente { get; set; }

    public int? Idingreso { get; set; }

    public int? Idcuenta { get; set; }

    public int? ConsultaCodigo { get; set; }

    public virtual MetodoDePago? CodigoMetodoDePagoNavigation { get; set; }

    public virtual PerfilUsuario? CodigoPacienteNavigation { get; set; }

    public virtual Consultum? ConsultaCodigoNavigation { get; set; }

    public virtual ICollection<FacturaProducto> FacturaProductos { get; set; } = new List<FacturaProducto>();

    public virtual ICollection<FacturaServicio> FacturaServicios { get; set; } = new List<FacturaServicio>();

    public virtual CuentaCobrar? IdcuentaNavigation { get; set; }

    public virtual Ingreso? IdingresoNavigation { get; set; }
}
