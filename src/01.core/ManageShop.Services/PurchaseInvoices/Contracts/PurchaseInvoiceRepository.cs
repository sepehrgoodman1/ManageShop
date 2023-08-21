using ManageShop.Entities.Entities;

namespace ManageShop.Services.PurchaseInvoices.Contracts
{
    public interface PurchaseInvoiceRepository
    {
        Task Add(PurchaseInvoice purchaseInvoice);
        Task<List<PurchaseInvoice>> GetAll();
        Task<List<PurchaseInvoice>> Search(string search);
    }
}
