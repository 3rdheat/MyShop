using MyShop.Core.Contracts;
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

        IClassContext<Product> productContext;
        IClassContext<ProductCategory> productCategoryContext;

      

        public ProductManagerController(IClassContext<Product> productContext, IClassContext<ProductCategory> productCategoryContext)
        {
            this.productContext = productContext;
            this.productCategoryContext = productCategoryContext;

        }

        public ActionResult Index()
        {
            List<Product> products = productContext.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel vmProductManager = new ProductManagerViewModel();
            vmProductManager.Product = new Product();
            vmProductManager.ProductCategories = productCategoryContext.Collection();

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
                productContext.Insert(p.Product);
                productContext.Commit();
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Edit(string id)
        {
            Product product = productContext.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {

                ProductManagerViewModel vmProductManager = new ProductManagerViewModel();
                vmProductManager.Product = product;
                vmProductManager.ProductCategories = productCategoryContext.Collection();

                return View(vmProductManager);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductManagerViewModel p, string id)
        {
            Product product = productContext.Find(id);
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

                productContext.Commit();
                return RedirectToAction("Index");
            }



        }

        public ActionResult Delete(string id)
        {
            Product product = productContext.Find(id);
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
            Product product = productContext.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                productContext.Delete(id);
                productContext.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Search(string id)
        {
            productContext.Find(id);
            return View();
        }


    }
}