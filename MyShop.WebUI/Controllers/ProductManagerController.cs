using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        // GET: ProductManager

        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p);
                context.Commit();
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product p, string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                product.Name = p.Name;
                product.Description = p.Description; 
                product.Category = p.Category;
                product.Price = p.Price;
                product.Image = p.Image;

                context.Commit();
                return RedirectToAction("Index");
            }



        }

        public ActionResult Delete(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Search(string id)
        {
            context.Find(id);
            return View();
        }


    }
}