using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Implementacion;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Implementacion
{
    public class RepositoryViaje : RepositoryGeneric<Viaje, int>, IRepositoryViaje
    {
        private readonly TravelingColombiabdContext _dbContext;
        public RepositoryViaje(TravelingColombiabdContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Viaje>> GetIncludesAsync()
        {
            return await _dbContext.Viajes
                .Include(a => a.IdAerolineaNavigation)
                .Include(d => d.IdDestinoIdaNavigation)
                .Include(d => d.IdDestinoLlegadaNavigation)
                .ToListAsync();
        }

        public async Task<ViajesViewModel> ObtenerViajesFiltradosAsync(FiltroViajesViewModel filtros)
        {
            var query = _dbContext.Viajes
    .Include(v => v.IdAerolineaNavigation)
    .Include(v => v.IdDestinoIdaNavigation)
    .Include(v => v.IdDestinoLlegadaNavigation)
    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtros.DestinoIda))
                query = query.Where(v => v.IdDestinoIdaNavigation.NombreDestino.Contains(filtros.DestinoIda));

            if (!string.IsNullOrWhiteSpace(filtros.DestinoLlegada))
                query = query.Where(v => v.IdDestinoLlegadaNavigation.NombreDestino.Contains(filtros.DestinoLlegada));

            if (filtros.HoraSalida.HasValue)
                query = query.Where(v => v.HoraSalida >= filtros.HoraSalida.Value);

            if (filtros.HoraLlegada.HasValue)
                query = query.Where(v => v.HoraLlegada <= filtros.HoraLlegada.Value);

            if (filtros.FechaMinima.HasValue)
                query = query.Where(v => v.FechaViaje >= DateOnly.FromDateTime(filtros.FechaMinima.Value));

            if (filtros.FechaMaxima.HasValue)
                query = query.Where(v => v.FechaViaje <= DateOnly.FromDateTime(filtros.FechaMaxima.Value));

            if (filtros.PrecioMinimo.HasValue)
                query = query.Where(v => v.PrecioViaje >= filtros.PrecioMinimo.Value);

            if (filtros.PrecioMaximo.HasValue)
                query = query.Where(v => v.PrecioViaje <= filtros.PrecioMaximo.Value);

            if (filtros.AerolineaId.HasValue)
                query = query.Where(v => v.IdAerolinea == filtros.AerolineaId.Value);

            var viewModel = new ViajesViewModel
            {
                ListadoViajes = await query.ToListAsync(),
                ListadoAerolineas = await _dbContext.Aerolineas.ToListAsync(),
                Filtros = filtros
            };

            return viewModel;
        }
    }
}