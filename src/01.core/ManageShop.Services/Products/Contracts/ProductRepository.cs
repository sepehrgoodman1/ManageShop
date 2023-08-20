using ManageShop.Entities.Entities;

namespace ManageShop.Services.Products.Contracts
{
    public interface ProductRepository
    {
        Task Add(Product product);
        Task<List<Product>> FindAllByIds(List<int> enumerable);
        Task<bool> HaveProduct(int productGroupId);
        Task<bool> IsExistTitle(string title, int productGroupId);
        void Update(List<Product> products);
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
    }
}
