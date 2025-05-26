using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class PlanesViewModel
    {
        public List<PlanViewModel>? ListadoPlanes { get; set; }
        public List<TipoPlan>? ListadoTipoPlanes { get; set; }
        public List<Hotele>? ListadoHoteles { get; set; }
        public List<Aerolinea>? ListadoAerolinea { get; set; }
        public List<Destino>? ListadoDestino { get; set; }
        
        
    }
}