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
        Task<IEnumerable<Viaje>> GetIncludesAsync();
        Task<ViajesViewModel> ObtenerViajesFiltradosAsync(FiltroViajesViewModel filtros);

        
    }
}