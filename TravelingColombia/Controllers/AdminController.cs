using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TravelingColombia.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        [Route("Gestiones")]
        public IActionResult Gestiones()
        {
            return View();
        }

        [Route("Informes")]
        public IActionResult Informes()
        {
            return View();
        }

    }
}