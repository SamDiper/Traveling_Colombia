using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Repository.Interface;
using TravelingColombia.Filtros;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Interface;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IRepositoryUsuario _RepositorioUsuario;
        private readonly IRepositoryGeneric<Rol, int> _RepositorioRol;
        private readonly IUnitUser _unitUser;
        public UsuarioController(IUnitUser unitUser, IRepositoryUsuario RepositorioUsuario, IRepositoryGeneric<Rol, int> RepositorioRol)
        {
            _RepositorioRol = RepositorioRol;
            _RepositorioUsuario = RepositorioUsuario;
            _unitUser = unitUser;
        }
        public async Task<IActionResult> Index(UsuarioViewModel filtro)
        {

            FiltroUsuarioCiewModel vista = new FiltroUsuarioCiewModel
            {
                listaUsuarios = await _RepositorioUsuario.ListaUsuarios(filtro),
                ListaRoles = await _RepositorioRol.GetAllAsync()
            };
            return View(vista);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioViewModel usuarioview, [FromServices] Cloudinary cloudinary)
        {
            string imageUrl = null;

            if (usuarioview.ImagenArchivo != null && usuarioview.ImagenArchivo.Length > 0)
            {
                using var stream = usuarioview.ImagenArchivo.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(usuarioview.ImagenArchivo.FileName, stream),
                    Folder = "Usuarios"
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    imageUrl = uploadResult.SecureUrl.ToString();
                }
                else
                {
                    ModelState.AddModelError("ImagenArchivo", "Error al subir la imagen");
                    return View(usuarioview);
                }
            }
            var usuario = new Usuario
            {
                NombreUsuario = usuarioview.NombreUsuario,
                ApellidoUsuario = usuarioview.ApellidoUsuario,
                CelularUsuario = usuarioview.CelularUsuario,
                EmailUsuario = usuarioview.EmailUsuario,
                EdadUsuario = usuarioview.EdadUsuario,
                IdRol = usuarioview.IdRol,
                CedulaUsuario = usuarioview.CedulaUsuario,
                FotoUsuario = imageUrl,
                CantidadFacturas = usuarioview.CantidadFacturas,
                Contrasena = usuarioview.Contrasena,
            };

            await _RepositorioUsuario.Create(usuario);
            await _unitUser.SaveChangesAsync();
            return RedirectToAction("Index", "Usuario");
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var UsuarioFiltrado = await _RepositorioUsuario.GetByIdAsync(id);
            UsuarioViewModel usuario = new UsuarioViewModel
            {
                IdUsuario = UsuarioFiltrado.IdUsuario,
                NombreUsuario = UsuarioFiltrado.NombreUsuario,
                ApellidoUsuario = UsuarioFiltrado.ApellidoUsuario,
                CelularUsuario = UsuarioFiltrado.CelularUsuario,
                EmailUsuario = UsuarioFiltrado.EmailUsuario,
                EdadUsuario = UsuarioFiltrado.EdadUsuario,
                IdRol = UsuarioFiltrado.IdRol,
                CedulaUsuario = UsuarioFiltrado.CedulaUsuario,
                FotoUsuario = UsuarioFiltrado.FotoUsuario,
                CantidadFacturas = UsuarioFiltrado.CantidadFacturas,
                Contrasena = UsuarioFiltrado.Contrasena,
            };
            var ListaRol = await _RepositorioRol.GetAllAsync();
            ViewBag.ListaRol = new SelectList(ListaRol, "IdRol", "Rol1", UsuarioFiltrado?.IdRol);

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioViewModel usuarioview, [FromServices] Cloudinary cloudinary)
        {
            string imageUrl = null;

            if (usuarioview.ImagenArchivo != null && usuarioview.ImagenArchivo.Length > 0)
            {
                using var stream = usuarioview.ImagenArchivo.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(usuarioview.ImagenArchivo.FileName, stream),
                    Folder = "Usuarios"
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    imageUrl = uploadResult.SecureUrl.ToString();
                }
                else
                {
                    ModelState.AddModelError("ImagenArchivo", "Error al subir la imagen");
                }
            }
            var Usuario = await _RepositorioUsuario.GetByIdAsync(usuarioview.IdUsuario);

            Usuario.IdUsuario = usuarioview.IdUsuario;
            Usuario.NombreUsuario = usuarioview.NombreUsuario;
            Usuario.ApellidoUsuario = usuarioview.ApellidoUsuario;
            Usuario.CelularUsuario = usuarioview.CelularUsuario;
            Usuario.EmailUsuario = usuarioview.EmailUsuario;
            Usuario.EdadUsuario = usuarioview.EdadUsuario;
            Usuario.CedulaUsuario = usuarioview.CedulaUsuario;
            Usuario.FotoUsuario = imageUrl;
            Usuario.IdRol = usuarioview.IdRol;
            Usuario.CantidadFacturas = usuarioview.CantidadFacturas;
            Usuario.Contrasena = usuarioview.Contrasena;


            await _RepositorioUsuario.Update(Usuario);
            await _unitUser.SaveChangesAsync();
            return RedirectToAction("Perfil", "Cliente");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viaje = await _RepositorioUsuario.GetByIdAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }

            await _RepositorioUsuario.DeleteByIdAsync(id);
            await _unitUser.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}