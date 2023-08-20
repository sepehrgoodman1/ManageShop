using ManageShop.Entities.Entities;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;

namespace ManageShop.Services.PurchaseInvoices.Contracts
{
    public interface PurchaseInvoiceRepository
    {
        Task Add(PurchaseInvoice purchaseInvoice);
        Task<List<PurchaseInvoice>> GetAll();
        Task<List<PurchaseInvoice>> Search(string search);
    }
}
