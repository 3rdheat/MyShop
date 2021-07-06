using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategory"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategory"] = productCategories;
        }

        public void Insert(ProductCategory category)
        {
            productCategories.Add(category);
        }

        public void Delete(string id)
        {
            ProductCategory target = productCategories.FirstOrDefault(p => p.Id == id);
            if (target != null)
            {
                productCategories.Remove(target);
            }
            else
            {
                throw new Exception("Product not found");
            }

        }

        public void Update(ProductCategory category)
        {
            ProductCategory target = productCategories.FirstOrDefault(p => p.Id == category.Id);

            if (target != null)
            {
                target = category;
            }
            else
            {
                throw new Exception("Product Category not found");
            }

        }

        public ProductCategory Find(string id)
        {
            ProductCategory target = productCategories.Find(p => p.Id == id);

            if (target != null)
            {
                return target;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }







    }
}
