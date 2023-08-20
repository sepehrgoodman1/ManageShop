using ManageShop.Entities.Entities;
using ManageShop.Services.SalesInvoices.Contracts;
using Microsoft.EntityFrameworkCore;

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
