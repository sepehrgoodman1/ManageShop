using ManageShop.Entities.Entities;
using ManageShop.Services.Products.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.Productss
{
    public class EFProductRepository : ProductRepository
    {
        private readonly DbSet<Product> _products;

        public EFProductRepository(EFDataContext context)
        {
            _products = context.Products;
        }

        public async Task<bool> HaveProduct(int productGroupId)
        {
            return await _products.AnyAsync(_=> _.ProductGroupId == productGroupId);
        }
    }
}
