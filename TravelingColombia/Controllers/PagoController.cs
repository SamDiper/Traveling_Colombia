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

namespace TravelingColombia.Controllers
{

    public class PagoController : Controller
    {
        private readonly IRepositoryPago _repositoryPago;
        private readonly IUnitUser _unidadadtrabajo;
        public PagoController(IRepositoryPago repositoryPago, IUnitUser unidadadtrabajo)
        {
            _repositoryPago = repositoryPago;
            _unidadadtrabajo = unidadadtrabajo;
        }
        public async Task<IActionResult> Index()
        {
            var listaPagos = await _repositoryPago.ListadoPagos();
            return View(listaPagos);
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var PagoFiltro = await _repositoryPago.GetByIdAsync(id);
            FiltroPagosViewModel pago = new FiltroPagosViewModel
            {
                IdPago = PagoFiltro.IdPago,
                Nombre = PagoFiltro?.Nombre,
                Cedula = PagoFiltro?.Cedula,
                IdBanco = PagoFiltro?.IdBanco,
                Cuenta = PagoFiltro?.Cuenta,
                Monto = PagoFiltro?.Monto,
                IdMetodo = PagoFiltro?.IdMetodo
            };

            var listaBancos = await _repositoryPago.ListaBancos();
            var ListaMetodoPago = await _repositoryPago.ListaMetodosPagos();
            ViewBag.ListaBancos = new SelectList(listaBancos, "IdBanco", "NombreBanco", PagoFiltro?.IdPago);
            ViewBag.MetodosPago = new SelectList(ListaMetodoPago, "IdMetodo", "MetodoPago1", PagoFiltro?.IdMetodo);
            return View(pago);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(FiltroPagosViewModel filtro)
        {
            var PagoFiltro = await _repositoryPago.GetByIdAsync(filtro.IdPago);

            PagoFiltro.IdPago = filtro.IdPago;
            PagoFiltro.Nombre = filtro.Nombre;
            PagoFiltro.Cedula = filtro.Cedula;
            PagoFiltro.IdBanco = filtro.IdBanco ?? 0;
            PagoFiltro.Cuenta = filtro.Cuenta;
            PagoFiltro.Monto = filtro.Monto ?? 0;
            PagoFiltro.IdMetodo = filtro.IdMetodo ?? 0;

            await _repositoryPago.Update(PagoFiltro);
            await _unidadadtrabajo.SaveChangesAsync();

            return RedirectToAction("Index", "Pago");
        }
         [HttpPost]
        public async Task<IActionResult> Crear(FiltroPagosViewModel filtro)
        {
             Pago pago = new Pago
            {
                
                Nombre = filtro?.Nombre,
                Cedula = filtro?.Cedula,
                IdBanco = filtro?.IdBanco??0,
                Cuenta = filtro?.Cuenta,
                Monto = filtro?.Monto??0,
                IdMetodo = filtro?.IdMetodo??0
            };

             await _repositoryPago.Create(pago);
             await _unidadadtrabajo.SaveChangesAsync();
            
            return RedirectToAction("Index","Pago");
        }


    }
}