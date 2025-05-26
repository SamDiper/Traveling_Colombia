using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TravelingColombia.Repository.Interface;

namespace TravelingColombia.Controllers
{

    public class ClienteController : Controller
    {

        private readonly IRepositoryPago _repositoryPago;

        public ClienteController(IRepositoryPago repositoryPago)
        {
            _repositoryPago = repositoryPago;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Pagos()
        {
            var listaBancos = await _repositoryPago.ListaBancos();
            var ListaMetodoPago = await _repositoryPago.ListaMetodosPagos();
            ViewBag.ListaBancos = new SelectList(listaBancos, "IdBanco", "NombreBanco");
            ViewBag.MetodosPago = new SelectList(ListaMetodoPago, "IdMetodo", "MetodoPago1");
            return View();
        }


        public IActionResult Perfil()
        {
            return View();
        }


    }
}