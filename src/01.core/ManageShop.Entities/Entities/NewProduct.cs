using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Entities.Entities
{
    public class NewProduct
    {
        public int Id { get; set; }
        public int ProductCode { get; set; }
        public int ProductRecivedCount { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; }
    }
}
