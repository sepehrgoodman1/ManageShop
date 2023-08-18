using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Entities.Entities
{
    public class ProductGroup
    {
        public ProductGroup()
        {
            Products = new();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<Product> Products { get; set; }
    }
}
