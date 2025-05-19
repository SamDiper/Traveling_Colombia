using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Repository.Interface;

namespace TravelingColombia.UnitOfWork.Interface
{
    public interface IUnitUser : IDisposable
    {
        IRepositoryAdmin repositoryAdmin {get;}
        Task<int> SaveChangesAsync();

    }
}