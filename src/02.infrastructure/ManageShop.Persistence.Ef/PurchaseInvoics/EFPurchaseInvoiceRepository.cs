using ManageShop.Entities.Entities;
using ManageShop.Services.PurchaseInvoices.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Persistence.Ef.PurchaseInvoics
{
    public class EFPurchaseInvoiceRepository : PurchaseInvoiceRepository
    {
        private readonly DbSet<PurchaseInvoice> _purchaseInvoices;

        public EFPurchaseInvoiceRepository(EFDataContext context)
        {
            _purchaseInvoices = context.PurchaseInvoices;
        }

        public async Task Add(PurchaseInvoice purchaseInvoice)
        {
            await _purchaseInvoices.AddAsync(purchaseInvoice);
        }
    }
}
