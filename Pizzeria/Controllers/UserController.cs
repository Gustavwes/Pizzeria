using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pizzeria.DAL;
using Pizzeria.Models;
using System.Web;
namespace Pizzeria.Controllers
{
    public class UserController : Controller
    {
        private TomasosContext _context;

        public UserController(TomasosContext context)
        {
            _context = context;
        }

        public IActionResult RegisterCustomer()
        {

            return View();
        }

        public IActionResult Register(Kund kund)
        {
            var dataAccess = new DataAccess();
            dataAccess.CreateNewCustomer(_context, kund);
            return RedirectToAction("Login");
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
                var existingMatch = JsonConvert.DeserializeObject<Kund>(temp);
              
                return RedirectToAction("LoginSuccessful", existingMatch);
            }
            var dataAccess = new DataAccess();

            foreach (var customer in dataAccess.GetAllCustomers(_context))
            {
                if (customer.AnvandarNamn == kund.AnvandarNamn && customer.Losenord == kund.Losenord)
                {
                    customerMatch = customer;
                }

            }
            if (customerMatch.AnvandarNamn == null)
            {
                return RedirectToAction("Login");
            }
            var serializedValue = JsonConvert.SerializeObject(customerMatch);
            Response.Cookies.Append("LoggedIn", serializedValue);
            return RedirectToAction("LoginSuccessful", customerMatch);

        }

       

      

    }
}