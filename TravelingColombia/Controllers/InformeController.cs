using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelingColombia.Filtros;
using TravelingColombia.Repository.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Controllers
{

    public class InformeController : Controller
    {
        private readonly IRepositoryViaje _repositoryViaje;
        private readonly IRepositoryReserva _repositoryReserva;
        private readonly IRepositoryPlan _repositoryPlan;
        private readonly IRepositoryPago _repositoryPago;



        public InformeController(IRepositoryViaje repositoryViaje, IRepositoryReserva repositoryReserva, IRepositoryPlan repositoryPlan, IRepositoryPago repositoryPago)
        {
            _repositoryViaje = repositoryViaje;
            _repositoryReserva = repositoryReserva;
            _repositoryPlan = repositoryPlan;
            _repositoryPago = repositoryPago;
        }
        public async Task<IActionResult> Viaje()
        {
            var viajes = await _repositoryViaje.GetIncludesAsync();

            var viewModel = new ViajesViewModel
            {
                ListadoViajes = viajes.ToList(),
                ListadoAerolineas = viajes.Select(v => v.IdAerolineaNavigation).Distinct().ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Viaje(FiltroViajesViewModel filtroViajes)
        {
            var resultado = await _repositoryViaje.ObtenerViajesFiltradosAsync(filtroViajes);
            return View(resultado);
        }
        public async Task<IActionResult> Plan()
        {
            var planes = await _repositoryPlan.listadoPlanes();
            return View(planes);
        }

        [HttpPost]
        public async Task<IActionResult> Plan(FiltroPlanesViewModel filtros)
        {
            var resultado = await _repositoryPlan.ObtenerPlanesFiltrados(filtros);
            return View(resultado);
        }
        public async Task<IActionResult> Reserva()
        {
            var reservas = await _repositoryReserva.GetIncludesAsync();
            return View(reservas);
        }
        public async Task<IActionResult> Pago()
        {
            var listaPagos = await _repositoryPago.ListadoPagos();
            return View(listaPagos);
        }

        [HttpPost]
        public async Task<IActionResult> Pago(string filtro = null)
        {
            var listaPagos = await _repositoryPago.ObtenerPagos(filtro);

            return View(listaPagos);
        }


    }
}