using ManageShop.Entities.Entities;

namespace ManageShop.Services.Products.Contracts
{
    public interface ProductRepository
    {
        Task Add(Product product);
        Task<bool> HaveProduct(int productGroupId);
        Task<bool> IsExistTitle(string title, int productGroupId);
    }
}
