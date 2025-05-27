using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Implementacion;
using Repository.Interface;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Implementacion
{
    public class RepositoryReserva : RepositoryGeneric<Reserva, int>, IRepositoryReserva
    {
        private readonly TravelingColombiabdContext _dbContext;

        private readonly IRepositoryGeneric<Viaje, int> _ListaViajes;
        private readonly IRepositoryGeneric<Plane, int> _ListaPlanes;
        private readonly IRepositoryGeneric<Estado, int> _ListaEstados;
        private readonly IRepositoryGeneric<Usuario, int> _ListaUsuarios;

        public RepositoryReserva(IRepositoryGeneric<Usuario, int> ListaUsuarios, IRepositoryGeneric<Estado, int> ListaEstados, IRepositoryGeneric<Plane, int> ListaPlanes, IRepositoryGeneric<Viaje, int> ListaViajes, TravelingColombiabdContext context) : base(context)
        {
            _dbContext = context;
            _ListaEstados = ListaEstados;
            _ListaPlanes = ListaPlanes;
            _ListaViajes = ListaViajes;
            _ListaUsuarios = ListaUsuarios;
        }
        public RepositoryReserva(TravelingColombiabdContext dbContext):base(dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<List<Estado>> ListaEstados()
        {
            return await _ListaEstados.GetAllAsync();
        }

        public async Task<List<Plane>> ListaPlanes()
        {
            return await _ListaPlanes.GetAllAsync();
        }

        public async Task<ReservasGenericoViewModel> ListaReservas()
        {
            var lista = await (from r in _dbContext.Reservas
                                join u in _dbContext.Usuarios on r.IdUsuario equals u.IdUsuario
                                join e in _dbContext.Estados on r.IdEstadoReserva equals e.IdEstado
                                join v in _dbContext.Viajes on r.IdViaje equals v.IdViaje into viajeJoin
                                from v in viajeJoin.DefaultIfEmpty()
                                join d in _dbContext.Destinos on v.IdDestinoLlegada equals d.IdDestino into destinoJoin
                                from d in destinoJoin.DefaultIfEmpty()
                                join p in _dbContext.Planes on r.IdPlan equals p.IdPlan into planJoin
                                from p in planJoin.DefaultIfEmpty()

                                select new ReservasViewModel
                                {
                                    IdReserva = r.IdReserva,
                                    FechaReserva = r.FechaReserva,
                                    NombreUsuario = u.NombreUsuario,
                                    EstadoReserva = e.Estado1,
                                    NombreViaje = d != null ? d.NombreDestino : "Sin destino",
                                    NombrePlan = p != null ? p.NombrePlan : "Sin plan"
                                }).ToListAsync();

            return new ReservasGenericoViewModel
            {
                ListadoReservas = lista,
                ListaUsuarios = await _dbContext.Usuarios.ToListAsync(),
                ListaEstados = await _dbContext.Estados.ToListAsync(),
                ListaViajes = await _dbContext.Viajes.ToListAsync(),
                ListaPlanes = await _dbContext.Planes.ToListAsync()
            };
        }

        public async Task<List<Usuario>> ListaUsuarios()
        {
            return await _ListaUsuarios.GetAllAsync();
        }

        public async Task<List<Viaje>> ListaViajes()
        {
            return await _ListaViajes.GetAllAsync();
        }

        public async Task<ReservasGenericoViewModel> ObtenerReservasFiltrados(FiltroReservasViewModel filtros)
        {
            var query = from r in _dbContext.Reservas
                        join u in _dbContext.Usuarios on r.IdUsuario equals u.IdUsuario
                        join e in _dbContext.Estados on r.IdEstadoReserva equals e.IdEstado
                        join v in _dbContext.Viajes on r.IdViaje equals v.IdViaje into viajeJoin
                        from v in viajeJoin.DefaultIfEmpty()
                        join d in _dbContext.Destinos on v.IdDestinoLlegada equals d.IdDestino into destinoJoin
                        from d in destinoJoin.DefaultIfEmpty()
                        join p in _dbContext.Planes on r.IdPlan equals p.IdPlan into planJoin
                        from p in planJoin.DefaultIfEmpty()
                        select new ReservasViewModel
                        {
                            IdReserva = r.IdReserva,
                            FechaReserva = r.FechaReserva,
                            NombreUsuario = u.NombreUsuario,
                            EstadoReserva = e.Estado1,
                            NombreViaje = d != null ? d.NombreDestino : "Sin destino",
                            NombrePlan = p != null ? p.NombrePlan : "Sin plan",
                            PrecioPlan = p != null ? p.PrecioPlan : 0,
                            PrecioViaje = v != null ? v.PrecioViaje : 0,
                            IdViaje = r.IdViaje,
                            IdPlan = r.IdPlan,
                            IdUsuario = r.IdUsuario
                        };

            // Aplicar filtros
            if (filtros.FechaReserva != default)
                query = query.Where(r => r.FechaReserva == filtros.FechaReserva);

            if (filtros.IdViaje.HasValue)
                query = query.Where(r => r.IdViaje == filtros.IdViaje);

            if (filtros.IdPlan.HasValue)
                query = query.Where(r => r.IdPlan == filtros.IdPlan);

            if (filtros.IdUsuario != 0)
                query = query.Where(r => r.IdUsuario == filtros.IdUsuario);

            if (filtros.PrecioPlan > 0)
                query = query.Where(r => r.PrecioPlan == filtros.PrecioPlan);

            if (filtros.PrecioViaje > 0)
                query = query.Where(r => r.PrecioViaje == filtros.PrecioViaje);

            var resultado = new ReservasGenericoViewModel
            {
                ListadoReservas = await query.ToListAsync(),
                ListaUsuarios = await _dbContext.Usuarios.ToListAsync(),
                ListaEstados = await _dbContext.Estados.ToListAsync(),
                ListaViajes = await _dbContext.Viajes.ToListAsync(),
                ListaPlanes = await _dbContext.Planes.ToListAsync()
            };

            return resultado;
        }
    }
}