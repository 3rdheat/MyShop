using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        public ActionResult Create(ProductManagerViewModel p, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                if (file != null)
                {
                    p.Product.Image = p.Product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + p.Product.Image);
                }

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
        public ActionResult Edit(ProductManagerViewModel p, string id, HttpPostedFileBase file)
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

                if (file != null)
                {
                    product.Image = p.Product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                product.Name = p.Product.Name;
                product.Description = p.Product.Description; 
                product.Category = p.Product.Category;
                product.Price = p.Product.Price;
               

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