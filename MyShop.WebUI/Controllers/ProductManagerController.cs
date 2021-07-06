using MyShop.Core.Models;
using MyShop.Core.ViewModels;
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
        ProductCategoryRepository contextProductCategory;

        public ProductManagerController()
        {
            context = new ProductRepository();
            contextProductCategory = new ProductCategoryRepository();
        }

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel vmProductManager = new ProductManagerViewModel();
            vmProductManager.Product = new Product();
            vmProductManager.ProductCategories = contextProductCategory.Collection();

            return View(vmProductManager);
        }
        [HttpPost]
        public ActionResult Create(ProductManagerViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p.Product);
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

                ProductManagerViewModel vmProductManager = new ProductManagerViewModel();
                vmProductManager.Product = product;
                vmProductManager.ProductCategories = contextProductCategory.Collection();

                return View(vmProductManager);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductManagerViewModel p, string id)
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

                product.Name = p.Product.Name;
                product.Description = p.Product.Description; 
                product.Category = p.Product.Category;
                product.Price = p.Product.Price;
                product.Image = p.Product.Image;

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