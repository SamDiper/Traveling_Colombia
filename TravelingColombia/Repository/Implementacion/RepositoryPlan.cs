using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Implementacion;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;

namespace TravelingColombia.Repository.Implementacion
{
    public class RepositoryPlan : RepositoryGeneric<Plane, int>, IRepositoryPlan
    {
        public RepositoryPlan(TravelingColombiabdContext context) : base(context)
        {
        }

        public Task<IEnumerable<Plane>> GetIncludesAsync()
        {
            throw new NotImplementedException();
        }
    }
}