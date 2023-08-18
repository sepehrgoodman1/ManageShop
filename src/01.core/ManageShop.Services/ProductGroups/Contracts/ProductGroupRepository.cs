using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Contracts.Dtos;

namespace ManageShop.Services.ProductGroups.Contracts
{
    public interface ProductGroupRepository
    {
        Task Add(ProductGroup productGroup);
        void Delete(ProductGroup productGroup);
        Task<ProductGroup> GetById(int id);
        Task<bool> IsExistByName(string name);
    }
}
