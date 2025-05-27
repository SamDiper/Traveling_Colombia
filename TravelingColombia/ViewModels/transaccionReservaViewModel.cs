using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.ViewModels
{
    public class transaccionReservaViewModel
    {
        public int IdPlan { get; set; }
        public int IdViaje { get; set; }
        public int? CantidadPersonas { get; set; }

        public decimal? PrecioPlan { get; set; }


    }
}