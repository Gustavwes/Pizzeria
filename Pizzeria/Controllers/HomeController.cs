using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.DAL;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        private TomasosContext _context;

        public HomeController(TomasosContext context)
        {
            _context = context;
        }

        public IActionResult RegisterCustomer()
        {
            
            return View();
        }

        public IActionResult RegisterComplete(Kund kund)
        {
            var dataAccess = new DataAccess();
            dataAccess.CreateNewCustomer(_context, kund);
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            var dataAccess = new DataAccess();
            var menuList = dataAccess.GetAllMenuItems(_context);
            return View(menuList);
        }
    }
}