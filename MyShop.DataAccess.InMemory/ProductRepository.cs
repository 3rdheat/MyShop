using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;


namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product product)
        {
            products.Add(product);
        }

        public void Delete(string id)
        {
            Product targetProduct = products.FirstOrDefault(p => p.Id == id);
            if (targetProduct != null)
            {
                products.Remove(targetProduct);
            }
            else
            {
                throw new Exception("Product not found");
            }

        }

        public void Update(Product product)
        {
            Product targetProduct = products.FirstOrDefault(p => p.Id == product.Id);

            if (targetProduct != null)
            {
                targetProduct = product;
            }
            else
            {
                throw new Exception("Product not found");
            }

        }

        public Product Find(string id)
        {
            Product product = products.Find(p => p.Id == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }







    }
}
