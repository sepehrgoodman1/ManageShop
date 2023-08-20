using ManageShop.Services.SalesInvoices.Contracts.Dtos;

namespace ManageShop.Services.SalesInvoices.Contracts
{
    public interface SaleInvoiceService
    {
        Task Add(string clientName, List<AddSaleInvoiceDto> dto);
    }
}
