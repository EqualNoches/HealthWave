using System;
using System.Collections.Generic;
using HospitalCore_core.DTO;

namespace HospitalCore_core.Models;

public partial class Pago
{
    public int Idpago { get; set; }

    public decimal? MontoPagado { get; set; }

    public DateOnly Fecha { get; set; }

    public int? Idcuenta { get; set; }

    public static Pago? FromDTO(PagoDto dto)
    {
        return new Pago()
        {
            Idpago = dto.IdPago,
            Idcuenta = dto.IdCuenta,
            MontoPagado = dto.MontoPagado,
            Fecha = dto.FechaPago
            
        };
    }

    public virtual CuentaCobrar? IdcuentaNavigation { get; set; }
}
