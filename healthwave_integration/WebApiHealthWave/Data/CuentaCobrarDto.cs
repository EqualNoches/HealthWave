using WebApiHealthWave.Models;

namespace WebApiHealthWave.Data
{
    public class CuentaCobrarDto
    {
        public int IDCuenta { get; set; }
        public decimal Balance { get; set; }
        public char Estado { get; set; }
        public string? CodigoPaciente { get; set; }

        public static CuentaCobrarDto FromModel(CuentaCobrar cuentaCobrar)
        {
            return new CuentaCobrarDto
            {
                IDCuenta = cuentaCobrar.IDCuenta,
                Balance = cuentaCobrar.Balance,
                Estado = cuentaCobrar.Estado,
                CodigoPaciente = cuentaCobrar.CodigoPaciente
            };
        }
    }

}
