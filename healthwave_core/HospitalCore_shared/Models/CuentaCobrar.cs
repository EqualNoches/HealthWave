using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models;

public partial class CuentaCobrar
{
    public int Idcuenta { get; set; }

    public decimal? Balance { get; set; }

    public string Estado { get; set; } = null!;

    public string? CodigoPaciente { get; set; }

    public virtual PerfilUsuario? CodigoPacienteNavigation { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
