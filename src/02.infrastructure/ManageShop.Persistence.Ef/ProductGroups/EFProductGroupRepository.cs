
using ManageShop.Entities.Entities;
using ManageShop.Services.ProductGroups.Contracts;
using ManageShop.Services.ProductGroups.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.ProductGroups
{
    public class EFProductGroupRepository : ProductGroupRepository
    {
        private readonly DbSet<ProductGroup> _productGroups;

        public EFProductGroupRepository(EFDataContext context)
        {
            _productGroups = context.ProductGroups;
        }

        public async Task Add(ProductGroup productGroup)
        {
            await _productGroups.AddAsync(productGroup);

        }

        public  void Delete(ProductGroup productGroup)
        {
             _productGroups.Remove(productGroup);
        }

        public async Task<ProductGroup> GetById(int id)
        {
            return await _productGroups.FindAsync(id);
        }

        public async Task<bool> IsExistByName(string name)
        {
            return await _productGroups.AnyAsync(_ => _.Name == name);
        }
    }
}
