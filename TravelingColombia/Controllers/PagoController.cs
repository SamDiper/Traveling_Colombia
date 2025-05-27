using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    
    public class PagoController : Controller
    {
        private readonly IRepositoryPago _repositoryPago;
        private readonly IUnitUser _unidadadtrabajo;
        private readonly IUnidadTransaccion _unidadTransaccion;
        public PagoController(IUnidadTransaccion unidadTransaccion, IRepositoryPago repositoryPago, IUnitUser unidadadtrabajo)
        {
            _repositoryPago = repositoryPago;
            _unidadadtrabajo = unidadadtrabajo;
            _unidadTransaccion = unidadTransaccion;
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
                Monto = PagoFiltro?.Monto,
                IdMetodo = PagoFiltro?.IdMetodo
            };

            var listaBancos = await _repositoryPago.ListaBancos();
            var ListaMetodoPago = await _repositoryPago.ListaMetodosPagos();
            ViewBag.ListaBancos = new SelectList(listaBancos, "IdBanco", "NombreBanco", PagoFiltro?.IdBanco);
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
                IdBanco = filtro?.IdBanco ?? 0,
                Monto = filtro?.Monto ?? 0,
                IdMetodo = filtro?.IdMetodo ?? 0
            };

            await _repositoryPago.Create(pago);
            await _unidadadtrabajo.SaveChangesAsync();

            return RedirectToAction("Index", "Pago");
        }








        
        public async Task<IActionResult> RegistrarReservaYPago(TransaccionPagoViewModels transaccionReservaPago)
        {

            using (var transaction = await _unidadTransaccion.BeginTransactionAsync())
            {
                try
                {
                    int idUsuario = int.Parse(User.FindFirst("IdUsuario")?.Value ?? "0");
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = idUsuario,
                    };

                    var PlanFiltrado = await _unidadTransaccion.repositoryPlan.ObtenerPlan(transaccionReservaPago.IdPlan);
                    var usuarioFiltrado = await _unidadTransaccion.repositoryUsuario.BuscarUsuario(usuario);

                    //Crear Reserva
                    var reserva = new Reserva
                    {
                        FechaReserva = DateOnly.FromDateTime(DateTime.Now),
                        IdViaje = 1,
                        IdPlan = PlanFiltrado.IdPlan,
                        IdEstadoReserva=1,
                        IdUsuario = usuarioFiltrado.IdUsuario,
                        CantidadPersonas = transaccionReservaPago.CantidadPersonas,
                        TotalReserva = transaccionReservaPago.PrecioPlan
                    };
                    await _unidadTransaccion.repositoryReserva.Create(reserva);

                    // Crear Pago
                    var pago = new Pago
                    {
                        Nombre = usuarioFiltrado.NombreUsuario + " " + usuarioFiltrado.ApellidoUsuario,
                        Cedula= usuarioFiltrado.CedulaUsuario, 
                        IdBanco = transaccionReservaPago.IdBanco,
                        Monto = transaccionReservaPago.Monto,
                        IdMetodo = transaccionReservaPago.IdMetodo,
                    
                    };
                    await _unidadTransaccion.repositoryPago.Create(pago);

                    

                    // 4. Guardar cambios en una sola transacción
                    await _unidadTransaccion.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Ocurrió un error al registrar la reserva y el pago.");
                    return RedirectToAction("Index", "Home");
                }
            }


        }

    }
}