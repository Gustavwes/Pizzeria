using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;

namespace Pizzeria.DAL
{
    public class DataAccess
    {

        public void CreateNewCustomer(TomasosContext context, Kund newCustomer)
        {
            context.Add(newCustomer);
            context.SaveChanges();

        }

        public List<Matratt> GetAllMenuItems(TomasosContext context)
        {
            var menuList = context.Matratt.Include(x=>x.MatrattProdukt).ToList();

            foreach (var item in menuList)
            {
                item.MatrattProdukt = context.MatrattProdukt.Include(x=>x.Produkt).ToList();
            }
            return menuList;
        }
    }
}
