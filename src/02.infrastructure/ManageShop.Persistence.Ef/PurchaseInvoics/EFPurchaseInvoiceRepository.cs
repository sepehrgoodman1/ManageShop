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
                                          .OrderByDescending(_=>_.Date)
                                          .ToListAsync();
        }

        public  async Task<List<PurchaseInvoice>> Search(string search)
        {
            var finedResult = await _purchaseInvoices
                                    .Include(_ => _.ProductPurchaseInvoices)
                                    .ThenInclude(_ => _.Products.ProductGroup)
                                    .Distinct()
                                    .OrderByDescending(_ => _.Date)
                                    .ToListAsync();

            return finedResult.Where(_ => _.ProductPurchaseInvoices.Select(_ => _.Products.Title).Contains(search) ||
                                     _.ProductPurchaseInvoices.Select(_ => _.Products.Status.ToString()).Contains(search) ||
                                     _.ProductPurchaseInvoices.Select(_ => _.Products.ProductGroup.Name).Contains(search)).ToList();
        }
    }
}
