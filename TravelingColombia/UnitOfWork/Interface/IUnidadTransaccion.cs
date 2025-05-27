using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using TravelingColombia.Repository.Interface;

namespace TravelingColombia.UnitOfWork.Interface
{
    public interface IUnidadTransaccion : IDisposable
    {
        IRepositoryPago repositoryPago { get; }
        IRepositoryPlan repositoryPlan { get; }
        IRepositoryReserva repositoryReserva { get; }
        IRepositoryUsuario repositoryUsuario { get; }
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}