using HospitalCore_core.Models;

namespace HospitalCore_core.DTO
{
    public class CuentaCobrarDto
    {
        public int Idcuenta { get; set; }
        public decimal? Balance { get; set; }
        public string Estado { get; set; } = null!;
        public string? CodigoPaciente { get; set; }

        public static CuentaCobrarDto FromModel(CuentaCobrar cuentaCobrar)
        {
            return new CuentaCobrarDto
            {
                Idcuenta = cuentaCobrar.Idcuenta,
                Balance = cuentaCobrar.Balance,
                Estado = cuentaCobrar.Estado,
                CodigoPaciente = cuentaCobrar.CodigoPaciente
            };
        }
    }
}


