using ManageShop.Services.PurchaseInvoices.Contracts;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ManageShop.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PurchaseInvoiceController : ControllerBase
    {
        private readonly PurchaseInvoiceService _service;

        public PurchaseInvoiceController(PurchaseInvoiceService service)
        {
            _service = service;
        }

        [HttpGet("get-all-purchase-invoices")]
        public async Task<List<GetPurchaseInvoiceDto>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet("search-purchase-invoices")]
        public async Task<List<GetPurchaseInvoiceDto>> Search(string search)
        {
            return await _service.Search(search);
        }

        [HttpPost("add-purchase-invoice")]
        public async Task<int> Add(List<AddPurchaseInvoiceDto> dto)
        {
            return await _service.Add( dto);
        }
    }
}
