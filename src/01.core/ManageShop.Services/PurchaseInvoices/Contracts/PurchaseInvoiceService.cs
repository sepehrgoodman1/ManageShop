using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;

namespace ManageShop.Services.PurchaseInvoices.Contracts
{
    public interface PurchaseInvoiceService
    {
        Task<int> Add(List<AddPurchaseInvoiceDto> dto);
        Task<List<GetPurchaseInvoiceDto>> GetAll();
        Task<List<GetPurchaseInvoiceDto>> Search(string search);
    }
}
