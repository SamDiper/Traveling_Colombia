using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Implementacion;
using TravelingColombia.UnitOfWork.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Controllers
{

    public class ViajeController : Controller
    {

        private readonly IRepositoryViaje _viajeRepository;
        private readonly TravelingColombiabdContext _context;
        private readonly IUnitUser _unitUser;
        public ViajeController(IUnitUser unitUser, IRepositoryViaje repositoryViaje, TravelingColombiabdContext context)
        {
            _viajeRepository = repositoryViaje;
            _context = context;
            _unitUser = unitUser;
        }

        public async Task<IActionResult> Index()
        {
            var resultado = await _viajeRepository.GetIncludesAsync();
            return View(resultado);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _viajeRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Viaje viaje)
        {
            if (!ModelState.IsValid)
            {
                return View(viaje);
            }
            await _viajeRepository.Create(viaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(int id)
        {
            var viaje = await _viajeRepository.GetByIdAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }

            var destinos = _context.Destinos.ToList();
            var aerolineas = _context.Aerolineas.ToList();

            var viewModel = new ViajesViewModel
            {
                Viaje = viaje,
                ListadoDestinos = destinos,
                ListadoAerolineas = aerolineas
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ViajesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ListadoDestinos = _context.Destinos.ToList();
                viewModel.ListadoAerolineas = _context.Aerolineas.ToList();

                return View(viewModel);
            }
            var viaje = await _viajeRepository.GetByIdAsync(viewModel.Viaje.IdViaje);
            viaje.IdViaje = viewModel.Viaje.IdViaje;
            viaje.IdDestinoIda = viewModel.Viaje.IdDestinoIda;
            viaje.IdDestinoLlegada = viewModel.Viaje.IdDestinoLlegada;
            viaje.HoraSalida = viewModel.Viaje.HoraSalida;
            viaje.HoraLlegada = viewModel.Viaje.HoraLlegada;
            viaje.FechaViaje = viewModel.Viaje.FechaViaje;
            viaje.PrecioViaje = viewModel.Viaje.PrecioViaje;
            viaje.CantidadPuestos = viewModel.Viaje.CantidadPuestos;
            viaje.IdAerolinea = viewModel.Viaje.IdAerolinea;
            viaje.Imagen = viewModel.Viaje.Imagen;

            // Actualiza el viaje en la base de datos
            await _viajeRepository.Update(viaje);
            await _unitUser.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _viajeRepository.GetByIdAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            await _viajeRepository.DeleteByIdAsync(id);
            await _unitUser.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}