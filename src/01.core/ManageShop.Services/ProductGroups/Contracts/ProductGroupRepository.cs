using ManageShop.Entities.Entities;

namespace ManageShop.Services.ProductGroups.Contracts
{
    public interface ProductGroupRepository
    {
        Task Add(ProductGroup productGroup);
        void Delete(ProductGroup productGroup);
        Task<List<ProductGroup>> GetAll();
        Task<ProductGroup> GetById(int id);
        Task<bool> IsExistByName(string name);
    }
}
