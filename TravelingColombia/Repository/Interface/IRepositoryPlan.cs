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
    public interface IRepositoryPlan : IRepositoryGeneric<Plane, int>
    {
        Task<PlanesViewModel> listadoPlanes();
        Task<PlanesViewModel> ObtenerPlanesFiltrados(FiltroPlanesViewModel filtros);
        Task<PlanViewModel> ObtenerPlan(int id);
    }
}