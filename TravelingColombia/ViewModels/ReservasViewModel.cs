using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.ViewModels
{
    public class ReservasViewModel
    {
        public int IdReserva { get; set; }

        public DateOnly FechaReserva { get; set; }

        public string? NombreViaje { get; set; }

        public string EstadoReserva { get; set; }

        public string NombreUsuario { get; set; }

        public string? NombrePlan { get; set; }
        public decimal PrecioPlan { get; set; }
        public decimal PrecioViaje { get; set; }
        public decimal? TotalReserva { get; set; }
        public int? CantidadPersonas { get; set; }
        public decimal? PagoUsuario { get; set; }
        public int? IdViaje { get; set; }
        public int? IdPlan { get; set; }
        public int IdUsuario { get; set; }
    }
}