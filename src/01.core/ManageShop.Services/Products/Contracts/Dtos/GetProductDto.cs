using ManageShop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Services.Products.Contracts.Dtos
{
    public class GetProductDto
    {
        public string Title { get; set; }
        public int MinimumInventory { get; set; }
        public int Inventory { get; set; } = 0;
        public ProductStatus Status { get; set; }
        public int ProductGroupId { get; set; }
    }
}
