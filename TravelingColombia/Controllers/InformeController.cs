using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelingColombia.Repository.Interface;

namespace TravelingColombia.Controllers
{
    
    public class InformeController : Controller
    {
        private readonly IRepositoryViaje _repositoryViaje;
        private readonly IRepositoryReserva _repositoryReserva;
        private readonly IRepositoryPlan _repositoryPlan;



        public InformeController(IRepositoryViaje repositoryViaje, IRepositoryReserva repositoryReserva, IRepositoryPlan repositoryPlan)
        {
            _repositoryViaje = repositoryViaje;
            _repositoryReserva = repositoryReserva;
            _repositoryPlan = repositoryPlan;
        }
        public async Task<IActionResult> Viaje()
        {
            var viajes = await _repositoryViaje.GetIncludesAsync();
            return View(viajes);
        }
        public async Task<IActionResult> Plan()
        {
            var planes = await _repositoryPlan.GetIncludesAsync();
            return View(planes);
        }
        public async Task<IActionResult> Reserva()
        {
            var reservas = await _repositoryReserva.GetIncludesAsync();
            return View(reservas);
        }
        public IActionResult Pago()
        {
            return View();
        }

       
    }
}