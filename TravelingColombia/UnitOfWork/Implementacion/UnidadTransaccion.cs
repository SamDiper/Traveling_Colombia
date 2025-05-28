using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using TravelingColombia.Models;
using TravelingColombia.Repository.Implementacion;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Interface;

namespace TravelingColombia.UnitOfWork.Implementacion
{
    public class UnidadTransaccion : IUnidadTransaccion
    {
        private readonly IRepositoryPago _repositoryPago;
        private readonly IRepositoryPlan _repositoryPlan;
        private readonly IRepositoryReserva _repositoryReserva;
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositorioFactura _repositorioFactura;
        private readonly IRepositoryViaje _repositorioViajes;
        private readonly TravelingColombiabdContext _context;

        public UnidadTransaccion(TravelingColombiabdContext context)
        {
            _context = context;
        }
        public IRepositoryPago repositoryPago => _repositoryPago ?? new RepositoryPago(_context);

        public IRepositoryPlan repositoryPlan => _repositoryPlan ?? new RepositoryPlan(_context);

        public IRepositoryReserva repositoryReserva => _repositoryReserva ?? new RepositoryReserva(_context);

        public IRepositoryUsuario repositoryUsuario => _repositoryUsuario ?? new RepositoryUsuario(_context);

        public IRepositorioFactura repositorioFactura => _repositorioFactura?? new RepositorioFactura(_context);

        public IRepositoryViaje repositoryViaje => _repositorioViajes ?? new RepositoryViaje(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}