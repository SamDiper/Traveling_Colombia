using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.Filtros
{
    public class FiltroPlanesViewModel
    {
        public string? NombreDestino { get; set; }
        public string? NombrePlan { get; set; }
        public int? IdTipoPlan { get; set; }
        public DateOnly? Fecha { get; set; }
        public decimal? Precio { get; set; }

    }
}