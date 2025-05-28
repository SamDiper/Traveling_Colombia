using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.ViewModels
{
    public class VistaIndex
    {
        public ListaViajes ListaViajes { get; set; } = new ListaViajes();
        public ListaPlanes ListaPlanes { get; set; } = new ListaPlanes();
    }

}