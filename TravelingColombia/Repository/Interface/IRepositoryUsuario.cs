using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Interface;
using TravelingColombia.Models;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Interface
{
    public interface IRepositoryUsuario:IRepositoryGeneric<Usuario,int>
    {
        Task<List<UsuarioViewModel>> ListaUsuarios(UsuarioViewModel filtro);
    }
}