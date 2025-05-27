using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.ViewModels
{
    public class TransaccionPagoViewModels
    {
        public int IdPlan { get; set; }
        public int IdViaje { get; set; }
        public int? CantidadPersonas { get; set; }

        public decimal? PrecioPlan { get; set; }
        public string Cedula { get; set; } = null!;

        public int IdBanco { get; set; }

        public decimal Monto { get; set; }

        public int IdMetodo { get; set; }
    }
}