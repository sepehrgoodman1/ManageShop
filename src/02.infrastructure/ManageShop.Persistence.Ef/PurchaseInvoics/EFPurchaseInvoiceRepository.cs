using ManageShop.Entities.Entities;
using ManageShop.Services.PurchaseInvoices.Contracts;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<PurchaseInvoice>> GetAll()
        {
            return await _purchaseInvoices.Include(_ => _.ProductPurchaseInvoices)
                                          .ThenInclude(_=>_.Products.ProductGroup)
                                          .ToListAsync();
        }

        public async Task<List<PurchaseInvoice>> Search(string search)
        {
            var x = _purchaseInvoices.Include(_ => _.ProductPurchaseInvoices)
                                           .ThenInclude(_ => _.Products.ProductGroup)
                                           .ToList();

            return x.Where(_ => _.ProductPurchaseInvoices.Select(_ => _.Products.Title).Contains(search) ||
                                _.ProductPurchaseInvoices.Select(_ => _.Products.Status.ToString()).Contains(search)).ToList();
        }
    }
}
