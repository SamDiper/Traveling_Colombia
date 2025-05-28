using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Interface;
using TravelingColombia.Models;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Interface
{
    public interface IRepositorioFactura : IRepositoryGeneric<Factura, int>
    {
        Task<FacturaVIewModel> EnviarFacturaHtmlAsync(string correoDestino, int idFactura);
        Task<FacturaVIewModel> FacturaViewModel(int idFactura);
    }
}