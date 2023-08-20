using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using ManageShop.Services.Products.Contracts;
using ManageShop.Services.Products.Contracts.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageShop.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet("get-all-products")]
        public async Task<List<GetProductDto>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost("add-a-product")]
        public async Task Add(AddProductDto dto)
        {
             await _service.Add(dto);
        }
    }
}
