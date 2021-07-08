using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IClassContext<Product> productContext;
        IClassContext<ProductCategory> productCategoryContext;

        public HomeController(IClassContext<Product> productContext, IClassContext<ProductCategory> productCategoryContext)
        {
            this.productContext = productContext;
            this.productCategoryContext = productCategoryContext;

        }


        public ActionResult Index(string Category = null)
        {
            List<ProductCategory> categories = productCategoryContext.Collection().ToList();
            List<Product> products; 

            if (Category == null)
            {
                products = productContext.Collection().ToList();
            }
            else
            {
                products = productContext.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListingViewModel vmProductList = new ProductListingViewModel();
            vmProductList.Product = products;
            vmProductList.ProductCategories = categories;


            return View(vmProductList);
        }

        public ActionResult Details(string id)
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
    }
}