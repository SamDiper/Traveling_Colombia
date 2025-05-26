using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.ViewModels
{
    public class LayoutViewModel
    {
        public PlanesViewModel listaPlanes { get; set; }
        public PlanViewModel Plan { get; set; }

        public ViajesViewModel Viaje { get; set; }
    }
}