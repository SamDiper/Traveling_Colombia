using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.ViewModels
{
    public class PagosViewModel
    {
        public string? NombreUsuario { get; set; }
        public string? CedulaUsuario { get; set; }
        public string? CuentaBanco { get; set; }
        public string? NombreBanco { get; set; }
        public string? MetodoPago { get; set; }
        public decimal Monto { get; set; }

    }
}