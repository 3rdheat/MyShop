using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategoryManager
        ClassContext<ProductCategory> context;

        public ProductCategoryManagerController()
        {
            context = new ClassContext<ProductCategory>();
        }

        public ActionResult Index()
        {
            List<ProductCategory> categories = context.Collection().ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            ProductCategory categories = new ProductCategory();
            return View(categories);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }
            else
            {
                context.Insert(c);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string id)
        {
            ProductCategory target = context.Find(id);
            if (target == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(target);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory p, string id)
        {
            ProductCategory target = context.Find(id);
            if (target == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(target);
                }

                target.Category = p.Category;
            
                context.Commit();
                return RedirectToAction("Index");
            }



        }

        public ActionResult Delete(string id)
        {
            ProductCategory target = context.Find(id);
            if (target == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(target);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory target = context.Find(id);
            if (target == null)
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