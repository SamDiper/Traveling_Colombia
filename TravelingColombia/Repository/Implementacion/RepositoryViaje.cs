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
    }
}