﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pizzeria.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult AddProduct()
        {
            return View();
        }
    }
}