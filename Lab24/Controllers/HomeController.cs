using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab24.Models;

namespace Lab24.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CoffeEntities CoffeeORM = new CoffeEntities();
            ViewBag.Items = CoffeeORM.Items.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NewItem()
        {
            return View();
        }

        public ActionResult SaveNewItem(Item newItem)
        {
            CoffeEntities CoffeeORM = new CoffeEntities();
            //NEEDS VALIDATION

            CoffeeORM.Items.Add(newItem);

            CoffeeORM.SaveChanges(); // syncs with the DB

            return RedirectToAction("Index");
        }

        public ActionResult ItemDetails(int itemID)
        {
            CoffeEntities CoffeeORM = new CoffeEntities();

            //find item
            Item ItemToEdit = CoffeeORM.Items.Find(itemID);

            if (ItemToEdit == null)
            {
                return RedirectToAction("Index");
            }

            //send back to view
            ViewBag.ItemToEdit = ItemToEdit;

            return View();
        }

        public ActionResult DeleteItem(int itemID)
        {
            CoffeEntities CoffeeORM = new CoffeEntities();

            //for loop to find the id
            //find is a mtehod that finds objects using PrimaryKey
            Item ItemToDelete = CoffeeORM.Items.Find(itemID);

            //remove
            CoffeeORM.Items.Remove(ItemToDelete);

            CoffeeORM.SaveChanges();//NEEDS TRY/CATCH

            return RedirectToAction("Index");
        }

        public ActionResult SaveChanges(Item UpdatedItem)
        {
            CoffeEntities CoffeeORM = new CoffeEntities();

            Item OldRecord = CoffeeORM.Items.Find(UpdatedItem.ItemID);
            
            //Check for Null
            if (OldRecord == null)
            {
                return RedirectToAction("Index");
            }

            OldRecord.Name = UpdatedItem.Name;
            OldRecord.Description = UpdatedItem.Description;
            OldRecord.Quantity = UpdatedItem.Quantity;
            OldRecord.Price = UpdatedItem.Price;

            //Modifies record (original vs current)
            CoffeeORM.Entry(OldRecord).State = System.Data.Entity.EntityState.Modified;

            CoffeeORM.SaveChanges();

            return RedirectToAction("Index");

        }
        
        public ActionResult SearchItemByName(string Name)
        {
            CoffeEntities CoffeeORM = new CoffeEntities();

            //Lambda function for searching
            ViewBag.Items = CoffeeORM.Items.Where(x => x.Name.ToLower().Contains(Name.ToLower())).ToList();

            return View("Index");
        }
    }
}