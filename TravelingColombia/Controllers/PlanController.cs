using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Repository.Interface;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Controllers
{

    [Authorize(Roles = "Administrador,Planificador")]
    public class PlanController : Controller
    {

        private readonly IRepositoryPlan _repositoryPlan;
        private readonly IUnitUser _unitUser;
        private readonly IRepositoryGeneric<Hotele, int> _repositoryHoteles;
        private readonly IRepositoryGeneric<Aerolinea, int> _repositoryAerolineas;
        private readonly IRepositoryGeneric<Destino, int> _repositoryDestinos;
        private readonly IRepositoryGeneric<TipoPlan, int> _repositoryTipoPlanes;
        public PlanController(IRepositoryPlan repositoryPlan, IUnitUser unitUser, IRepositoryGeneric<Hotele, int> repositoryHoteles, IRepositoryGeneric<Aerolinea, int> repositoryAerolineas, IRepositoryGeneric<Destino, int> repositoryDestinos, IRepositoryGeneric<TipoPlan, int> repositoryTipoPlanes)
        {
            _repositoryPlan = repositoryPlan;
            _unitUser = unitUser;
            _repositoryAerolineas = repositoryAerolineas;
            _repositoryDestinos = repositoryDestinos;
            _repositoryHoteles = repositoryHoteles;
            _repositoryTipoPlanes = repositoryTipoPlanes;
        }
        public async Task<IActionResult> Index()
        {
            var listadoplanes = await _repositoryPlan.listadoPlanes();
            return View(listadoplanes);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FiltroPlanesViewModel filtros)
        {
            var resultado = await _repositoryPlan.ObtenerPlanesFiltrados(filtros);
            return View(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var planFiltrado = await _repositoryPlan.GetByIdAsync(id);



            var ListadoHoteles = await _repositoryHoteles.GetAllAsync();
            var ListadoAerolinea = await _repositoryAerolineas.GetAllAsync();
            var ListadoDestino = await _repositoryDestinos.GetAllAsync();
            var ListadoTipoPlanes = await _repositoryTipoPlanes.GetAllAsync();
            ViewBag.ListadoHoteles = new SelectList(ListadoHoteles, "IdHotel", "NombreHotel", planFiltrado?.IdHotel);
            ViewBag.ListadoAerolinea = new SelectList(ListadoAerolinea, "IdAerolinea", "NombreAerolinea", planFiltrado?.IdAerolinea);
            ViewBag.ListadoDestino = new SelectList(ListadoDestino, "IdDestino", "NombreDestino", planFiltrado?.IdDestinoIda);
            ViewBag.ListadoTipoPlanes = new SelectList(ListadoTipoPlanes, "IdTipoPlan", "NombrePlan", planFiltrado?.IdTipoPlan);
            return View(planFiltrado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Plane plan)
        {

            var planFiltrado = await _repositoryPlan.GetByIdAsync(plan.IdPlan);
            planFiltrado.IdPlan = plan.IdPlan;
            planFiltrado.IdDestinoIda = plan.IdDestinoIda;
            planFiltrado.NombrePlan = plan.NombrePlan;
            planFiltrado.FechaIda = plan.FechaIda;
            planFiltrado.FechaRegreso = plan.FechaRegreso;
            planFiltrado.IdTipoPlan = plan.IdTipoPlan;
            planFiltrado.Descripcion = plan.Descripcion;
            planFiltrado.CantidadPersonas = plan.CantidadPersonas;
            planFiltrado.Imagen = "imagen.jpg";
            planFiltrado.PrecioPlan = plan.PrecioPlan;
            planFiltrado.IdHotel = plan.IdHotel;
            planFiltrado.IdAerolinea = plan.IdAerolinea;
            planFiltrado.Introduccion = plan.Introduccion;

            await _repositoryPlan.Update(planFiltrado);
            await _unitUser.SaveChangesAsync();



            return RedirectToAction("Index", "Plan");
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Plane plan)
        {

            await _repositoryPlan.Create(plan);
            await _unitUser.SaveChangesAsync();



            return RedirectToAction("Index", "Plan");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var plan =await _repositoryPlan.GetByIdAsync(id);

            await _repositoryPlan.DeleteByIdAsync(plan.IdPlan);
            await _unitUser.SaveChangesAsync();



            return RedirectToAction("Index", "Plan");
        }


    }
}