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
        
        
    }
}