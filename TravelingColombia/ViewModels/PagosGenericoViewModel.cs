using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class PagosGenericoViewModel
    {
       public List<PagosViewModel>? ListadoPagos { get; set; }
       public List<MetodoPago>? ListadoMetodoPagos { get; set; }
       public List<Banco>? ListadoBnaco { get; set; }

    }
}