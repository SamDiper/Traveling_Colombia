using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Controllers;

public class HomeController : Controller
{


    private readonly IRepositoryViaje _repositoryViaje;
    private readonly IRepositoryPlan _repositoryPlan;

    public HomeController(IRepositoryViaje repositoryViaje, IRepositoryPlan repositoryPlan)
    {
        _repositoryPlan = repositoryPlan;
        _repositoryViaje = repositoryViaje;
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

    public IActionResult Privacy()
    {
        return View();
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
