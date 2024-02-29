using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserPortal.Models;
using PagedList;


namespace UserPortal.Controllers
{
    public class HomeController : Controller
    { 

        ProductsEntities db = new ProductsEntities();

        public ActionResult Index()
        { return View(); }
            
        /*public ActionResult DashboardData()
        {
            List<storeProduct> stor = db.storeProducts.ToList();
            return View(stor);
        }*/

        public ActionResult DashboardData(string option, string search, int? pageNumber)
        {
            if (option == "Item_Name")
            {
                return View(db.storeProducts.Where
                    (x => x.Item_Name.StartsWith(search) || search == null).ToList()
                    .ToPagedList(pageNumber ?? 1, 3));
            }
            else
            {
                return View(db.storeProducts.Where
                    (x => x.Description == search || search == null).ToList()
                    .ToPagedList(pageNumber ?? 1, 3));
            }

        }

        //Start of Create Functionality

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(storeProduct dash)
        {
            try
            {
                if (dash != null)
                {
                    storeProduct dashData = new storeProduct();
                    dashData.Item_Name = dash.Item_Name;
                    dashData.Description = dash.Description;
                    dashData.Price = dash.Price;
                    dashData.Quantity = dash.Quantity;

                    db.storeProducts.Add(dashData);
                    db.SaveChanges();
                }
                return RedirectToAction("DashboardData");
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred.";
                return RedirectToAction("DashboardData");
            }
        }

        //Start of Edit Functionality

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            try
            {
                if (ID != 0)
                {
                    storeProduct prod_data = db.storeProducts.SingleOrDefault(x => x.ID == ID);
                    return PartialView("Edit", prod_data);
                }
                else
                {
                    return RedirectToAction("DashboardData");
                }
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred";
                return RedirectToAction("DashboardData");
            }
        }
        [HttpPost]
        public ActionResult Edit(storeProduct dash_modified)
        {
            try
            {
                if (dash_modified != null)
                {
                    storeProduct dash_update = db.storeProducts.SingleOrDefault(x => x.ID == dash_modified.ID);
                    dash_update.Item_Name = dash_modified.Item_Name;
                    dash_update.Description = dash_modified.Description;
                    dash_update.Price = dash_modified.Price;
                    dash_update.Quantity = dash_modified.Quantity;
                    db.SaveChanges();
                }
                return RedirectToAction("DashboardData");
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred";
                return RedirectToAction("DashboardData");
            }
        }

        //Start of Read Functionality
        [HttpGet]
        public ActionResult Read(int id)
        {
            using (var context = new ProductsEntities())
            {
                var data = context.storeProducts.Where(x => x.ID == id).FirstOrDefault();
                return View(data);
            }
        }

        //Start of Delete Functionality
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                storeProduct prod_data = db.storeProducts.SingleOrDefault(x => x.ID == id);
                if (prod_data != null)
                {
                    db.storeProducts.Remove(prod_data);
                    db.SaveChanges();
                }
                return RedirectToAction("DashboardData");
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred.";
                return RedirectToAction("DashboardData");
            }
        }
    }
}