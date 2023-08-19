using ManageShop.Services.ProductGroups.Contracts.Dtos;

namespace ManageShop.Services.ProductGroups.Contracts
{
    public interface ProductGroupService
    {
        Task<int> Add(AddProductGroupDto dto);
        Task Delete(int id);
        Task Update(int id, AddProductGroupDto dto);
    }
}
