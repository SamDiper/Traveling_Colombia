using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Implementacion;
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

        public async Task<List<PagosViewModel>> ListadoPagos()
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

            return lista;
        }


        public async Task<List<PagosViewModel>> ObtenerPagos(string filtro = null)
        {
            var lista = await
                (from p in _dbcontext.Pagos
                 join mp in _dbcontext.MetodoPagos on p.IdMetodo equals mp.IdMetodo
                 join b in _dbcontext.Bancos on p.IdBanco equals b.IdBanco
                 where string.IsNullOrEmpty(filtro) ||
                        p.Cedula == filtro ||
                        p.Cuenta == filtro ||
                        b.NombreBanco.Contains(filtro)
                 select new PagosViewModel
                 {
                     NombreUsuario = p.Nombre,
                     CedulaUsuario = p.Cedula,
                     NombreBanco = b.NombreBanco,
                     CuentaBanco = p.Cuenta,
                     Monto = p.Monto,
                     MetodoPago = mp.MetodoPago1
                 }).ToListAsync();

            return lista;
        }


    }
}