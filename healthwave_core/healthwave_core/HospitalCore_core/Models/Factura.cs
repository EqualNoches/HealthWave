using System;
using System.Collections.Generic;

using HospitalCore_core.DTO;

namespace HospitalCore_core.Models;

public partial class Factura
{
    public string FacturaCodigo { get; set; }
    
    public decimal MontoTotal { get; set; } = 0.00m;
    
    public decimal MontoSubtotal { get; set; } = 0.00m;
    
    public DateTime Fecha { get; set; }
    
    public string Rnc { get; set; }
    public int? CodigoMetodoDePago { get; set; }
    
    public string CodigoPaciente { get; set; }

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
    
    
    public static Factura FromDto(FacturaDto facturaDto)
    {
        return new Factura
        {
            FacturaCodigo = facturaDto.FacturaCodigo,
            MontoTotal = facturaDto.MontoTotal,
            MontoSubtotal = facturaDto.MontoSubtotal,
            Fecha = facturaDto.Fecha,
            Rnc = facturaDto.Rnc,
            CodigoMetodoDePago = facturaDto.CodigoMetodoDePago,
            CodigoPaciente = facturaDto.CodigoPaciente,
            Idingreso = facturaDto.Idingreso,
            Idcuenta = facturaDto.Idcuenta,
            ConsultaCodigo = facturaDto.ConsultaCodigo
        };
    }
}
