using ManageShop.Entities.Entities;
using ManageShop.Services.SalesInvoices.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Persistence.Ef.SalesInvoices
{
    public class EFSalesInvoiceRepository : SalesInvoiceRepository
    {
        private readonly DbSet<SalesInvoice> _salesInvoices;

        public EFSalesInvoiceRepository(EFDataContext context)
        {
            _salesInvoices = context.SalesInvoices;
        }

        public async Task Add(SalesInvoice salesInvoice)
        {
            await _salesInvoices.AddAsync(salesInvoice);
        }
    }
}
