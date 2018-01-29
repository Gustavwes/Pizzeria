using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public IActionResult Login()
        {

            return View();
        }

        public IActionResult LoginSuccessful(Kund user)
        {
            return View(user);
        }

      
        [HttpPost]
        public IActionResult Login(Kund kund)
        {
            Kund customerMatch;
            if (Request.Cookies["LoggedIn"] == null)
            {
                customerMatch = new Kund();
            }
            else
            {
                var temp = Request.Cookies["LoggedIn"];
                customerMatch = JsonConvert.DeserializeObject<Kund>(temp);
                return RedirectToAction("LoginSuccessful", customerMatch);
            }
            var dataAccess= new DataAccess();
           
            foreach (var customer in dataAccess.GetAllCustomers(_context))
            {
                if (customer.AnvandarNamn == kund.AnvandarNamn && customer.Losenord == kund.Losenord)
                {
                    customerMatch = customer;
                }
                
            }
            if (customerMatch.AnvandarNamn == null)
            {
                RedirectToAction("Login");
            }
            var serializedValue = JsonConvert.SerializeObject(customerMatch);
            Response.Cookies.Append("LoggedIn", serializedValue);
            return RedirectToAction("Login");

        }
    }
}