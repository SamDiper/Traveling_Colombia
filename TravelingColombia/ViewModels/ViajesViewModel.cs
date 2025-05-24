using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Filtros;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class ViajesViewModel
    {
        public int IdViaje { get; set; }
        public string DestinoIda { get; set; }
        public string DestinoLlegada { get; set; }
        public DateOnly FechaViaje { get; set; }
        public TimeOnly HoraSalida { get; set; }
        public TimeOnly HoraLlegada { get; set; }
        public decimal PrecioViaje { get; set; }
        public int CantidadPuestos { get; set; }
        public string AerolineaNombre { get; set; }
        public string Imagen { get; set; }
    }
}