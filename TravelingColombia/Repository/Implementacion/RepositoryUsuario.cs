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
    public class RepositoryUsuario : RepositoryGeneric<Usuario, int>, IRepositoryUsuario
    {
        private readonly TravelingColombiabdContext _context;

        public RepositoryUsuario(TravelingColombiabdContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UsuarioViewModel>> ListaUsuarios(UsuarioViewModel filtro)
        {
            var query = _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .Select(u => new UsuarioViewModel
                {
                    IdUsuario = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    ApellidoUsuario = u.ApellidoUsuario,
                    CelularUsuario = u.CelularUsuario,
                    EmailUsuario = u.EmailUsuario,
                    EdadUsuario = u.EdadUsuario,
                    CantidadFacturas=u.CantidadFacturas,
                    Rol = u.IdRolNavigation.Rol1,
                    IdRol = u.IdRol 
                });

            if (!string.IsNullOrWhiteSpace(filtro.EmailUsuario))
            {
                query = query.Where(u => u.EmailUsuario == filtro.EmailUsuario);
            }

            if (filtro.IdRol > 0)
            {
                query = query.Where(u => u.IdRol == filtro.IdRol);
            }

            return await query.ToListAsync();
        }

    }
}