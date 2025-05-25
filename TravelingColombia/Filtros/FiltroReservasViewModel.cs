using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.Filtros
{
    public class FiltroReservasViewModel
    {
        public int IdReserva { get; set; }
        public DateOnly FechaReserva { get; set; }
        public int? IdViaje { get; set; }
        public int IdUsuario { get; set; }
        public int? IdPlan { get; set; }
        public int IdEstadoReserva { get; set; }
        public decimal PrecioPlan { get; set; }
        public decimal PrecioViaje { get; set; }

    }
}