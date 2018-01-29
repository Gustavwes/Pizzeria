using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using Pizzeria.ViewModel;

namespace Pizzeria.DAL
{
    public class DataAccess
    {

        public void CreateNewCustomer(TomasosContext context, Kund newCustomer)
        {
            context.Add(newCustomer);
            context.SaveChanges();

        }

        public void EditCustomer(TomasosContext context, KundViewModel editedCustomer, Kund currentCustomer)
        {
            //var string
            //Kund changedCustomer = new Kund()
            //{
            //    KundId = currentCustomer.KundId,
            //    AnvandarNamn = editedCustomer.AnvandarNamn,
            //    Email = editedCustomer.Email,
            //    Gatuadress = editedCustomer.Gatuadress,
            //    Postnr = editedCustomer.Postnr,
            //    Postort = editedCustomer.Postort,
            //    Namn = editedCustomer.Namn,
            //    Telefon = editedCustomer.Telefon,
            //    Losenord = editedCustomer.Losenord
            //};

            //foreach (PropertyInfo propertyInfo in currentCustomer.GetType().GetProperties())
            //{
            //    if (propertyInfo.GetValue(changedCustomer, null) != null)
            //        propertyInfo.SetValue(currentCustomer, propertyInfo.GetValue(changedCustomer, null), null);
            //}
            //    context.Entry(currentCustomer).CurrentValues.SetValues(changedCustomer);
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
