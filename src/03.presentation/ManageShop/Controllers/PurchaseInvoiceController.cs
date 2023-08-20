using ManageShop.Services.Products.Contracts;
using ManageShop.Services.PurchaseInvoices.Contracts;
using ManageShop.Services.PurchaseInvoices.Contracts.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpPost("add-purchase-invoice")]
        public async Task<int> Add(List<AddPurchaseInvoiceDto> dto)
        {
            return await _service.Add( dto);
        }
    }
}
