using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
namespace TravelingColombia.Controllers
{

    public class ClienteController : Controller
    {

        private readonly IRepositoryPago _repositoryPago;
        private readonly IRepositoryUsuario _RepositoryUsuario;

        public ClienteController(IRepositoryPago repositoryPago, IRepositoryUsuario RepositoryUsuario)
        {
            _repositoryPago = repositoryPago;
            _RepositoryUsuario = RepositoryUsuario;
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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            var usuarioFiltrado =  await _RepositoryUsuario.BuscarUsuario(usuario);

            if (usuarioFiltrado != null)
            {
                
                var Claims = new List<Claim>{
                    new Claim(ClaimTypes.Email,usuarioFiltrado.EmailUsuario),
                    new Claim(ClaimTypes.Role,usuarioFiltrado.Rol),   
                };

                var claimsIdentity= new ClaimsIdentity(Claims,CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}