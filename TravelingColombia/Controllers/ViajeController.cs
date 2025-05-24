using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Controllers
{
    public class ViajeController : Controller
    {
        private readonly IRepositoryViaje _viajeRepository;
        private readonly TravelingColombiabdContext _context;
        private readonly IUnitUser _unitUser;

        public ViajeController(IUnitUser unitUser, IRepositoryViaje viajeRepository, TravelingColombiabdContext context)
        {
            _unitUser = unitUser;
            _viajeRepository = viajeRepository;
            _context = context;
        }

        public async Task<IActionResult> Index(FiltroViajesViewModel filtros)
        {
            var model = await _viajeRepository.ObtenerViajesFiltrados(filtros);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FiltroViajesViewModel filtro)
        {
            var viaje = new Viaje
            {
                IdDestinoIda = filtro?.IdDestinoIda?? 0,
                IdDestinoLlegada = filtro?.IdDestinoLlegada??0,
                HoraSalida = filtro.HoraSalida,
                HoraLlegada = filtro.HoraLlegada,
                FechaViaje = filtro.FechaViaje,
                PrecioViaje = filtro.PrecioViaje,
                CantidadPuestos = filtro.CantidadPuestos,
                IdAerolinea = filtro?.IdAerolinea??0,
                Imagen = filtro.Imagen
            };

            await _viajeRepository.Create(viaje);
            await _unitUser.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(int id)
        {
            var viaje = await _viajeRepository.GetByIdAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }

            var filtro = new FiltroViajesViewModel
            {
                IdViaje = viaje.IdViaje,
                IdDestinoIda = viaje.IdDestinoIda,
                IdDestinoLlegada = viaje.IdDestinoLlegada,
                HoraSalida = viaje.HoraSalida,
                HoraLlegada = viaje.HoraLlegada,
                FechaViaje = viaje.FechaViaje,
                PrecioViaje = viaje.PrecioViaje,
                CantidadPuestos = viaje.CantidadPuestos,
                IdAerolinea = viaje.IdAerolinea,
                Imagen = viaje.Imagen
            };

            ViewBag.ListadoDestinos = new SelectList(_context.Destinos.ToList(), "IdDestino", "NombreDestino", viaje.IdDestinoIda);
            ViewBag.ListadoAerolineas = new SelectList(_context.Aerolineas.ToList(), "IdAerolinea", "NombreAerolinea", viaje.IdAerolinea);

            return View(filtro);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(FiltroViajesViewModel filtro)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListadoDestinos = new SelectList(_context.Destinos.ToList(), "IdDestino", "NombreDestino", filtro.IdDestinoIda);
                ViewBag.ListadoAerolineas = new SelectList(_context.Aerolineas.ToList(), "IdAerolinea", "NombreAerolinea", filtro.IdAerolinea);
                return View(filtro);
            }

            var viaje = await _viajeRepository.GetByIdAsync(filtro.IdViaje);
            if (viaje == null)
            {
                return NotFound();
            }

            viaje.IdDestinoIda = filtro.IdDestinoIda;
            viaje.IdDestinoLlegada = filtro.IdDestinoLlegada;
            viaje.HoraSalida = filtro.HoraSalida;
            viaje.HoraLlegada = filtro.HoraLlegada;
            viaje.FechaViaje = filtro.FechaViaje;
            viaje.PrecioViaje = filtro.PrecioViaje;
            viaje.CantidadPuestos = filtro.CantidadPuestos;
            viaje.IdAerolinea = filtro.IdAerolinea;
            viaje.Imagen = filtro.Imagen;

            await _viajeRepository.Update(viaje);
            await _unitUser.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viaje = await _viajeRepository.GetByIdAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }

            await _viajeRepository.DeleteByIdAsync(id);
            await _unitUser.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
