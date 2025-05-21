using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Implementacion;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Implementacion
{
    public class RepositoryPlan : RepositoryGeneric<Plane, int>, IRepositoryPlan
    {
        private readonly TravelingColombiabdContext _context;
        public RepositoryPlan(TravelingColombiabdContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PlanesViewModel> listadoPlanes()
        {
            var planes = new PlanesViewModel();
            var resultado = await (from p in _context.Planes
                                   join d in _context.Destinos on p.IdDestinoIda equals d.IdDestino
                                   join tp in _context.TipoPlans on p.IdTipoPlan equals tp.IdTipoPlan
                                   join h in _context.Hoteles on p.IdHotel equals h.IdHotel
                                   join a in _context.Aerolineas on p.IdAerolinea equals a.IdAerolinea
                                   select new PlanViewModel
                                   {
                                       NombrePlan = p.NombrePlan,
                                       DestinoIda = d.NombreDestino,
                                       Pais = d.Pais,
                                       FechaIda = p.FechaIda,
                                       FechaRegreso = p.FechaRegreso,
                                       TipoPlan = tp.NombrePlan,
                                       Descripcion = p.Descripcion,
                                       CantidadPersonas = p.CantidadPersonas,
                                       Imagen = p.Imagen,
                                       Hotel = h.NombreHotel,
                                       Aerolinea = a.NombreAerolinea,
                                       PrecioPlan = p.PrecioPlan,
                                   }).ToListAsync();

            planes.ListadoPlanes = resultado;
            planes.ListadoTipoPlanes = await _context.TipoPlans.ToListAsync();

            return planes;

        }

        public async Task<PlanesViewModel> ObtenerPlanesFiltrados(FiltroPlanesViewModel filtros)
        {
            var query = from p in _context.Planes
                        join d in _context.Destinos on p.IdDestinoIda equals d.IdDestino
                        join tp in _context.TipoPlans on p.IdTipoPlan equals tp.IdTipoPlan
                        join h in _context.Hoteles on p.IdHotel equals h.IdHotel
                        join a in _context.Aerolineas on p.IdAerolinea equals a.IdAerolinea
                        select new PlanViewModel
                        {
                            IdPlan = p.IdPlan,
                            NombrePlan = p.NombrePlan,
                            DestinoIda = d.NombreDestino,
                            Pais = d.Pais,
                            FechaIda = p.FechaIda,
                            FechaRegreso = p.FechaRegreso,
                            TipoPlan = tp.NombrePlan,
                            Descripcion = p.Descripcion,
                            CantidadPersonas = p.CantidadPersonas,
                            Imagen = p.Imagen,
                            Hotel = h.NombreHotel,
                            Aerolinea = a.NombreAerolinea,
                            PrecioPlan = p.PrecioPlan
                        };

            
            if (!string.IsNullOrWhiteSpace(filtros.NombreDestino))
                query = query.Where(p => p.DestinoIda.Contains(filtros.NombreDestino));
                
            if (!string.IsNullOrWhiteSpace(filtros.NombrePlan))
                query = query.Where(p => p.NombrePlan.Contains(filtros.NombrePlan));

            if (filtros.IdTipoPlan.HasValue)
            {
                var tipoPlanNombre = await _context.TipoPlans
                                        .Where(tp => tp.IdTipoPlan == filtros.IdTipoPlan.Value)
                                        .Select(tp => tp.NombrePlan)
                                        .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(tipoPlanNombre))
                {
                    query = query.Where(p => p.TipoPlan == tipoPlanNombre);
                }
            }

            if (filtros.FechaMin.HasValue)
                query = query.Where(p => p.FechaIda >= filtros.FechaMin.Value);

            if (filtros.FechaMax.HasValue)
                query = query.Where(p => p.FechaIda <= filtros.FechaMax.Value);

            if (filtros.PrecioMin.HasValue)
                query = query.Where(p => p.PrecioPlan >= filtros.PrecioMin.Value);

            if (filtros.PrecioMax.HasValue)
                query = query.Where(p => p.PrecioPlan <= filtros.PrecioMax.Value);

            var planes = new PlanesViewModel
            {
                ListadoPlanes = query.ToList(),
                ListadoTipoPlanes = await _context.TipoPlans.ToListAsync()
            };

            return planes;
        }
    }
}