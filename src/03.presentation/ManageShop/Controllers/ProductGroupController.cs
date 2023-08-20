using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ManageShop.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductGroupController : ControllerBase
    {
        private readonly ProductGroupService _service;

        public ProductGroupController(ProductGroupService service)
        {
            _service = service;
        }

        [HttpGet("get-all-product-groups")]
        public async Task<List<GetProductGroupDto>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost("add-product-group")]
        public async Task<int> Add(AddProductGroupDto dto)
        {
           return await _service.Add(dto);
        }

        [HttpPut("update-product-group")]
        public async Task Update(int id, AddProductGroupDto dto)
        {
            await _service.Update(id, dto);
        }

        [HttpDelete("delete-a-product-group")]
        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }
    }
}
