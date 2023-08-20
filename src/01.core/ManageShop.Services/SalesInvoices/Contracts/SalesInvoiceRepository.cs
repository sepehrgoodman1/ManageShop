using ManageShop.Entities.Entities;

namespace ManageShop.Services.SalesInvoices.Contracts
{
    public interface SalesInvoiceRepository
    {
        Task Add(SalesInvoice salesInvoice);
    }
}
