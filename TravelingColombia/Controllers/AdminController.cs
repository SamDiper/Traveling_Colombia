using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TravelingColombia.Controllers
{
    
    public class AdminController : Controller
    {
       
        public IActionResult Gestiones()
        {
            return View();
        }

        
        public IActionResult Informes()
        {
            return View();
        }

    }
}