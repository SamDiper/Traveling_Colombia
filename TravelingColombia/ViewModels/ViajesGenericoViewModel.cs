using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Filtros;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class ViajesGenericoViewModel
    {
        public List<ViajesViewModel>? ListadoViajes { get; set; }
        public List<Aerolinea>? ListadoAerolinea { get; set; }
        public List<Destino>? ListadoDestino { get; set; }

    }
}