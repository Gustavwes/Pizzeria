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
    public class OrderController : Controller
    {
        private TomasosContext _context;

        public OrderController(TomasosContext context)
        {
            _context = context;
        }

        public IActionResult AddProduct(int id)
        {
            var orderTotal = new Bestallning();
            if (Request.Cookies["LoggedIn"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var temp = Request.Cookies["LoggedIn"];
                var existingMatch = JsonConvert.DeserializeObject<Kund>(temp);

                if (Request.Cookies["Cart"] == null)
                {

                    orderTotal.Kund = existingMatch;
                    orderTotal.KundId = existingMatch.KundId;
                    orderTotal.Levererad = true;
                    orderTotal.BestallningDatum = DateTime.Now;
                }
                else
                {

                    var temp2 = Request.Cookies["Cart"];
                    orderTotal = JsonConvert.DeserializeObject<Bestallning>(temp2);
                }
                DataAccess dataAccess = new DataAccess();
                var foodOrder = dataAccess.GetSpecificMatratt(_context, id);
                var orderConnection = new BestallningMatratt();

                orderConnection.MatrattId = foodOrder.MatrattId;
                orderConnection.BestallningId = orderTotal.BestallningId;

                orderTotal.Totalbelopp += foodOrder.Pris;
                orderTotal.BestallningMatratt.Add(orderConnection);

                var serializedValue = JsonConvert.SerializeObject(orderTotal);
                Response.Cookies.Append("Cart", serializedValue);
                return RedirectToAction("Menu", "Home");



            }

            return RedirectToAction("Menu", "Home");
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}