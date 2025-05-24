using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class ReservasGenericoViewModel
    {
        public List<ReservasViewModel>? ListadoReservas { get; set; }
        public List<Viaje>? ListaViajes { get; set; }
        public List<Usuario>? ListaUsuarios { get; set; }
        public List<Estado>? ListaEstados { get; set; }
        public List<Plane>? ListaPlanes { get; set; }

    }
}