using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Interface;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Interface
{
    public interface IRepositoryViaje : IRepositoryGeneric<Viaje, int>
    {
        Task<ViajesGenericoViewModel> ListadoViajes();
        Task<List<Aerolinea>> ListaAerolineas();
        Task<List<Destino>> ListaDestinos();
        Task<ViajesGenericoViewModel> ObtenerViajesFiltrados(FiltroViajesViewModel filtros);

    }
}