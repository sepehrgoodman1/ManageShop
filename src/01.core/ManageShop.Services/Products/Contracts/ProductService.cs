using ManageShop.Services.Products.Contracts.Dtos;

namespace ManageShop.Services.Products.Contracts
{
    public interface ProductService
    {
        Task Add(AddProductDto dto);
    }
}
