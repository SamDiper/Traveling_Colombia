using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class PlanViewModel
    {
        public int IdPlan { get; set; }

        public string DestinoIda { get; set; }
        public string Pais { get; set; }

        public string NombrePlan { get; set; } = null!;

        public DateOnly FechaIda { get; set; }

        public DateOnly FechaRegreso { get; set; }

        public string TipoPlan { get; set; }

        public string Descripcion { get; set; } = null!;

        public int CantidadPersonas { get; set; }

        public string Imagen { get; set; } = null!;

        public decimal PrecioPlan { get; set; }

        public string Hotel { get; set; }

        public string Aerolinea { get; set; }

    }
}