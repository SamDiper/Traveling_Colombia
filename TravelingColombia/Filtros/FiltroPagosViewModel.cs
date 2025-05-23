using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.Filtros
{
    public class FiltroPagosViewModel
    {
        
        public int IdPago { get; set; }
        public string? Nombre { get; set; } 

        public string? Cedula { get; set; } 

        public int? IdBanco { get; set; }

        public string? Cuenta { get; set; } 

        public decimal? Monto { get; set; }

        public int? IdMetodo { get; set; }
    }
}