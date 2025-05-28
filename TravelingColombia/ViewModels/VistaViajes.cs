using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class ListaViajes
    {
        public List<Viaje> ListadoViajes { get; set; } = new List<Viaje>();
        public List<Destino> ListadoDestino { get; set; } = new List<Destino>();
        public List<Aerolinea> ListadoAerolinea { get; set; } = new List<Aerolinea>();
    }

}