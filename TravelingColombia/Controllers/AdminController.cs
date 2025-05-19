using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;

namespace TravelingColombia.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IRepositoryAdmin _adminRepository;

        public AdminController(IRepositoryAdmin adminRepository){
            _adminRepository = adminRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _adminRepository.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _adminRepository.GetByIdAsync(id);
            if (user == null){
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario user)
        {
            if(!ModelState.IsValid){
                return View(user);
            }
            await _adminRepository.Create(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id){
            var empleado = await _adminRepository.GetByIdAsync(id);
            if(empleado==null){
                return NotFound();
            }
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario user){
            if(!ModelState.IsValid){
                return View(user);
            }
            
            await _adminRepository.Update(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id){
            var empleado = await _adminRepository.GetByIdAsync(id);
            if(empleado == null){
                return NotFound();
            }
            await _adminRepository.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Gestiones()
        {
            return View();
        }

        
        public async Task<IActionResult> Informes()
        {
            var users = await _adminRepository.GetAllAsync();
            return View(users);
        }

    }
}