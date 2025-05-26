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
        public ViajesGenericoViewModel ListaViajes { get; set; }
        public ViajesViewModel Viaje { get; set; }

            // Campos de búsqueda
        public string BusquedaOrigen { get; set; }
        public string BusquedaDestino { get; set; }
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaRegreso { get; set; }
        public string CiudadDestinoPlan { get; set; }

        // Bandera para saber qué mostrar
        public string TipoBusqueda { get; set; } // "viajes", "planes", o null
    }
}