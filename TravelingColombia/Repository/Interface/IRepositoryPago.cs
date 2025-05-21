using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Interface;
using TravelingColombia.Models;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Interface
{
    public interface IRepositoryPago : IRepositoryGeneric<Pago, int>
    {
        Task<List<PagosViewModel>> ListadoPagos();
        Task<List<PagosViewModel>> ObtenerPagos(string filtro = null);
    }

    
}