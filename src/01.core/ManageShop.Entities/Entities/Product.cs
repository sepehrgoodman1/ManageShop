using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Entities.Entities
{
    public class Product
    {
        public Product()
        {
            ProductPurchaseInvoices = new();
            ProductSalesInvoices = new();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int MinimumInventory { get; set; }
        public int Inventory { get; set; } = 0;
        public ProductStatus Status { get; set; }
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public HashSet<ProductPurchaseInvoice> ProductPurchaseInvoices { get; set; }
        public HashSet<ProductSalesInvoice> ProductSalesInvoices { get; set; }
    }
    public enum ProductStatus : int
    {
        Available = 2,
        ReadyToOrder = 1,
        UnAvailable = 0
    }
}
