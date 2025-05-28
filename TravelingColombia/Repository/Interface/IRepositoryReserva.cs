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
    public interface IRepositoryReserva : IRepositoryGeneric<Reserva, int>
    {
        Task<ReservasGenericoViewModel> ListaReservas();
        Task<List<Plane>> ListaPlanes();
        Task<List<Viaje>> ListaViajes();
        Task<List<Estado>> ListaEstados();
        Task<List<Usuario>> ListaUsuarios();
        Task<int> UltimoRegistro();
        Task<ReservasGenericoViewModel> ObtenerReservasFiltrados(FiltroReservasViewModel filtros);
        Task<ReservasGenericoViewModel> ReservasUsuario(int id);
    }
}