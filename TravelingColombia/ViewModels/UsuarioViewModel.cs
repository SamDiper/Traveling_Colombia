using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string ApellidoUsuario { get; set; } = null!;

        public string CelularUsuario { get; set; } = null!;

        public string EmailUsuario { get; set; } = null!;

        public int EdadUsuario { get; set; }

        public int IdRol { get; set; }
        public string Rol { get; set; }

        public int CantidadFacturas { get; set; }

        public string Contrasena { get; set; } = null!;

        [NotMapped]
        public IFormFile ImagenArchivo { get; set; }
        public string? FotoUsuario { get; set; }
        public string? CedulaUsuario { get; set; }

        public List<Rol> ListaRoles { get; set; }

    }
}