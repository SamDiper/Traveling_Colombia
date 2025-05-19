using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;
using TravelingColombia.Repository.Implementacion;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Interface;

namespace TravelingColombia.UnitOfWork.Implementacion
{

    public class UnitUser : IUnitUser
    {
        private readonly IRepositoryAdmin _repositoryAdmin;
        private readonly TravelingColombiabdContext _dbContext;

        public UnitUser(TravelingColombiabdContext context)
        {
            _dbContext = context;
        }

        public IRepositoryAdmin repositoryAdmin => _repositoryAdmin?? new RepositoryAdmin(_dbContext);

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}