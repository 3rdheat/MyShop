using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public abstract class BaseModel
    {
        public string Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    
    }


}
