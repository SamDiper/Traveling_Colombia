using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Filtros
{
    public class FiltroUsuarioCiewModel
    {
        public List<UsuarioViewModel> listaUsuarios { get; set; }
       public List<Rol> ListaRoles { get; set; }

    }
}