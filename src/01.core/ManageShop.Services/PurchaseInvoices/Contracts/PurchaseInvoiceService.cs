using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Services.PurchaseInvoices.Contracts
{
    public interface PurchaseInvoiceService
    {
        Task<int> Add(List<AddPurchaseInvoiceDto> dto);
        Task<List<GetPurchaseInvoiceDto>> GetAll();
    }
}
