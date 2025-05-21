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
    public class RepositoryPlan : RepositoryGeneric<Plane, int>, IRepositoryPlan
    {
        private readonly TravelingColombiabdContext _dbContext;
        public RepositoryPlan(TravelingColombiabdContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Plane>> GetIncludesAsync()
        {
            return await _dbContext.Planes
                    .Include(a => a.IdDestinoIdaNavigation)
                    .Include(a => a.IdAerolineaNavigation)
                    .Include(a => a.IdHotelNavigation)
                    .Include(a => a.IdTipoPlanNavigation)
                    .ToListAsync();
        }
    }
}