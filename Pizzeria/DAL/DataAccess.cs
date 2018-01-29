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

        public List<Kund> GetAllCustomers(TomasosContext context)
        {
            var customerList = context.Kund;

            return customerList.ToList();
        }

        public List<Matratt> GetAllMenuItems(TomasosContext context)
        {
            var menuList = context.Matratt.Include(x=>x.MatrattProdukt).ToList();

            foreach (var item in menuList)
            {
                item.MatrattProdukt = GetAllMatrattProdukts(context, item.MatrattId);
            }
            return menuList;
        }

        public void CreateOrderConnection(TomasosContext context, BestallningMatratt connection)
        {
            
            context.Add(connection);
            context.SaveChanges();

        }

        public void CreateOrder(TomasosContext context, Bestallning order)
        {
            context.Add(order);
            context.SaveChanges();
            
        }

        public Matratt GetSpecificMatratt(TomasosContext context, int foodId)
        {
            var selectedItem = context.Matratt.FirstOrDefault(x => x.MatrattId == foodId);
            return selectedItem;
        }

        public List<MatrattProdukt> GetAllMatrattProdukts(TomasosContext context,int matId)
        {
            var matrattprodukts = context.MatrattProdukt.Where(x => x.MatrattId == matId).Include(y=>y.Produkt).ToList();
            return matrattprodukts;
        }
    }
}
