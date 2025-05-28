using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
public class ListaPlanes
{
    public List<Plane> ListadoPlanes { get; set; } = new List<Plane>();
    public List<Destino> ListadoDestino { get; set; } = new List<Destino>();
    public List<TipoPlan> ListadoTipoPlanes { get; set; } = new List<TipoPlan>();
}

}