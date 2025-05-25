using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Implementacion;
using Repository.Interface;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Implementacion
{
    public class RepositoryViaje : RepositoryGeneric<Viaje, int>, IRepositoryViaje
    {
        private readonly TravelingColombiabdContext _dbcontext;
        private readonly IRepositoryGeneric<Aerolinea, int> _ListaAerolinea;
        private readonly IRepositoryGeneric<Destino, int> _ListaDestino;

        public RepositoryViaje(TravelingColombiabdContext context, IRepositoryGeneric<Aerolinea, int> listaAerolinea, IRepositoryGeneric<Destino, int> listaDestino) : base(context)
        {
            _dbcontext = context;
            _ListaAerolinea = listaAerolinea;
            _ListaDestino = listaDestino;
        }

        public async Task<List<Aerolinea>> ListaAerolineas()
        {
            return await _ListaAerolinea.GetAllAsync();
        }

        public async Task<List<Destino>> ListaDestinos()
        {
            return await _ListaDestino.GetAllAsync();
        }

        public async Task<ViajesGenericoViewModel> ListadoViajes()
        {
            var lista = await (
            from v in _dbcontext.Viajes
            join a in _dbcontext.Aerolineas on v.IdAerolinea equals a.IdAerolinea
            join dIda in _dbcontext.Destinos on v.IdDestinoIda equals dIda.IdDestino
            join dLlegada in _dbcontext.Destinos on v.IdDestinoLlegada equals dLlegada.IdDestino
            select new ViajesViewModel
            {
                IdViaje = v.IdViaje,
                DestinoIda = dIda.NombreDestino,
                DestinoLlegada = dLlegada.NombreDestino,
                HoraSalida = v.HoraSalida,
                HoraLlegada = v.HoraLlegada,
                FechaViaje = v.FechaViaje,
                PrecioViaje = v.PrecioViaje,
                CantidadPuestos = v.CantidadPuestos,
                AerolineaNombre = a.NombreAerolinea,
                Imagen = v.Imagen
            }).ToListAsync();

            var model = new ViajesGenericoViewModel
            {
                ListadoViajes = lista,
                ListadoAerolinea = await _dbcontext.Aerolineas.ToListAsync(),
                ListadoDestino = await _dbcontext.Destinos.ToListAsync(),
            };

            return model;
        }

        public async Task<ViajesGenericoViewModel> ObtenerViajesFiltrados(FiltroViajesViewModel filtros)
        {
            // Empieza con el query base sin filtros
            var query =
                from v in _dbcontext.Viajes
                join a in _dbcontext.Aerolineas on v.IdAerolinea equals a.IdAerolinea
                join dIda in _dbcontext.Destinos on v.IdDestinoIda equals dIda.IdDestino
                join dLlegada in _dbcontext.Destinos on v.IdDestinoLlegada equals dLlegada.IdDestino
                select new ViajesViewModel
                {
                    IdViaje = v.IdViaje,
                    DestinoIda = dIda.NombreDestino,
                    DestinoLlegada = dLlegada.NombreDestino,
                    HoraSalida = v.HoraSalida,
                    HoraLlegada = v.HoraLlegada,
                    FechaViaje = v.FechaViaje,
                    PrecioViaje = v.PrecioViaje,
                    CantidadPuestos = v.CantidadPuestos,
                    AerolineaNombre = a.NombreAerolinea,
                    Imagen = v.Imagen
                };


            if (filtros.IdViaje != 0) 
            {
                query = query.Where(v => v.IdViaje == filtros.IdViaje);
            }

            if (filtros.IdDestinoIda != 0)
            {
                var destinoIda = await _dbcontext.Destinos
                    .Where(d => d.IdDestino == filtros.IdDestinoIda)
                    .Select(d => d.NombreDestino)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(destinoIda))
                    query = query.Where(v => v.DestinoIda == destinoIda);
            }

            if (filtros.IdDestinoLlegada != 0)
            {
                var destinoLlegada = await _dbcontext.Destinos
                    .Where(d => d.IdDestino == filtros.IdDestinoLlegada)
                    .Select(d => d.NombreDestino)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(destinoLlegada))
                    query = query.Where(v => v.DestinoLlegada == destinoLlegada);
            }

            if (filtros.HoraSalida != default)
            {
                query = query.Where(v => v.HoraSalida == filtros.HoraSalida);
            }

            if (filtros.HoraLlegada != default)
            {
                query = query.Where(v => v.HoraLlegada == filtros.HoraLlegada);
            }

            if (filtros.FechaViaje != default)
            {
                query = query.Where(v => v.FechaViaje == filtros.FechaViaje);
            }

            if (filtros.PrecioViaje != 0)
            {
                query = query.Where(v => v.PrecioViaje == filtros.PrecioViaje);
            }

            if (filtros.CantidadPuestos != 0)
            {
                query = query.Where(v => v.CantidadPuestos == filtros.CantidadPuestos);
            }

            if (filtros.IdAerolinea != 0)
            {
                var nombreAerolinea = await _dbcontext.Aerolineas
                    .Where(a => a.IdAerolinea == filtros.IdAerolinea)
                    .Select(a => a.NombreAerolinea)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(nombreAerolinea))
                    query = query.Where(v => v.AerolineaNombre == nombreAerolinea);
            }

            if (!string.IsNullOrWhiteSpace(filtros.Imagen))
            {
                query = query.Where(v => v.Imagen.Contains(filtros.Imagen));
            }

            // Finalmente convertimos la consulta a lista asincrónicamente
            var lista = await query.ToListAsync();

            var model = new ViajesGenericoViewModel
            {
                ListadoViajes = lista,
                ListadoAerolinea = await _dbcontext.Aerolineas.ToListAsync(),
                ListadoDestino = await _dbcontext.Destinos.ToListAsync(),
            };

            return model;
        }

    }
}
