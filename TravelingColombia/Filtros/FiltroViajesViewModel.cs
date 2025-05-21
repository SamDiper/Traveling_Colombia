using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.Filtros
{
    public class FiltroViajesViewModel
    {
        public string DestinoIda { get; set; }
        public string DestinoLlegada { get; set; }
        public TimeOnly? HoraSalida { get; set; }
        public TimeOnly? HoraLlegada { get; set; }
        public DateTime? FechaMinima { get; set; }
        public DateTime? FechaMaxima { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public int? AerolineaId { get; set; }
    }
}