using ManageShop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Services.PurchaseInvoices.Contracts.Dtos
{
    public class GetPurchaseInvoiceDto
    {
        public int ProductCode { get; set; }
        public string Title { get; set; }
        public int MinimumInventory { get; set; }
        public int Inventory { get; set; } = 0;
        public string Status { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }
    }
}
