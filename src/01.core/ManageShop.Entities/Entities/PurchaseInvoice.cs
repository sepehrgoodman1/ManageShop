using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Entities.Entities
{
    public class PurchaseInvoice //رسید خرید و ورود به انبار
    {
        public PurchaseInvoice()
        {
            ProductPurchaseInvoices = new();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public HashSet<NewProduct> NewProducts { get; set; } = new HashSet<NewProduct>();
        public HashSet<ProductPurchaseInvoice> ProductPurchaseInvoices { get; set; }

    }
   
}
