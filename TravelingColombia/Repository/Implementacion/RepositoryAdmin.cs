using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Implementacion;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;

namespace TravelingColombia.Repository.Implementacion
{
    public class RepositoryAdmin : RepositoryGeneric<Usuario, int>, IRepositoryAdmin
    {
        public RepositoryAdmin(TravelingColombiabdContext context) : base(context)
        {
            
        }
    }
}