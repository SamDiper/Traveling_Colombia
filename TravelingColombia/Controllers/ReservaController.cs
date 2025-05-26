using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Controllers
{

    public class ReservaController : Controller
    {
        private readonly IRepositoryReserva _repositoryReserva;
        private readonly IRepositoryPlan _RepositoryPlan;
        private readonly IUnitUser _unidadadtrabajo;

        public ReservaController(IRepositoryReserva repositoryReserva, IUnitUser unitUser, IRepositoryPlan RepositoryPlan)
        {
            _repositoryReserva = repositoryReserva;
            _unidadadtrabajo = unitUser;
            _RepositoryPlan=RepositoryPlan;
        }

        public async Task<IActionResult> Index(FiltroReservasViewModel filtros)
        {
            var reservas = await _repositoryReserva.ObtenerReservasFiltrados(filtros);
            return View(reservas);
        }
        public async Task<IActionResult> FormularioUsuario(int id)
        {
            var lista = await _RepositoryPlan.ObtenerPlan(id);

            return View(lista);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var reserva = await _repositoryReserva.GetByIdAsync(id);
            if (reserva == null)
                return NotFound();

            var viewModel = new FiltroReservasViewModel
            {
                IdReserva = reserva.IdReserva,
                IdUsuario = reserva.IdUsuario,
                IdViaje = reserva.IdViaje,
                IdPlan = reserva.IdPlan,
                IdEstadoReserva = reserva.IdEstadoReserva,
                FechaReserva = reserva.FechaReserva
            };

            // ViewBag usados en el formulario para selects
            ViewBag.Usuarios = new SelectList(await _repositoryReserva.ListaUsuarios(), "IdUsuario", "NombreUsuario", reserva.IdUsuario);
            ViewBag.Planes = new SelectList(await _repositoryReserva.ListaPlanes(), "IdPlan", "NombrePlan", reserva.IdPlan);
            ViewBag.Viajes = new SelectList(await _repositoryReserva.ListaViajes(), "IdViaje", "IdViaje", reserva.IdViaje);
            ViewBag.Estados = new SelectList(await _repositoryReserva.ListaEstados(), "IdEstado", "Estado1", reserva.IdEstadoReserva);

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(FiltroReservasViewModel filtro)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(await _repositoryReserva.ListaUsuarios(), "IdUsuario", "NombreUsuario", filtro.IdUsuario);
                ViewBag.Planes = new SelectList(await _repositoryReserva.ListaPlanes(), "IdPlan", "NombrePlan", filtro.IdPlan);
                ViewBag.Viajes = new SelectList(await _repositoryReserva.ListaViajes(), "IdViaje", "IdViaje", filtro.IdViaje);
                ViewBag.Estados = new SelectList(await _repositoryReserva.ListaEstados(), "IdEstado", "Estado1", filtro.IdEstadoReserva);

                return View(filtro);
            }

            var reserva = await _repositoryReserva.GetByIdAsync(filtro.IdReserva);
            if (reserva == null)
                return NotFound();

            reserva.IdUsuario = filtro.IdUsuario;
            reserva.IdViaje = filtro.IdViaje;
            reserva.IdPlan = filtro.IdPlan;
            reserva.IdEstadoReserva = filtro.IdEstadoReserva;
            reserva.FechaReserva = filtro.FechaReserva;

            await _repositoryReserva.Update(reserva);
            await _unidadadtrabajo.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Crear(FiltroReservasViewModel filtro)
        {
            // Validación básica antes de usar los valores
            if (filtro.IdViaje == 0)
                filtro.IdViaje = null;

            if (filtro.IdPlan == 0)
                filtro.IdPlan = null;

            var reserva = new Reserva
            {
                IdUsuario = filtro.IdUsuario,
                IdViaje = filtro.IdViaje,
                IdPlan = filtro.IdPlan,
                IdEstadoReserva = filtro.IdEstadoReserva,
                FechaReserva = filtro.FechaReserva
            };

            await _repositoryReserva.Create(reserva);
            await _unidadadtrabajo.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viaje = await _repositoryReserva.GetByIdAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }

            await _repositoryReserva.DeleteByIdAsync(id);
            await _unidadadtrabajo.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}