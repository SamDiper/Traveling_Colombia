using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Implementacion;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;

namespace TravelingColombia.Repository.Implementacion
{
    public class RepositoryReserva : RepositoryGeneric<Reserva, int>, IRepositoryReserva
    {
        private readonly TravelingColombiabdContext _dbContext;
        public RepositoryReserva(TravelingColombiabdContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Reserva>> GetIncludesAsync()
        {
            return await _dbContext.Reservas
                .Include(a => a.IdPlanNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .Include(a => a.IdViajeNavigation)
                .Include(a => a.EstadoReservaNavigation)
                .ToListAsync();

        }
    }
}