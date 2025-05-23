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
        public Viaje Viaje { get; set; }
        public List<Destino> ListadoDestinos { get; set; }
        public List<Viaje>? ListadoViajes { get; set; }
        public List<Aerolinea>? ListadoAerolineas { get; set; }
        public FiltroViajesViewModel? Filtros { get; set; }
    }
}