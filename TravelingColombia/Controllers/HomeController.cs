using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly TravelingColombiabdContext _context;
    private readonly IRepositorioFactura _RepositoryFacturas;

    public HomeController(TravelingColombiabdContext context, IRepositorioFactura RepositoryFacturas, IRepositoryViaje repositoryViaje, IRepositoryPlan repositoryPlan)
    {
        _repositoryPlan = repositoryPlan;
        _repositoryViaje = repositoryViaje;
        _RepositoryFacturas = RepositoryFacturas;
        _context = context;
    }
    [HttpGet]
    public IActionResult Index(
        // Filtros de viajes
        int? IdDestinoIda, int? IdDestinoLlegada, DateTime? FechaViaje, TimeSpan? HoraSalida,
        int? IdAerolinea, decimal? PrecioViaje,

        // Filtros de planes
        int? IdDestinoPlan, string NombrePlan, int? IdTipoPlan, DateTime? FechaPlan, decimal? PrecioPlan)
    {
        // ----- VIAJES -----
        var viajes = _context.Viajes
            .Include(v => v.IdDestinoIdaNavigation)
            .Include(v => v.IdDestinoLlegadaNavigation)
            .Include(v => v.IdAerolineaNavigation)
            .AsQueryable();

        if (IdDestinoIda.HasValue)
            viajes = viajes.Where(v => v.IdDestinoIda == IdDestinoIda);
        if (IdDestinoLlegada.HasValue)
            viajes = viajes.Where(v => v.IdDestinoLlegada == IdDestinoLlegada);
        if (FechaViaje.HasValue)
        {
            var fechaFiltro = DateOnly.FromDateTime(FechaViaje.Value);
            viajes = viajes.Where(v => v.FechaViaje == fechaFiltro);
        }
        if (HoraSalida.HasValue)
        {
            var horaFiltro = TimeOnly.FromTimeSpan(HoraSalida.Value);
            viajes = viajes.Where(v => v.HoraSalida == horaFiltro);
        }
        if (IdAerolinea.HasValue)
            viajes = viajes.Where(v => v.IdAerolinea == IdAerolinea);
        if (PrecioViaje.HasValue)
            viajes = viajes.Where(v => v.PrecioViaje <= PrecioViaje);

        // ----- PLANES -----
        var planes = _context.Planes
            .Include(p => p.IdDestinoIdaNavigation)
            .Include(p => p.IdTipoPlanNavigation)
            .AsQueryable();

        if (IdDestinoPlan.HasValue)
            planes = planes.Where(p => p.IdDestinoIda == IdDestinoPlan);
        if (!string.IsNullOrEmpty(NombrePlan))
            planes = planes.Where(p => p.NombrePlan.Contains(NombrePlan));
        if (IdTipoPlan.HasValue)
            planes = planes.Where(p => p.IdTipoPlan == IdTipoPlan);
        if (FechaPlan.HasValue)
        {
            var fechaPlanDateOnly = DateOnly.FromDateTime(FechaPlan.Value);
            planes = planes.Where(p => p.FechaIda == fechaPlanDateOnly);
        }
        if (PrecioPlan.HasValue)
            planes = planes.Where(p => p.PrecioPlan <= PrecioPlan);

        // Cargar listas complementarias para los formularios
        var model = new VistaIndex
        {
            ListaViajes = new ListaViajes
            {
                ListadoViajes = viajes.ToList(),
                ListadoDestino = _context.Destinos.ToList(),
                ListadoAerolinea = _context.Aerolineas.ToList()
            },
            ListaPlanes = new ListaPlanes
            {
                ListadoPlanes = planes.ToList(),
                ListadoDestino = _context.Destinos.ToList(),
                ListadoTipoPlanes = _context.TipoPlans.ToList()
            }
        };

        return View("Index", model);
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
