using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Interface;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Interface
{
    public interface IRepositoryPago : IRepositoryGeneric<Pago, int>
    {
        Task<PagosGenericoViewModel> ListadoPagos();
        Task<PagosGenericoViewModel> ObtenerPagos(FiltroPagosViewModel filtros);
    }

    
}