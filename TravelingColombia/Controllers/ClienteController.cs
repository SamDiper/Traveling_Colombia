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
using Microsoft.EntityFrameworkCore.Storage;
using TravelingColombia.ViewModels;
using TravelingColombia.Filtros;
using TravelingColombia.ViewModels;
namespace TravelingColombia.Controllers
{

    public class ClienteController : Controller
    {

        private readonly IRepositoryPago _repositoryPago;
        private readonly IRepositoryUsuario _RepositoryUsuario;
        private readonly IRepositoryPlan _RepositoryPlan;
        private readonly IRepositoryReserva _repositoryReserva;
        private readonly IRepositoryViaje _repositoryViaje;
        private readonly TravelingColombiabdContext _context;

        public ClienteController(IRepositoryViaje repositoryViaje,IRepositoryReserva repositoryReserva,IRepositoryPlan RepositoryPlan, IRepositoryPago repositoryPago, IRepositoryUsuario RepositoryUsuario, TravelingColombiabdContext context)
        {
            _repositoryPago = repositoryPago;
            _RepositoryUsuario = RepositoryUsuario;
            _RepositoryPlan = RepositoryPlan;
            _repositoryReserva= repositoryReserva;
            _repositoryViaje= repositoryViaje;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var Roles = new UsuarioViewModel
            {
                ListaRoles = _context.Rols.ToList(),
            };
            return View(Roles);
        }
        
        public async Task<IActionResult> Pagos(PlanViewModel plan)
        {
            int idUsuario = int.Parse(User.FindFirst("IdUsuario")?.Value ?? "0");
            Usuario usuario = new Usuario
            {
                IdUsuario = idUsuario,
            };
            vistaPagoViewModel procesoPago = new vistaPagoViewModel
            {
                Plan = await _RepositoryPlan.ObtenerPlan(plan.IdPlan),
                Usuario = await _RepositoryUsuario.BuscarUsuario(usuario)
            };
            procesoPago.Plan.CantidadPersonas=plan.CantidadPersonas;
            procesoPago.Plan.PrecioPlan=plan.PrecioPlan;
            var listaBancos = await _repositoryPago.ListaBancos();
            var ListaMetodoPago = await _repositoryPago.ListaMetodosPagos();
            ViewBag.ListaBancos = new SelectList(listaBancos, "IdBanco", "NombreBanco");
            ViewBag.MetodosPago = new SelectList(ListaMetodoPago, "IdMetodo", "MetodoPago1");
            return View(procesoPago);
        }
        public async Task<IActionResult> PagosViajes(ViajesViewModel viaje)
        {
            int idUsuario = int.Parse(User.FindFirst("IdUsuario")?.Value ?? "0");
            Usuario usuario = new Usuario
            {
                IdUsuario = idUsuario,
            };
            vistaPagoViewModel procesoPago = new vistaPagoViewModel
            {
                viaje = await _repositoryViaje.ObtenerViaje(viaje.IdViaje),
                Usuario = await _RepositoryUsuario.BuscarUsuario(usuario)
            };
            procesoPago.viaje.CantidadPersonas=viaje.CantidadPersonas;
            procesoPago.viaje.PrecioViaje=viaje.PrecioViaje;
            var listaBancos = await _repositoryPago.ListaBancos();
            var ListaMetodoPago = await _repositoryPago.ListaMetodosPagos();
            ViewBag.ListaBancos = new SelectList(listaBancos, "IdBanco", "NombreBanco");
            ViewBag.MetodosPago = new SelectList(ListaMetodoPago, "IdMetodo", "MetodoPago1");
            return View(procesoPago);
        }
        public async Task<IActionResult> Pago(int id)
        {
            int idUsuario = int.Parse(User.FindFirst("IdUsuario")?.Value ?? "0");
            Usuario usuario = new Usuario
            {
                IdUsuario = idUsuario,
            };
            vistaPagoViewModel procesoPago = new vistaPagoViewModel
            {
                Usuario = await _RepositoryUsuario.BuscarUsuario(usuario),
                reserva= await _repositoryReserva.GetByIdAsync(id)
            };
            
            var listaBancos = await _repositoryPago.ListaBancos();
            var ListaMetodoPago = await _repositoryPago.ListaMetodosPagos();
            ViewBag.ListaBancos = new SelectList(listaBancos, "IdBanco", "NombreBanco");
            ViewBag.MetodosPago = new SelectList(ListaMetodoPago, "IdMetodo", "MetodoPago1");
            return View(procesoPago);
        }
        


        public async Task<IActionResult> Perfil()
        {
            int idUsuario = int.Parse(User.FindFirst("IdUsuario")?.Value ?? "0");
            Usuario usuario = new Usuario
            {
                IdUsuario = idUsuario,
            };
            var usuarioFiltrado =  await _RepositoryUsuario.BuscarUsuario(usuario);
            return View(usuarioFiltrado);
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
                    new Claim("Nombre",usuarioFiltrado.NombreUsuario),
                    new Claim("Apellido",usuarioFiltrado.ApellidoUsuario),
                    new Claim("IdUsuario", usuarioFiltrado.IdUsuario.ToString()),
                    new Claim("FotoUsuario", usuarioFiltrado.FotoUsuario ?? "https://static.vecteezy.com/system/resources/previews/005/544/718/non_2x/profile-icon-design-free-vector.jpg"),
                };

                var claimsIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

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