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
    public class RepositoryPago : RepositoryGeneric<Pago, int>, IRepositoryPago
    {
        private readonly TravelingColombiabdContext _dbcontext;
        public RepositoryPago(TravelingColombiabdContext context) : base(context)
        {
            _dbcontext = context;
        }

        public async Task<PagosGenericoViewModel> ListadoPagos()
        {
            var lista = await (from p in _dbcontext.Pagos
                               join mp in _dbcontext.MetodoPagos on p.IdMetodo equals mp.IdMetodo
                               join b in _dbcontext.Bancos on p.IdBanco equals b.IdBanco
                               select new PagosViewModel
                               {
                                   NombreUsuario = p.Nombre,
                                   CedulaUsuario = p.Cedula,
                                   NombreBanco = b.NombreBanco,
                                   CuentaBanco = p.Cuenta,
                                   Monto = p.Monto,
                                   MetodoPago = mp.MetodoPago1
                               }).ToListAsync();

            var pagos = new PagosGenericoViewModel
            {
                ListadoPagos = lista,
                ListadoBnaco= await _dbcontext.Bancos.ToListAsync(),
                ListadoMetodoPagos= await _dbcontext.MetodoPagos.ToListAsync(),
            };

            return pagos;
        }


        public async Task<PagosGenericoViewModel> ObtenerPagos(FiltroPagosViewModel filtros)
        {

            var query = 
                from p in _dbcontext.Pagos
                join mp in _dbcontext.MetodoPagos on p.IdMetodo equals mp.IdMetodo
                join b in _dbcontext.Bancos on p.IdBanco equals b.IdBanco
                select new PagosViewModel
                {
                    NombreUsuario = p.Nombre,
                    CedulaUsuario = p.Cedula,
                    NombreBanco = b.NombreBanco,
                    CuentaBanco = p.Cuenta,
                    Monto = p.Monto,
                    MetodoPago = mp.MetodoPago1
                };


                 if (!string.IsNullOrWhiteSpace(filtros.Nombre))
                query = query.Where(p => p.NombreUsuario.Contains(filtros.Nombre));
                
            if (!string.IsNullOrWhiteSpace(filtros.Cedula))
                query = query.Where(p => p.CedulaUsuario.Contains(filtros.Cedula));

            if (!string.IsNullOrWhiteSpace(filtros.Cuenta))
                query = query.Where(p => p.CuentaBanco.Contains(filtros.Cuenta));

            if (filtros.IdBanco.HasValue)
            {
                var NombreBanco = await _dbcontext.Bancos
                                        .Where(b => b.IdBanco == filtros.IdBanco.Value)
                                        .Select(b => b.NombreBanco)
                                        .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(NombreBanco))
                {
                    query = query.Where(b => b.NombreBanco == NombreBanco);
                }
            }
            if (filtros.IdMetodo.HasValue)
            {
                var NombreMetodo = await _dbcontext.MetodoPagos
                                        .Where(mp => mp.IdMetodo == filtros.IdMetodo.Value)
                                        .Select(mp => mp.MetodoPago1)
                                        .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(NombreMetodo))
                {
                    query = query.Where(mp => mp.MetodoPago == NombreMetodo);
                }
            }

            if (filtros.Monto.HasValue)
                query = query.Where(p => p.Monto == filtros.Monto.Value);

            var pagos = new PagosGenericoViewModel
            {
                ListadoPagos = query.ToList(),
                ListadoBnaco= await _dbcontext.Bancos.ToListAsync(),
                ListadoMetodoPagos= await _dbcontext.MetodoPagos.ToListAsync(),
            };


            return pagos;
        }


    }
}