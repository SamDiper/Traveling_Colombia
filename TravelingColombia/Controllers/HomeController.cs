using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Implementacion;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Controllers;

public class HomeController : Controller
{


    private readonly IRepositoryViaje _repositoryViaje;
    private readonly IRepositoryPlan _repositoryPlan;

    private readonly IRepositorioFactura _RepositoryFacturas;

    public HomeController(IRepositorioFactura RepositoryFacturas, IRepositoryViaje repositoryViaje, IRepositoryPlan repositoryPlan)
    {
        _repositoryPlan = repositoryPlan;
        _repositoryViaje = repositoryViaje;
        _RepositoryFacturas= RepositoryFacturas;
    }
    public async Task<IActionResult> Index()
    {

        LayoutViewModel listas = new LayoutViewModel
        {
            listaPlanes = await _repositoryPlan.listadoPlanes(),
            ListaViajes = await _repositoryViaje.ListadoViajes()
        };
        return View(listas);
    }

    public async Task<IActionResult> Privacy(int id)
{
    var factura = await _RepositoryFacturas.FacturaViewModel(id);
    
    return View(factura);
}
[HttpGet]
    public async Task<IActionResult> DetallesPlan(int id)
    {
        var plan = await _repositoryPlan.ObtenerPlan(id);
        return View(plan);
    }

    public async Task<IActionResult> DetallesViaje(int id)
    {
        var viaje = await _repositoryViaje.ObtenerViaje(id);
        return View(viaje);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
