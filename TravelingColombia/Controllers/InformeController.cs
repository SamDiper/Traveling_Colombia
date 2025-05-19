using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TravelingColombia.Controllers
{
    
    public class InformeController : Controller
    {
      

        public IActionResult Viaje()
        {
            return View();
        }
        public IActionResult Plan()
        {
            return View();
        }
        public IActionResult Reserva()
        {
            return View();
        }
        public IActionResult Pago()
        {
            return View();
        }

       
    }
}