using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ClassContext<T> : IClassContext<T> where T : BaseModel
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public ClassContext()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Delete(string id)
        {
            T target = items.Find(i => i.Id == id);

            if (target != null)
            {
                items.Remove(target);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public void Update(T t)
        {
            T target = items.Find(i => i.Id == t.Id);

            if (target != null)
            {
                target = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }



        public T Find(string id)
        {
            T target = items.Find(i => i.Id == id);

            if (target != null)
            {
                return target;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

    }
}
