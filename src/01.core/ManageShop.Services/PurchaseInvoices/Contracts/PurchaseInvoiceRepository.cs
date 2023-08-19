using ManageShop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Services.PurchaseInvoices.Contracts
{
    public interface PurchaseInvoiceRepository
    {
        Task Add(PurchaseInvoice purchaseInvoice);
    }
}
